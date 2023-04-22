using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInst : MonoBehaviour {
    private static AbilityInst instance;
    public static AbilityInst Instance {
        get {
            if (instance == null)
                instance = Transform.FindObjectOfType<AbilityInst>();
            return instance;
        }
    }
    public bool BasicAttack, FireBallAttack, Dash;
    public float FireBallCD,DashCD;
    public enum ShortRangeAttackElementEffect{
        no_effect,
        decelerate,
        stun,
        confuse,
        poison
    };
    public ShortRangeAttackElementEffect shortRangeAttackElementEffect;
    public enum ShortRangeAttackMagicEffect{
        no_effect,
        swordimpact,
        meteorolite,
        decreaseCD
    };
    public ShortRangeAttackMagicEffect shortRangeAttackMagicEffect;
    void Start() {
        DontDestroyOnLoad(gameObject);
        shortRangeAttackElementEffect = ShortRangeAttackElementEffect.no_effect;
        shortRangeAttackMagicEffect = ShortRangeAttackMagicEffect.no_effect;
        BasicAttack = true; FireBallAttack = false; Dash = false;
    }

    //private bool 
    /*
     * 强化能力：
     * 近战攻击具有属性效果（概率减速、概率击晕、概率混乱、概率中毒）
     * 近战攻击具有法术效果（释放剑气、概率天降陨石、概率减少技能cd）
     * 远程
     * 
    */
}