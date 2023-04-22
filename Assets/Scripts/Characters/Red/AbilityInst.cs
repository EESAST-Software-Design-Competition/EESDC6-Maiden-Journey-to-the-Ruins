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
     * ǿ��������
     * ��ս������������Ч�������ʼ��١����ʻ��Ρ����ʻ��ҡ������ж���
     * ��ս�������з���Ч�����ͷŽ����������콵��ʯ�����ʼ��ټ���cd��
     * Զ��
     * 
    */
}