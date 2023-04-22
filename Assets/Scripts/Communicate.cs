using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Communicate : MonoBehaviour
{

    private static Communicate instance;
    public static Communicate Instance {
        get {
            if (instance == null)
                instance = Transform.FindObjectOfType<Communicate>();
            return instance;
        }
    }
    public AbilityData abilityDataR;
    public int AbilityChooseLevel;
    public int CurrentLevel;
    public float DodgeTimeleft;
    public float DashTimeleft;
    public float FireballTimeleft;
    public Material _Default;
    public Material _Decelerate;
    public Material _Stun;
    public Material _Confuse;
    public Material _Poison;
    public bool ChooseDash;
    public int lastSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
