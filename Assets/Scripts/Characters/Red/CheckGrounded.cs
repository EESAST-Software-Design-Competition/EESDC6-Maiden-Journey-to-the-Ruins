using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour{
    private static CheckGrounded instance;
    public static CheckGrounded Instance {
        get {
            if (instance == null)
                instance = Transform.FindObjectOfType<CheckGrounded>();
            return instance;
        }
    }
    private BoxCollider2D feet;
    public bool Grounded=false;
    void Start()
    {
        feet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        Grounded = feet.IsTouchingLayers(LayerMask.GetMask("ForeGround"));
    }
}
