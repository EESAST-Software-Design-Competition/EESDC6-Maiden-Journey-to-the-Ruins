using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordinpact : MonoBehaviour {
    private Rigidbody2D RigidbodySwordImpact;
    public float ShotSpeed;
    public float ExistTime;
    private float ExistTimer;
    private float screenShakeTime = 0.04f;
    private float screenShakeStrength = 0.04f;
    public int damage;
    // Start is called before the first frame update
    void Start() {
        RigidbodySwordImpact = GetComponent<Rigidbody2D>();
        RigidbodySwordImpact.velocity = new Vector2(ShotSpeed * BulletControl.Instance.swordimpactDirection, 0);
        ExistTimer = ExistTime;
    }

    // Update is called once per frame
    void Update() {
        if (ExistTimer > Mathf.Epsilon) {
            ExistTimer -= Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("AttackedArea") && !other.GetComponentInParent<EnemyScriptAttach>().Death) {
            other.GetComponentInParent<EnemyScriptAttach>().TakeDamage(damage, other.transform.position.x > transform.position.x ? -1 : 1, 0.05f, false);
            AttackSense.Instance.Shake(screenShakeTime, screenShakeStrength);
        } else if (other.CompareTag("ForeGround")) {
            Destroy(gameObject);
        }
    }
}
