using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator loadAnim;
    public bool loadnextlevel;
    public bool GameAccepted;
    public float loadTime;
    public TextMeshProUGUI loadtext;
    public LevelCommunicater LevelCommunicater;
    // Update is called once per frame
    private void Start() {
        loadAnim = GameObject.Find("ImageTrans").GetComponent<Animator>();
        loadtext = GameObject.Find("LoadText").GetComponent<TextMeshProUGUI>();
        LevelCommunicater = GameObject.Find("AllCommunicate").GetComponent<LevelCommunicater>();
    }
    void Update()
    {
        if (loadnextlevel) {
            if (GameAccepted) {
                loadAnim.SetTrigger("GameEnd");
                if (Input.GetButtonDown("Exit"))
                    ExitGame();
            } else {
                if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 3) {
                    Communicate.Instance.CurrentLevel = SceneManager.GetActiveScene().buildIndex;
                    Communicate.Instance.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    LoadNextLevel(SceneManager.sceneCountInBuildSettings - 1);
                } else {
                    loadAnim.SetTrigger("GameEnd");
                    if (Input.GetButtonDown("Reset")) {
                        LoadNextLevel(0);
                    }
                    if (Input.GetButtonDown("Exit"))
                        ExitGame();

                }
            }
        }
    }
    public void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying=false;
        #else
            Application.Quit();
        #endif
    }
    public void LoadNextLevel(int nextlevel) {
        Communicate.Instance.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(nextlevel));
    }
    IEnumerator LoadLevel(int levelIndex) {
        loadAnim.SetTrigger("nextlevel");
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(levelIndex);
    }
}
