using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    public AbilityData AbilityData; 
    protected int Health;
    public bool isHit;
    public bool Death;
    protected float StaggerTimer;
    public int RepelDirection;
    protected Animator Anim, StrikeEffectAnim;
    public Rigidbody2D EnemyRigidbody;
    public Slider HealthSlider;
    public LevelCommunicater levelCommunicater;
    // Start is called before the first frame update
    protected void Start() {
        Anim = GetComponent<Animator>();
        EnemyRigidbody = GetComponent<Rigidbody2D>();
        HealthSlider = transform.GetChild(1).transform.GetChild(0).GetComponent<Slider>();
        levelCommunicater = GameObject.Find("Communicate").GetComponent<LevelCommunicater>();
        AbilityData= GameObject.Find("AllCommunicate").GetComponent<Communicate>().abilityDataR;
    }



    // Update is called once per frame
    protected void Update() {

    }
    protected void Die() {
        Destroy(gameObject);
        levelCommunicater.killnum++;
    }
}
