using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererBotAttack2Judge : MonoBehaviour {
    private EnemyScriptAttach Main;
    public SorcererBotController SorcererBotController;
    // Start is called before the first frame update
    void Start() {
        Main = transform.parent.GetComponent<EnemyScriptAttach>();
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            SorcererBotController.InAttack2Area = true;
            if (SorcererBotController.Attack2CoolTimer <= 0  && !SorcererBotController.Attacking2 && !SorcererBotController.Attacking1 && !Main.Death) {
                SorcererBotController.Attacking2 = true;
                SorcererBotController.Attack2CoolTimer = SorcererBotController.Attack2CoolTime;
                transform.parent.GetComponent<Animator>().SetTrigger("Attack2");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            SorcererBotController.InAttack2Area = false;
        }
    }
}
