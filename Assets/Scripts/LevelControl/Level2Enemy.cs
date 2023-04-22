using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level2Enemy : MonoBehaviour {
    // Start is called before the first frame update
    public float EnemyCreateTime;
    private float EnemyCreateTimer;
    public GameObject Bat;
    public GameObject BotWheel;
    public Transform EnemyPoint1, EnemyPoint2;
    private int EnemyExistnum;
    public int Enemynum;
    public LevelCommunicater levelCommunicater;
    public LevelLoader levelLoader;
    void Start() {
        EnemyCreateTimer = 3f;
        levelCommunicater = GameObject.Find("Communicate").GetComponent<LevelCommunicater>();
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update() {
        EnemyCreateTimer -= Time.deltaTime;
        if (EnemyCreateTimer <= 0 && EnemyExistnum < Enemynum) {
            CreateBat(0);
            CreateBat(1);
            CreateBotWheel(0);
            CreateBat(2);
            CreateBat(3);
            CreateBotWheel(1);
            EnemyExistnum+=6;
        }
        if (levelCommunicater.killnum >= 2 * Enemynum) {
            levelLoader.loadnextlevel = true;
        }
    }
    void CreateBat(int birthnum) {
        Instantiate(Bat, transform.GetChild(0).GetChild(birthnum).transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), EnemyPoint1);
    }
    void CreateBotWheel(int birthnum) {
        Instantiate(BotWheel, transform.GetChild(1).GetChild(birthnum).transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), EnemyPoint2);
    }
}
