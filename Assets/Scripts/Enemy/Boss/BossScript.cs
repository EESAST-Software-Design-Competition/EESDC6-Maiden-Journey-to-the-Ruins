using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BossData", menuName = "BossData", order = 0)]

public class BossScript : ScriptableObject {
    public int Healthmax;
    public int RangeAttackDamage;
    public int SpinChargeDamage;
    public int SuperAttackDamage;
}
