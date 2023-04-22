using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehavior : MonoBehaviour {
    private static MeleeAttackBehavior instance;
    public static MeleeAttackBehavior Instance {
        get {
            if (instance == null)
                instance = Transform.FindObjectOfType<MeleeAttackBehavior>();
            return instance;
        }
    }
    public GameObject BombDroid;
    public GameObject EnemyParent;
    // Start is called before the first frame update
    void Start()
    {
        EnemyParent = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateBombDroid(Vector3 Pos) {
        Instantiate(BombDroid,Pos, Quaternion.Euler(new Vector3(0, 0, 0)),EnemyParent.transform);
    }
}
