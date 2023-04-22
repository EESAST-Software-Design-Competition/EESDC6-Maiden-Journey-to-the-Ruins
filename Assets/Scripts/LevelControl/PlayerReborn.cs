using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReborn : MonoBehaviour
{
    LevelLoader levelloader;
    // Start is called before the first frame update
    void Start()
    {
        levelloader=GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Reborn() {
        levelloader.LoadNextLevel(Communicate.Instance.lastSceneIndex);
    }
}
