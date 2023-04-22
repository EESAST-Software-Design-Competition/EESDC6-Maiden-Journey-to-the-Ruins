using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BotWheelBullet : MonoBehaviour {
    private Rigidbody2D Bullet;
    public Transform Player;
    public EnemyScript BotWheelData;
    public float Speed;
    public Vector2 Velocity;
    private Vector2 Velocitynew;
    private Vector2 Displacement;
    public float theta;
    private float DHoriz, DVert,FlyAngle;
    public float Forcemag;
    // Start is called before the first frame update
    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Bullet = GetComponent<Rigidbody2D>();
        if (transform.localRotation.z ==1){
            Bullet.velocity = new Vector2(-Speed, 0);
        } else {
            Bullet.velocity = new Vector2(Speed, 0);
        }
    }

    // Update is called once per frame4
    void FixedUpdate()
    {
        Velocity = Bullet.velocity;
        Displacement = Player.position-transform.position;
        Velocitynew = new Vector2(Velocity.x * Mathf.Cos(theta) - Velocity.y * Mathf.Sin(theta), Velocity.y * Mathf.Cos(theta) + Velocity.x * Mathf.Sin(theta));
        if (Vector2.Dot(Velocitynew, Displacement) > Vector2.Dot(Velocity, Displacement)) {
            Bullet.velocity = Velocitynew;
        } else {
            Velocitynew = new Vector2(Velocity.x * Mathf.Cos(-theta) - Velocity.y * Mathf.Sin(-theta), Velocity.y * Mathf.Cos(-theta) + Velocity.x * Mathf.Sin(-theta));
            Bullet.velocity = Velocitynew;
        }
        DHoriz = Velocity.x;
        DVert = Velocity.y;
        if (DHoriz < 0)
            FlyAngle = 180 * Mathf.Atan(DVert / DHoriz) / Mathf.PI + 180;
        else
            FlyAngle = 180 * Mathf.Atan(DVert / DHoriz) / Mathf.PI;
        transform.localRotation = Quaternion.Euler(0, 0, FlyAngle);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerHealth>().PlayerTakeDamage(BotWheelData.Damage, 0.1f);
            Destroy(gameObject);
        }
    }
}
