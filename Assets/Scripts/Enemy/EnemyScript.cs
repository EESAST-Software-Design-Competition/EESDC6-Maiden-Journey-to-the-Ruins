using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;
[CreateAssetMenu(fileName = "EnemyData",menuName = "EnemyScriptData",order = 0)]
public class EnemyScript : ScriptableObject {
    public int Healthmax;
    public int Damage;
    public int Damage2;
    public float Speed;
    public float RepelSpeed;
    public float StaggerTime;
}
