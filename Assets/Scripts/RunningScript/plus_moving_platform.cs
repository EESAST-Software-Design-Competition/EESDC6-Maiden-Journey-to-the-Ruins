using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plus_moving_platform : MonoBehaviour
{
    public float speed;
    public float waittime;
    public Transform[] movePos;

    private int i;
    private Transform playerDefTransform;

    // Start is called before the first frame update
    void Start()
    {
        i = 4;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            if (waittime < 0.0f)
            {
                switch (i)
                {
                    case 0:i = 1;break;
                    case 1:i = 2;break;
                    case 2:i = 3;break;
                    case 3:i = 4;break;
                    case 4:i = 0;break;
                    default:i = 0;break;
                }
                waittime = 0.5f;
            }
            else
            {
                waittime -= Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.parent = playerDefTransform;
        }
    }
}