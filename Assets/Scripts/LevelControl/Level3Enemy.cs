using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Enemy : MonoBehaviour {
    // Start is called before the first frame update
    public float EnemyCreateTime;
    private float EnemyCreateTimer;
    public GameObject FlameThrower;
    public GameObject SorcererBot;
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
            CreateFlameThrower(0);
            CreateFlameThrower(1);
            CreateSorcererBot(0);
            CreateSorcererBot(1);
            EnemyExistnum += 4;
        }
        if (levelCommunicater.killnum >= 2 * Enemynum) {
            levelLoader.loadnextlevel = true;
        }
    }
    void CreateFlameThrower(int birthnum) {
        Instantiate(FlameThrower, transform.GetChild(0).GetChild(birthnum).transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), EnemyPoint1);
    }
    void CreateSorcererBot(int birthnum) {
        Instantiate(SorcererBot, transform.GetChild(1).GetChild(birthnum).transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), EnemyPoint2);
    }
}
