using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AbilityData", menuName = "AbilityData", order = 0)]
public class AbilityData : ScriptableObject {
    public float decelerateTime;
    public float decelerateRate;
    public float stunTime;
    public float confuseTime;
    public float poisonTime;
    public int poisonDamage;
}
