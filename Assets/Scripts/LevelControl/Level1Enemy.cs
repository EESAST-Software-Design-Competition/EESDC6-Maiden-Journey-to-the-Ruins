using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level1Enemy : MonoBehaviour {
    // Start is called before the first frame update
    public float EnemyCreateTime;
    private float EnemyCreateTimer;
    private System.Random birthrand;
    public GameObject Bat;
    public Transform EnemyPoint;
    private int EnemyExistnum;
    public int Enemynum;
    public LevelCommunicater levelCommunicater;
    public LevelLoader levelLoader;
    void Start() {
        birthrand = new System.Random();
        EnemyCreateTimer = 3f;
        levelCommunicater = GameObject.Find("Communicate").GetComponent<LevelCommunicater>();
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update() {
        EnemyCreateTimer -= Time.deltaTime;
        if (EnemyCreateTimer <= 0&&EnemyExistnum<Enemynum) {
            CreateBat(birthrand.Next(0, 5));
            EnemyCreateTimer = EnemyCreateTime;
            EnemyExistnum++;
        }
        if (levelCommunicater.killnum >= 2*Enemynum) {
            levelLoader.loadnextlevel = true;
        }
    }
    void CreateBat(int birthnum) {
        Instantiate(Bat, transform.GetChild(0).GetChild(birthnum).transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), EnemyPoint);
    }
}
