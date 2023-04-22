using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AbilityInst;

public class AbilityToChoose : MonoBehaviour
{
    Button button1,button2;
    TextMeshProUGUI text1, text2;
    TextMeshProUGUI text11, text21;
    int ChooseState;
    float random;
    // Start is called before the first frame update
    void Start() {
        button1 = transform.GetChild(1).GetComponent<Button>();
        button2 = transform.GetChild(2).GetComponent<Button>();
        text1 = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        text2 = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        text11 = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        text21 = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        button1.onClick.AddListener(ChooseAbility1);
        button2.onClick.AddListener(ChooseAbility2);
            random = Random.value;
        ChooseState = (int)(random / 0.1666f);
        switch (Communicate.Instance.CurrentLevel) {
            case 2:
                switch (ChooseState) {
                    case 0:
                        text1.text = "剑术精进"; text2.text = "冰霜之刃";
                        text11.text = "攻击会对敌人造成迟缓效果"; text21.text = "攻击敌人造成短时间冰冻";
                        break;
                    case 1:
                        text1.text = "剑术精进"; text2.text = "精神控制";
                        text11.text = "攻击会对敌人造成迟缓效果"; text21.text = "攻击敌人造成短时间混乱效果";
                        break;
                    case 2:
                        text1.text = "剑术精进"; text2.text = "淬毒之刃";
                        text11.text = "攻击会对敌人造成迟缓效果"; text21.text = "攻击敌人附带中毒效果";
                        break;
                    case 3:
                        text1.text = "冰霜之刃"; text2.text = "精神控制";
                        text11.text = "攻击敌人造成短时间冰冻"; text21.text = "攻击敌人造成短时间混乱效果";
                        break;
                    case 4:
                        text1.text = "冰霜之刃"; text2.text = "淬毒之刃";
                        text11.text = "攻击敌人造成短时间冰冻"; text21.text = "攻击敌人附带中毒效果";
                        break;
                    case 5:
                        text1.text = "精神控制"; text2.text = "淬毒之刃";
                        text11.text = "攻击敌人造成短时间混乱效果"; text21.text = "攻击敌人附带中毒效果";
                        break;
                }
                break;
            case 3:
                if (ChooseState <= 2) {
                    text1.text = "敏捷步法"; text2.text = "法术精通";
                    text11.text = "按住右键并移动鼠标以冲刺"; text21.text = "按住左键并移动鼠标以发射火球";
                } else {
                    text1.text = "法术精通"; text2.text = "敏捷步法";
                    text11.text = "按住左键并移动鼠标以发射火球"; text21.text = "按住右键并移动鼠标以冲刺";
                }
                break;
            case 4:
                switch (ChooseState) {
                    case 0:
                        text1.text = "剑术大师"; text2.text = "唤星术";
                        text11.text = "普通攻击释放剑气"; text21.text = "重击召唤陨石";
                        break;
                    case 1:
                        text1.text = "剑术大师"; text2.text = "游刃有余";
                        text11.text = "普通攻击释放剑气"; text21.text = "攻击有概率减少技能CD";
                        break;
                    case 2:
                        text1.text = "唤星术"; text2.text = "剑术大师";
                        text11.text = "重击召唤陨石"; text21.text = "普通攻击释放剑气";
                        break;
                    case 3:
                        text1.text = "唤星术"; text2.text = "游刃有余";
                        text11.text = "重击召唤陨石"; text21.text = "攻击有概率减少技能CD";
                        break;
                    case 4:
                        text1.text = "游刃有余"; text2.text = "剑术大师";
                        text11.text = "攻击有概率减少技能CD"; text21.text = "普通攻击释放剑气";
                        break;
                    case 5:
                        text1.text = "游刃有余"; text2.text = "唤星术";
                        text11.text = "攻击有概率减少技能CD"; text21.text = "重击召唤陨石";
                        break;
                }
                break;
            case 5:
                if (Communicate.Instance.ChooseDash) {
                    text1.text = "法术精通"; text2.text = "法术精通";
                    text11.text = "按住左键并移动鼠标以发射火球"; text21.text = "按住左键并移动鼠标以发射火球";
                } else {
                    text1.text = "敏捷步法"; text2.text = "敏捷步法";
                    text11.text = "按住右键并移动鼠标以冲刺"; text21.text = "按住右键并移动鼠标以冲刺";
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChooseAbility1() {
        switch (Communicate.Instance.CurrentLevel) {
            case 2:
                if (ChooseState == 0 || ChooseState == 1 || ChooseState == 2)
                    AbilityInst.Instance.shortRangeAttackElementEffect = ShortRangeAttackElementEffect.decelerate;
                if (ChooseState == 3 || ChooseState == 4)
                    AbilityInst.Instance.shortRangeAttackElementEffect = ShortRangeAttackElementEffect.stun;
                if (ChooseState == 5)
                    AbilityInst.Instance.shortRangeAttackElementEffect = ShortRangeAttackElementEffect.confuse;
                break;
            case 3:
                if (ChooseState <= 2) {
                    AbilityInst.Instance.Dash = true;
                    Communicate.Instance.ChooseDash = true;
                } else
                    AbilityInst.Instance.FireBallAttack = true;
                break;
            case 4:
                if (ChooseState == 0 || ChooseState == 1)
                    AbilityInst.Instance.shortRangeAttackMagicEffect = ShortRangeAttackMagicEffect.swordimpact;
                if (ChooseState == 2 || ChooseState == 3)
                    AbilityInst.Instance.shortRangeAttackMagicEffect = ShortRangeAttackMagicEffect.meteorolite;
                if (ChooseState == 4 || ChooseState == 5)
                    AbilityInst.Instance.shortRangeAttackMagicEffect = ShortRangeAttackMagicEffect.decreaseCD;
                break;
            case 5:
                if(Communicate.Instance.ChooseDash)
                    AbilityInst.Instance.FireBallAttack = true;
                else
                    AbilityInst.Instance.Dash = true;
                break;
            default:
                break;
        }
        SceneManager.LoadScene(Communicate.Instance.CurrentLevel + 1);
    }
    public void ChooseAbility2() {
        switch (Communicate.Instance.CurrentLevel) {
            case 2:
                if (ChooseState == 0)
                    AbilityInst.Instance.shortRangeAttackElementEffect = ShortRangeAttackElementEffect.stun;
                if (ChooseState == 1 || ChooseState == 3)
                    AbilityInst.Instance.shortRangeAttackElementEffect = ShortRangeAttackElementEffect.confuse;
                if (ChooseState == 2 || ChooseState == 4 || ChooseState == 5)
                    AbilityInst.Instance.shortRangeAttackElementEffect = ShortRangeAttackElementEffect.poison;
                break;
            case 3:
                if (ChooseState <= 2)
                    AbilityInst.Instance.FireBallAttack = true;
                else {
                    AbilityInst.Instance.Dash = true;
                    Communicate.Instance.ChooseDash = true;
                }
                break;
            case 4:
                if (ChooseState == 2 || ChooseState == 4)
                    AbilityInst.Instance.shortRangeAttackMagicEffect = ShortRangeAttackMagicEffect.swordimpact;
                if (ChooseState == 0 || ChooseState == 5)
                    AbilityInst.Instance.shortRangeAttackMagicEffect = ShortRangeAttackMagicEffect.meteorolite;
                if (ChooseState == 1 || ChooseState == 3)
                    AbilityInst.Instance.shortRangeAttackMagicEffect = ShortRangeAttackMagicEffect.decreaseCD;
                break;
            case 5:
                if (Communicate.Instance.ChooseDash)
                    AbilityInst.Instance.FireBallAttack = true;
                else
                    AbilityInst.Instance.Dash = true;
                break;
            default:
                break;
        }
        SceneManager.LoadScene(Communicate.Instance.CurrentLevel + 1);
    }
}
