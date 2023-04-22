using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Red_Controller;

public class FAttack : MonoBehaviour
{
    public int[] AttackDamage=new int[3];
    private int HitPauseframe=5;
    private float screenShakeTime=0.06f;
    private float screenShakeStrength=0.08f;

    // Start is called before the first frame update
    void Start()
    {
        if (AbilityInst.Instance.shortRangeAttackMagicEffect == AbilityInst.ShortRangeAttackMagicEffect.swordimpact) {
            AttackDamage[0] = AttackDamage[0] * 3 / 4;
            AttackDamage[1] = AttackDamage[1] * 3 / 4;
            AttackDamage[2] = AttackDamage[2] * 3 / 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other) {
        Player_AnimState AttackMode = transform.parent.GetComponent<Red_Controller>().PlayerAnimState;
        if (other.CompareTag("AttackedArea")) {
            switch (AttackMode) {
                case Player_AnimState.Attack1:
                    other.GetComponentInParent<EnemyScriptAttach>().TakeDamage(AttackDamage[0], other.transform.position.x > transform.position.x ? -1 : 1, 0.2f, true); break;
                case Player_AnimState.Attack2:
                    other.GetComponentInParent<EnemyScriptAttach>().TakeDamage(AttackDamage[1], other.transform.position.x > transform.position.x ? -1 : 1, 0.2f, true); break;
                case Player_AnimState.Attack3:
                    other.GetComponentInParent<EnemyScriptAttach>().TakeDamage(AttackDamage[2], other.transform.position.x > transform.position.x ? -1 : 1, 0.2f, true); break;
            }
            AttackSense.Instance.HitPause(HitPauseframe);
            AttackSense.Instance.Shake(screenShakeTime, screenShakeStrength);
        }
        if (other.CompareTag("EnemyBullet")) {
            Destroy(other.gameObject);
        }
    }
}
