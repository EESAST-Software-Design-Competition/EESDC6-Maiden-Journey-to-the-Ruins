using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCDController : MonoBehaviour
{
    private Image DodgeImage;
    private Image DashImage;
    private Image FireballImage;
    public PlayerScript PlayerData;
    // Start is called before the first frame update
    void Start() {
        DodgeImage = GameObject.Find("DodgeMask").GetComponent<Image>();
        DashImage = GameObject.Find("DashMask").GetComponent<Image>();
        FireballImage = GameObject.Find("FireballMask").GetComponent<Image>();
        if (AbilityInst.Instance != null) {
            transform.GetChild(1).gameObject.SetActive(AbilityInst.Instance.Dash);
            transform.GetChild(2).gameObject.SetActive(AbilityInst.Instance.FireBallAttack);
        }
    }

    // Update is called once per frame
    void Update() {
        DodgeImage.fillAmount = Communicate.Instance.DodgeTimeleft / PlayerData.DodgeCDTime;
        DashImage.fillAmount = Communicate.Instance.DashTimeleft / PlayerData.DashCDTime;
        FireballImage.fillAmount = Communicate.Instance.FireballTimeleft / PlayerData.ShootingCDTime;
    }
}
