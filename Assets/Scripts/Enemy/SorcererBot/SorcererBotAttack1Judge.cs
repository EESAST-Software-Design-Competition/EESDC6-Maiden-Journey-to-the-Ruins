using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererBotAttack1Judge : MonoBehaviour {
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
            SorcererBotController.InAttack1Area = true;
            if (SorcererBotController.Attack1CoolTimer <= 0  && !SorcererBotController.Attacking1 && !SorcererBotController.Attacking2 && !Main.Death) {
                SorcererBotController.Attacking1 = true;
                SorcererBotController.Attack1CoolTimer = SorcererBotController.Attack1CoolTime;
                transform.parent.GetComponent<Animator>().SetTrigger("Attack1");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            SorcererBotController.InAttack1Area = false;
        }
    }
}
