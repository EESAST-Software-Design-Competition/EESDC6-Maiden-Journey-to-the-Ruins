using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSense : MonoBehaviour {
    private static AttackSense instance;
    private bool isShaking;
    public static AttackSense Instance {
        get {
            if (instance == null)
                instance = Transform.FindObjectOfType<AttackSense>();
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Shake(float shaketime, float strength) {
        if (!isShaking)
            StartCoroutine(ShakeScreen(shaketime, strength));
    }

    IEnumerator ShakeScreen(float shaketime, float strength) {
        isShaking = true;
        Transform camera = Camera.main.transform;
        Vector3 startposition = camera.position;
        while (shaketime > 0) {
            camera.position = Random.insideUnitSphere * strength + startposition;
            shaketime -= Time.deltaTime;
            yield return null;
        }
        camera.position = startposition;
        isShaking = false;
    }

    public void HitPause(int frameduration) {
        StartCoroutine(Pause(frameduration));
    }

    IEnumerator Pause(int frameduration) {
        float duration = frameduration * Time.deltaTime;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;

    }
}
