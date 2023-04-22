using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotWheelController : EnemyScriptAttach {
    private Transform PlayerPosition;
    private Transform BulletParent;
    public EnemyScriptAttach Main;
    private Vector3 PlayerPos;
    public bool isfacingright;
    private bool StaticIdle;
    private bool Wake;
    private bool Wakeover;
    private bool Move;
    private bool Charge;
    private bool Shoot;
    private bool GASDash;
    private bool PlayerEnterWakeArea;
    private bool PlayerInShootingArea;
    private bool PlayerInGASDashArea;
    private bool GASDashAvailable;
    private bool HitFlip;
    public float WakeRadius;
    public float ShootingRadius;
    public float GASDashRadius;
    public float ShootColdTime;
    private float ShootColdTimer;
    public LayerMask PlayerLayer;
    public GameObject BotWheelBullet;
    // Start is called before the first frame update
    new void Start() {
        base.Start();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        BulletParent = GameObject.Find("EnemyBullet").transform;
        StaticIdle = true;
        isfacingright = true;
        ShootColdTimer = 0;
        GASDashAvailable = false;
    }

    // Update is called once per frame
    new void Update() {
        Main.attacking = Charge && Shoot && GASDash;
        if (PlayerEnterWakeArea) {
            if (Wake) {
                if (Wakeover) {
                    Move = true;
                }
            } else {
                StaticIdle = false;
                Anim.SetTrigger("Wake");
                Wake = true;
            }
        }
        if (PlayerInGASDashArea&& GASDashAvailable) {
            //GASDash
            GASDashAvailable = false;
        }else if ((PlayerInGASDashArea||PlayerInShootingArea) && ShootColdTimer <= 0) {
            Move = false;
            EnemyRigidbody.velocity = new Vector2(0, EnemyRigidbody.velocity.y);
            Charge = true;
            ShootColdTimer = ShootColdTime;
        }
        MoveTowardsPlayer(EnemyData.Speed);
        updateArea();
        updateAnimator();
        if(ShootColdTimer>0)
            ShootColdTimer-= Time.deltaTime;
        if (Main.isHit) {
            if (!Wake) {
                StaticIdle = false;
                Anim.SetTrigger("Wake");
                Wake = true;
            }
            if (!isfacingright && Main.RepelDirection == 1)
                flip();
            if (isfacingright && Main.RepelDirection == -1)
                flip();
        }

    }
    void flip() {
        isfacingright = !isfacingright;
        if (isfacingright) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.GetChild(1).transform.localRotation = Quaternion.Euler(0, 0, 0);
        } else {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            transform.GetChild(1).transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void MoveTowardsPlayer(float speed) {
        if (Move) {
            PlayerPos = PlayerPosition.position;
            if (PlayerPos.x > transform.position.x) {
                EnemyRigidbody.velocity = new Vector2(speed, EnemyRigidbody.velocity.y);
                if (!isfacingright)
                    flip();
            } else {
                EnemyRigidbody.velocity = new Vector2(-speed, EnemyRigidbody.velocity.y);
                if (isfacingright)
                    flip();
            }
        }
    }
    void updateArea() {
        PlayerInGASDashArea = Physics2D.OverlapCircle(transform.position, GASDashRadius, PlayerLayer);
        if(!PlayerInGASDashArea)
            PlayerInShootingArea = Physics2D.OverlapCircle(transform.position, ShootingRadius, PlayerLayer);
        else
            PlayerInShootingArea = false;
        if (!PlayerInShootingArea&&!PlayerInGASDashArea)
            PlayerEnterWakeArea = Physics2D.OverlapCircle(transform.position, WakeRadius, PlayerLayer);
        else
            PlayerEnterWakeArea = false;
    }
    void updateAnimator() {
        Anim.SetBool("StaticIdle", StaticIdle);
        if (Wake)
            Anim.SetTrigger("Wake");
        Anim.SetBool("Move", Move);
        if (Charge) {
            Anim.SetTrigger("Charge");
            Charge = false;
        }
        if (Shoot) {
            Anim.SetTrigger("Shoot");
            Shoot = false;
        }
    }
    public void WakeOver() {
        Wakeover = true;
    }
    public void ChargeOver() {
        Shoot = true;
    }
    public void ShootStart() {

        if (isfacingright)
            Instantiate(BotWheelBullet, transform.position + new Vector3(1.5f, 0.45f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), BulletParent);
        else
            Instantiate(BotWheelBullet, transform.position + new Vector3(-1.5f, 0.45f, 0), Quaternion.Euler(new Vector3(0, 0, 180)), BulletParent);
    }
}
