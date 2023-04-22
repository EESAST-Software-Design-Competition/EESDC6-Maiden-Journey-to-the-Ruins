using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fail_detector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.position = new Vector3(-13.4f, 0.4f, 0f);
            }
        }
    // Update is called once per frame
    void Update()
    {
    }
}
