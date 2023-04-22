using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerController : EnemyScriptAttach {
    public Transform PlayerPosition;
    private EnemyScriptAttach Main;
    public float AttackCoolTime;
    public float AttackCoolTimer;
    public bool Attacking;
    public bool InAttackArea;
    public bool Run;
    public bool Idle;
    public bool isfacingright;
    // Start is called before the first frame update
    new void Start() {
        base.Start();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        Main = GetComponent<EnemyScriptAttach>();
        Idle = true;
        isfacingright = true;
    }

    // Update is called once per frame
    new void Update() {
        Main.attacking = Attacking;
        if (!Attacking&&!Main.Death) {
            if (!InAttackArea) {
                Run = true;
                Idle = false;
                if (PlayerPosition.position.x > transform.position.x + 0.1f)
                    EnemyRigidbody.velocity = new Vector2(EnemyData.Speed, EnemyRigidbody.velocity.y);
                else if (PlayerPosition.position.x < transform.position.x - 0.1f)
                    EnemyRigidbody.velocity = new Vector2(-EnemyData.Speed, EnemyRigidbody.velocity.y);
                else
                    EnemyRigidbody.velocity = new Vector2(0, EnemyRigidbody.velocity.y);
                if ((!isfacingright&& EnemyRigidbody.velocity.x>=0)||(isfacingright && EnemyRigidbody.velocity.x < 0))
                    flip();
            } else {
                Run = false;
                Idle = true;
                EnemyRigidbody.velocity = new Vector2(0, EnemyRigidbody.velocity.y);
            }
        }else
            EnemyRigidbody.velocity = new Vector2(0, EnemyRigidbody.velocity.y);
        AttackCoolTimer -= Time.deltaTime;
        Anim.SetBool("Attacking", Attacking);
        Anim.SetBool("Idle", Idle);
        Anim.SetBool("Run", Run);
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
    public void AttackOver() {
        Attacking = false;
        AttackCoolTimer = AttackCoolTime;
    }
 
}
