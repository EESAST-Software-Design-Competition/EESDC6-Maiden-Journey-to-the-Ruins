using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerDataRun", menuName = "PlayerScriptDataRun", order = 0)]

public class PlayerDataRun : ScriptableObject {
    public float PlayerGravityScale;
    public float RunSpeed;
    public float JumpSpeed;
    public int HealthMax;
}
