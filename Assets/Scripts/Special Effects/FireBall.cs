using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Red_Controller;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D RigidbodyFireBall;
    private bool DamangEnabled;
    public float ShotSpeed;
    public float ExistTime;
    private float ExistTimer;
    public GameObject Explosion1;
    public SpriteRenderer Explosion2;
    private float screenShakeTime = 0.1f;
    private float screenShakeStrength = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        RigidbodyFireBall=GetComponent<Rigidbody2D>();
        RigidbodyFireBall.velocity = new Vector2(ShotSpeed*Mathf.Cos(Mathf.PI/180* BulletControl.Instance.ShotAngle), ShotSpeed * Mathf.Sin(Mathf.PI / 180*BulletControl.Instance.ShotAngle));
        DamangEnabled = true;
        ExistTimer = ExistTime;
    }

    // Update is called once per frame
    void Update()
    {
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
                timeDeceleration(0.5f);
                AttackSense.Instance.Shake(screenShakeTime, screenShakeStrength);
                DamangEnabled = false;
                Explosion2.enabled = false;
            } else if (other.CompareTag("ForeGround")) {
                Instantiate(Explosion1, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)), transform.parent);
                timeDeceleration(0.5f);
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
