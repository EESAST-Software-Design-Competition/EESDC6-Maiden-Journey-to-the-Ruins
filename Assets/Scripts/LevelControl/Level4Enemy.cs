using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Enemy : MonoBehaviour {
    // Start is called before the first frame update
    public float EnemyCreateTime;
    private float EnemyCreateTimer;
    public GameObject Bat;
    public GameObject BotWheel;
    public GameObject FlameThrower;
    public GameObject SorcererBot;
    public Transform EnemyPoint1, EnemyPoint2, EnemyPoint3, EnemyPoint4;
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
            CreateBat(2);
            CreateBotWheel(0);
            CreateBotWheel(1);
            CreateFlameThrower(0);
            CreateFlameThrower(1);
            CreateSorcererBot(0);
            CreateSorcererBot(1);
            EnemyExistnum += 9;
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
    void CreateFlameThrower(int birthnum) {
        Instantiate(FlameThrower, transform.GetChild(2).GetChild(birthnum).transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), EnemyPoint3);
    }
    void CreateSorcererBot(int birthnum) {
        Instantiate(SorcererBot, transform.GetChild(3).GetChild(birthnum).transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), EnemyPoint4);
    }
}
