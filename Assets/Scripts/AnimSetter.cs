using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSetter : MonoBehaviour
{
    // Start is called before the first frame update
    private static AnimSetter instance;
    public static AnimSetter Instance {
        get {
            if (instance == null)
                instance = Transform.FindObjectOfType<AnimSetter>();
            return instance;
        }
    }
    Animator PlayerAnim;
    void Start()
    {
        PlayerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAnim(Red_Controller.Player_AnimState AnimState) {
        PlayerAnim.SetInteger("status",((int)AnimState));
    }
}
