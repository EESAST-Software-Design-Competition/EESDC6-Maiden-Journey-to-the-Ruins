using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Final : MonoBehaviour
{
    BoxCollider2D m_BoxCollider;
    LevelLoader levelLoader;
    public bool InRange;
    // Start is called before the first frame update
    void Start()
    {

        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InRange && Input.GetButtonDown("Interact")) {
            levelLoader.GameAccepted = true;
            levelLoader.loadnextlevel = true;
            Debug.Log("Trig");
        }

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            InRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            InRange = false;
    }
}
