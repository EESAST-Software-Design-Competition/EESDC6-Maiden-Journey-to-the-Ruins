using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Red_Controller;

public class PlayerHealth : MonoBehaviour {
    LevelLoader levelloader;
    private int Health;
    private Animator PlayerAnim;
    private Slider HealthSlider;
    private Red_Controller Red_Controller;
    private TextMeshProUGUI HealthText;
    public PlayerScript PlayerData;
    private Image buffer;
    // Start is called before the first frame update
    void Start() {
        levelloader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        Health = PlayerData.HealthMax;
        PlayerAnim = GetComponent<Animator>();
        Red_Controller = GetComponent<Red_Controller>();
        HealthSlider = GameObject.Find("PlayerHealthBar").GetComponent<Slider>();
        HealthText = GameObject.Find("PlayerHealthText").GetComponent<TextMeshProUGUI>();
        HealthSlider.value = Health * 1f / PlayerData.HealthMax;
        HealthText.SetText(Health.ToString()+"/"+ PlayerData.HealthMax.ToString());
        buffer = GameObject.Find("Buffer").GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        buffer.fillAmount = Mathf.Lerp(HealthSlider.value,buffer.fillAmount, 0.9f);
        
    }
    public void PlayerDeath() {
        Destroy(gameObject);
    }
    public void PlayerTakeDamage(int damage,float StaggerTime) {
        if (Red_Controller.invincible)
            return;
        Health = Mathf.Max( Health-damage,0);
        HealthSlider.value = 1.0f * Health / PlayerData.HealthMax;
        HealthText.SetText(Health.ToString() + "/" + PlayerData.HealthMax.ToString());
        if (Health == 0) {
            levelloader.LoadNextLevel(SceneManager.sceneCountInBuildSettings - 2);
        } else {
            Red_Controller.Hitting = true;
        }
    }
}
