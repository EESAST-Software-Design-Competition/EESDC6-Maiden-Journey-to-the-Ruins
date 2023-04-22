using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletControl : MonoBehaviour {
    private static BulletControl instance;
    public static BulletControl Instance {
        get {
            if (instance == null)
                instance = Transform.FindObjectOfType<BulletControl>();
            return instance;
        }
    }
    public PlayerScript PlayerData;
    public GameObject FireBall,Arrow_UI,Arrow,Player;
    public float ShotAngle;
    public CanvasGroup BlackScreen;
    public bool ShootingTime;
    public float ShootingSpeed;
    private bool Shooting;
    public float ShootingCDTimer;
    public Vector3 mousePos1,mousePos2;
    public int swordimpactDirection;
    public float MeteoroliteAngle;
    private void Start() {
        Player = GameObject.Find("Red");
        BlackScreen = GameObject.Find("BlackSpace").GetComponent<CanvasGroup>();
    }
    public void BulletController(Vector3 playerPos) {
        Communicate.Instance.FireballTimeleft = ShootingCDTimer;
        if (Input.GetMouseButtonDown(0) && ShootingCDTimer <= 0) {
            ShootingCDTimer = PlayerData.ShootingCDTime;
            Shooting = true;
            mousePos1 = Input.mousePosition;
            Arrow = Instantiate(Arrow_UI, new Vector3(playerPos.x, playerPos.y + 0.5f, playerPos.z), Quaternion.Euler(new Vector3(0, 0, ShotAngle)), transform);
            ShootingTime = true;
            Time.timeScale = 0.1f;
        }
        if (Shooting) {
            if (ShootingTime) {
                mousePos2 = Input.mousePosition;
                if (mousePos2 == mousePos1)
                    ShotAngle = 90;
                else if (mousePos2.x - mousePos1.x < 0)
                    ShotAngle = 180 * Mathf.Atan((mousePos2.y - mousePos1.y) / (mousePos2.x - mousePos1.x)) / Mathf.PI + 180;
                else
                    ShotAngle = 180 * Mathf.Atan((mousePos2.y - mousePos1.y) / (mousePos2.x - mousePos1.x)) / Mathf.PI;
                BlackScreen.alpha = 0.3f;
                Arrow.transform.position = Player.transform.position;
                Arrow.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, ShotAngle));
            }
            if (Input.GetMouseButtonUp(0)) {
                Instantiate(FireBall, new Vector3(playerPos.x, playerPos.y + 0.5f, playerPos.z), Quaternion.Euler(new Vector3(0, 0, ShotAngle)), transform);
                Destroy(Arrow);
                ShootingTime = false;
                BlackScreen.alpha = 0;
                Time.timeScale = 1f;
                Shooting = false;
            }
        }
        ShootingCDTimer -= Time.deltaTime;
    }
}
