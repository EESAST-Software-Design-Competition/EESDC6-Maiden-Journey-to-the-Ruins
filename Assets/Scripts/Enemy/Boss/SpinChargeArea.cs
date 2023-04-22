using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinChargeArea : MonoBehaviour
{
    public BossScript BossData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerHealth>().PlayerTakeDamage(BossData.SpinChargeDamage, 0.05f);
        }
    }
}
