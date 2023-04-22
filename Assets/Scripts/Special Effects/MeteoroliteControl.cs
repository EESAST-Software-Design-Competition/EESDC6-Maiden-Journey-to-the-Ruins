using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroliteControl : MonoBehaviour {
    private Rigidbody2D RigidbodyMeteorolite;
    public float ShotSpeed;
    public float ExistTime;
    private bool DamangEnabled;
    private float ExistTimer;
    public GameObject Explosion1;
    public SpriteRenderer Explosion2;
    private float screenShakeTime = 0.04f;
    private float screenShakeStrength = 0.04f;
    public int damage;
    // Start is called before the first frame update
    void Start() {
        RigidbodyMeteorolite = GetComponent<Rigidbody2D>();
        RigidbodyMeteorolite.velocity = new Vector2(ShotSpeed* (BulletControl.Instance.MeteoroliteAngle==240?-0.5f:0.5f), -ShotSpeed * 0.866f);
        DamangEnabled = true;
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
        if (DamangEnabled)
            if (other.CompareTag("AttackedArea") && !other.GetComponentInParent<EnemyScriptAttach>().Death) {
                Instantiate(Explosion1, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), transform.parent);
                timeDeceleration(0.2f);
                AttackSense.Instance.Shake(screenShakeTime, screenShakeStrength);
                DamangEnabled = false;
                Explosion2.enabled = false;
            } else if (other.CompareTag("ForeGround")) {
                Instantiate(Explosion1, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), transform.parent);
                timeDeceleration(0.2f);
                AttackSense.Instance.Shake(screenShakeTime, screenShakeStrength);
                DamangEnabled = false;
                Explosion2.enabled = false;
            }
    }
    void timeDeceleration(float duration) {
        StartCoroutine(ExplosionEffect(duration));
    }
    IEnumerator ExplosionEffect(float duration) {
        Time.timeScale = 0.4f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
