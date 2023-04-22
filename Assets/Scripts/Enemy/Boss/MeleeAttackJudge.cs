using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackJudge : MonoBehaviour {
    public BossController BossController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (BossController.NextSkill() == "MeleeAttack" || BossController.NextSkill() == "MeleeAttack2")
            BossController.InAttackArea = true;
    }
}
