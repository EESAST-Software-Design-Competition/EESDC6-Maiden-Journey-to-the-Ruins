using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerAttackJudge : MonoBehaviour {
    private EnemyScriptAttach Main;
    public FlameThrowerController FlameThrowerController;
    // Start is called before the first frame update
    void Start() {
        Main = transform.parent.GetComponent<EnemyScriptAttach>();
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            FlameThrowerController.InAttackArea = true;
            if (FlameThrowerController.AttackCoolTimer <= 0  && !FlameThrowerController.Attacking &&!Main.Death) {
                FlameThrowerController.Attacking = true;
                FlameThrowerController.AttackCoolTimer = FlameThrowerController.AttackCoolTime;
                transform.parent.GetComponent<Animator>().SetTrigger("Attack");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            FlameThrowerController.InAttackArea = false;
        }
    }
}
