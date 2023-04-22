using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour {
    public Transform target1;
    public Transform target2;
    public Vector3 target;
    public float smoothing;
    public float smoothing2;
    private Vector3 currentpos;
    private Camera MainCamera;
    public GameObject maincamera;
    public float ViewField;
    public float TargetViewField;
    public float distance;
    public float ViewFieldMultiplier;
    public float ViewRate;

    // Start is called before the first frame update
    void Start() {
        MainCamera = transform.GetChild(0).GetComponent<Camera>();
        MainCamera.orthographicSize = ViewField;
    }

    void LateUpdate() {
    }
    // Update is called once per frame
    void Update() {
        if (target2 == null) {
            currentpos = maincamera.transform.position;
            float freezz = currentpos.z;
            if (currentpos != target1.position) {
                maincamera.transform.position = Vector3.Lerp(currentpos, target1.position, smoothing);
            }
            maincamera.transform.position = new Vector3(maincamera.transform.position.x, maincamera.transform.position.y, -10);
        } else {
            distance=(target1.position-target2.position).magnitude;
            /*TargetViewField = ViewField * distance * ViewFieldMultiplier;
            if (TargetViewField > ViewField) {
                if (TargetViewField> MainCamera.orthographicSize+smoothing2) {
                    MainCamera.orthographicSize += smoothing2;
                }
                if (TargetViewField < MainCamera.orthographicSize - smoothing2) {
                    MainCamera.orthographicSize -= smoothing2;
                }
            }
            */
            target = target1.position * ViewRate + target2.position * (1- ViewRate);
            currentpos = MainCamera.transform.position;
            float freezz = currentpos.z;
            if (currentpos != target) {
                maincamera.transform.position = Vector3.Lerp(currentpos, target, smoothing);
            }
            if(maincamera.transform.position.x<-4.5)
                maincamera.transform.position = new Vector3(-4.5f, maincamera.transform.position.y, -10);
            if (maincamera.transform.position.x >= 4.5)
                maincamera.transform.position = new Vector3(4.5f, maincamera.transform.position.y, -10);
            if (maincamera.transform.position.y < -2)
                maincamera.transform.position = new Vector3(maincamera.transform.position.x, -2f, -10);

            maincamera.transform.position = new Vector3(maincamera.transform.position.x, maincamera.transform.position.y, -10);
        }
    }
}
