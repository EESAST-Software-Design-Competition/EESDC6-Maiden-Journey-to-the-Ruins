using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTextController : MonoBehaviour
{
    public TextMeshProUGUI LevelText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintAbility() {
        LevelText.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
    }

    public void PrintLevel() {
        if(SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings-2)
            LevelText.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
        else
            LevelText.text = "";
    }

    public void PrintLoading() {
        LevelText.text = "Loading...";
    }

    public void PrintEnd() {
        LevelText.text = "GameOver\nPress 'e' to exit.";
    }
}
