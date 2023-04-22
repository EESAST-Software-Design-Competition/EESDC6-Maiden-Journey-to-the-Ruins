using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WakeScript : MonoBehaviour {
    BoxCollider2D wakerange;
    public BossController bossController;
    private int dialogLevel;
    private bool inRange;
    public TextMeshProUGUI PressETrigger;
    // Start is called before the first frame update
    void Start() {
        wakerange = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (inRange&& !bossController.canWake) {
            PressETrigger.enabled = true;
            if (Input.GetButtonDown("Interact") && bossController.BossAnimState == BossController.Boss_AnimState.StaticSleep) {
                switch (dialogLevel) {
                    case 0:
                        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                        break;
                    case 1:
                        transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                        break;
                    case 2:
                        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
                        break;
                    case 3:
                        transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
                        break;
                    case 4:
                        transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false);
                        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                        bossController.canWake = true;
                        break;
                    default: break;
                }
                dialogLevel++;
            }
        } else {
            PressETrigger.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player"))
            inRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            inRange = false;
    }
}