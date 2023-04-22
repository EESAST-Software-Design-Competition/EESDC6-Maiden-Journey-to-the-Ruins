using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class esc : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject escmenu;
    public GameObject helppage;

    public void Continue()
    {
        escmenu.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void back()
    {
        GameIsPaused=false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Beginning scene");
    }

    public void help()
    {
        escmenu.SetActive(false);
        helppage.SetActive(true);
    }


    public void Pause()
    {
        escmenu.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void inback()
    {
        helppage.SetActive(false);
        escmenu.SetActive(true);
    }



    // Start is called before the first frame update
    void Start()
    {
        escmenu.SetActive(false);
        helppage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }
}
