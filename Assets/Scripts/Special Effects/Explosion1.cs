using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion1 : MonoBehaviour
{
    public int FireBallDamage;
    // Start is called before the first frame update
    public Explosion1(float size) {
        transform.localScale *= size;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyEffect() {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")&&other.GetComponent<EnemyScriptAttach>().Death==false) {
            if (other.transform.position.x > transform.position.x)
                other.GetComponent<EnemyScriptAttach>().TakeDamage(FireBallDamage, -1, 0.2f, false);
            else if (other.transform.position.x < transform.position.x)
                other.GetComponent<EnemyScriptAttach>().TakeDamage(FireBallDamage, 1, 0.2f, false);
        }
    }
}
