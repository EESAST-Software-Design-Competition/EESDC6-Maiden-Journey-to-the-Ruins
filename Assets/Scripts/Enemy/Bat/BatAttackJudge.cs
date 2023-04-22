using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackJudge : MonoBehaviour
{

    private EnemyScriptAttach Main;
    public BatController BatController;
    // Start is called before the first frame update
    void Start()
    {

        Main = transform.parent.GetComponent<EnemyScriptAttach>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")&&BatController.AttackCoolTimer<0) {
            BatController.AttackCoolTimer = BatController.AttackCoolTime;
            BatController.Attacking = true;
            transform.parent.GetComponent<Animator>().SetTrigger("Attack");
        }
    }
}
