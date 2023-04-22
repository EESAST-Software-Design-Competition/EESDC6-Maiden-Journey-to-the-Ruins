using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackJudge : MonoBehaviour {
    public BossController BossController;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player"))
            if (BossController.NextSkill() == "SuperAttack")
                BossController.InAttackArea = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (BossController.NextSkill() == "SuperAttack")
                BossController.InAttackArea = false;
        }
    }
}
