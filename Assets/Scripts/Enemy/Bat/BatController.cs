using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : EnemyScriptAttach {
    public Transform PlayerPosition;
    private float FlyAngle;
    private Vector3 PlayerPos;
    private EnemyScriptAttach Main;
    public float AttackCoolTime;
    public float AttackCoolTimer;
    private float DHoriz;
    private float DVert;
    public bool Attacking;
    // Start is called before the first frame update
    new void Start() {
        base.Start();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        Main = GetComponent<EnemyScriptAttach>();
    }

    protected void AttackOver() {
        Attacking = false;
    }
    // Update is called once per frame
    new void Update() {
        Main.attacking = Attacking;
        if (!Main.Death && !Main.isHit) {
            PlayerPos = PlayerPosition.position;
            DHoriz = (PlayerPos.x - transform.position.x);
            DVert = (PlayerPos.y - transform.position.y);
            if (DHoriz < 0)
                FlyAngle = 180 * Mathf.Atan((PlayerPos.y - transform.position.y) / (PlayerPos.x - transform.position.x)) / Mathf.PI + 180;
            else
                FlyAngle = 180 * Mathf.Atan((PlayerPos.y - transform.position.y) / (PlayerPos.x - transform.position.x)) / Mathf.PI;
            if ((DHoriz * DHoriz + DVert * DVert * 5) < 1f)
                FlyAngle = 0;
            if(FlyAngle!=0)
                EnemyRigidbody.velocity = new Vector2(EnemyData.Speed * Mathf.Cos(Mathf.PI / 180 * FlyAngle), EnemyData.Speed * Mathf.Sin(Mathf.PI / 180 * FlyAngle));
            else
                EnemyRigidbody.velocity = new Vector2(0,0);
            if (EnemyRigidbody.velocity.x < 0) {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                transform.GetChild(1).transform.localRotation = Quaternion.Euler(0, 180, 0);
            } else if (EnemyRigidbody.velocity.x > 0) {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                transform.GetChild(1).transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        } else {
            if(!Main.isHit)
                EnemyRigidbody.velocity = new Vector2(0,0);
        }
        AttackCoolTimer -= Time.deltaTime;
    }


}
