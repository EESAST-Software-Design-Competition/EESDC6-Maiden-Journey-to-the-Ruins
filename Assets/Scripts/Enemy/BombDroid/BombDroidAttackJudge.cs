using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDroidAttackJudge : MonoBehaviour {
    private EnemyScriptAttach Main;
    public BombDroidController BombDroidController;
    // Start is called before the first frame update
    void Start() {
        Main = transform.parent.GetComponent<EnemyScriptAttach>();
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            BombDroidController.InAttackArea = true;
            if (BombDroidController.AttackCoolTimer <= 0 && !BombDroidController.Attacking && !Main.Death) {
                BombDroidController.Attacking = true;
                BombDroidController.AttackCoolTimer = BombDroidController.AttackCoolTime;
                transform.parent.GetComponent<Animator>().SetTrigger("Attack");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            BombDroidController.InAttackArea = false;
        }
    }
}
