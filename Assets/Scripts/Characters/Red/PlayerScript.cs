using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerScriptData", order = 0)]

public class PlayerScript : ScriptableObject
{
    public float PlayerGravityScale;
    public float RunSpeed;
    public float JumpSpeed;
    public float AttackSpeed;
    public int HealthMax;

    public float DashSpeed;
    public float DodgeSpeed;


    public float ShootingCDTime;
    public float DodgeCDTime;
    public float DashCDTime;
}
