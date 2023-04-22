using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc_manager : MonoBehaviour
{

    public GameObject dialogImage;
    public float showTime = 3;
    public float showTimer;

    // Start is called before the first frame update
    void Start()
    {
        dialogImage.SetActive(false);
        showTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;
        if (showTimer < 0)
        {
            dialogImage.SetActive(false);
        }
    }
    public void ShowDialog()
    {
        showTimer=showTime;
        dialogImage.SetActive(true);
    }
}
