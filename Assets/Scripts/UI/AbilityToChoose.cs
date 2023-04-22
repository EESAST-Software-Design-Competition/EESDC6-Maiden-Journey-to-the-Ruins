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
                        text1.text = "��������"; text2.text = "��˪֮��";
                        text11.text = "������Ե�����ɳٻ�Ч��"; text21.text = "����������ɶ�ʱ�����";
                        break;
                    case 1:
                        text1.text = "��������"; text2.text = "�������";
                        text11.text = "������Ե�����ɳٻ�Ч��"; text21.text = "����������ɶ�ʱ�����Ч��";
                        break;
                    case 2:
                        text1.text = "��������"; text2.text = "�㶾֮��";
                        text11.text = "������Ե�����ɳٻ�Ч��"; text21.text = "�������˸����ж�Ч��";
                        break;
                    case 3:
                        text1.text = "��˪֮��"; text2.text = "�������";
                        text11.text = "����������ɶ�ʱ�����"; text21.text = "����������ɶ�ʱ�����Ч��";
                        break;
                    case 4:
                        text1.text = "��˪֮��"; text2.text = "�㶾֮��";
                        text11.text = "����������ɶ�ʱ�����"; text21.text = "�������˸����ж�Ч��";
                        break;
                    case 5:
                        text1.text = "�������"; text2.text = "�㶾֮��";
                        text11.text = "����������ɶ�ʱ�����Ч��"; text21.text = "�������˸����ж�Ч��";
                        break;
                }
                break;
            case 3:
                if (ChooseState <= 2) {
                    text1.text = "���ݲ���"; text2.text = "������ͨ";
                    text11.text = "��ס�Ҽ����ƶ�����Գ��"; text21.text = "��ס������ƶ�����Է������";
                } else {
                    text1.text = "������ͨ"; text2.text = "���ݲ���";
                    text11.text = "��ס������ƶ�����Է������"; text21.text = "��ס�Ҽ����ƶ�����Գ��";
                }
                break;
            case 4:
                switch (ChooseState) {
                    case 0:
                        text1.text = "������ʦ"; text2.text = "������";
                        text11.text = "��ͨ�����ͷŽ���"; text21.text = "�ػ��ٻ���ʯ";
                        break;
                    case 1:
                        text1.text = "������ʦ"; text2.text = "��������";
                        text11.text = "��ͨ�����ͷŽ���"; text21.text = "�����и��ʼ��ټ���CD";
                        break;
                    case 2:
                        text1.text = "������"; text2.text = "������ʦ";
                        text11.text = "�ػ��ٻ���ʯ"; text21.text = "��ͨ�����ͷŽ���";
                        break;
                    case 3:
                        text1.text = "������"; text2.text = "��������";
                        text11.text = "�ػ��ٻ���ʯ"; text21.text = "�����и��ʼ��ټ���CD";
                        break;
                    case 4:
                        text1.text = "��������"; text2.text = "������ʦ";
                        text11.text = "�����и��ʼ��ټ���CD"; text21.text = "��ͨ�����ͷŽ���";
                        break;
                    case 5:
                        text1.text = "��������"; text2.text = "������";
                        text11.text = "�����и��ʼ��ټ���CD"; text21.text = "�ػ��ٻ���ʯ";
                        break;
                }
                break;
            case 5:
                if (Communicate.Instance.ChooseDash) {
                    text1.text = "������ͨ"; text2.text = "������ͨ";
                    text11.text = "��ס������ƶ�����Է������"; text21.text = "��ס������ƶ�����Է������";
                } else {
                    text1.text = "���ݲ���"; text2.text = "���ݲ���";
                    text11.text = "��ס�Ҽ����ƶ�����Գ��"; text21.text = "��ס�Ҽ����ƶ�����Գ��";
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
