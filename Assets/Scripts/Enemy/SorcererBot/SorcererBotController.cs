using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererBotController : EnemyScriptAttach {
    public Transform PlayerPosition;
    private EnemyScriptAttach Main;
    public float Attack1CoolTime;
    public float Attack1CoolTimer;
    public float Attack2CoolTime;
    public float Attack2CoolTimer;
    public bool Attacking1;
    public bool Attacking2;
    public bool InAttack1Area;
    public bool InAttack2Area;
    public bool Run;
    public bool Idle;
    public bool isfacingright;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        Main = GetComponent<EnemyScriptAttach>();
        if (Main == null)
            Debug.Log("Main failed.");
        Idle = true;
        isfacingright = true;
    }

    // Update is called once per frame
    new void Update() {
        Main.attacking = Attacking1 || Attacking2;
        if (!Attacking1 && !Attacking2 && !Main.Death) {
            if (!InAttack1Area && !InAttack2Area) {
                Run = true;
                Idle = false;
                if (PlayerPosition.position.x > transform.position.x + 0.1f)
                    EnemyRigidbody.velocity = new Vector2(EnemyData.Speed, EnemyRigidbody.velocity.y);
                else if (PlayerPosition.position.x < transform.position.x - 0.1f)
                    EnemyRigidbody.velocity = new Vector2(-EnemyData.Speed, EnemyRigidbody.velocity.y);
                else
                    EnemyRigidbody.velocity = new Vector2(0, EnemyRigidbody.velocity.y);
                if ((!isfacingright && EnemyRigidbody.velocity.x >= 0) || (isfacingright && EnemyRigidbody.velocity.x < 0))
                    flip();
            } else {
                Run = false;
                Idle = true;
                EnemyRigidbody.velocity = new Vector2(0, EnemyRigidbody.velocity.y);
            }
        }else
            EnemyRigidbody.velocity = new Vector2(0, EnemyRigidbody.velocity.y);
        Attack1CoolTimer -= Time.deltaTime;
        Attack2CoolTimer -= Time.deltaTime;
        Anim.SetBool("Idle", Idle);
        Anim.SetBool("Walk", Run);
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
    public void Attack1Over() {
        Attacking1 = false;
        Attack1CoolTimer = Attack1CoolTime;
    }
    public void Attack2Over() {
        Attacking2 = false;
        Attack2CoolTimer = Attack2CoolTime;
    }
}
