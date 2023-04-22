using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class RunningController : MonoBehaviour {
    public enum Player_AnimState {
        Still, Run, Wink, Rise, Run_jump, Fall, Slide_down, Dodge, Show, Attack1, Attack2, Attack3, Climb, Hit, landpre
    };
    //Wink Still
    //Run
    //Jump
    //Attack1 Attack2 Attack3

    public Player_AnimState PlayerAnimState;
    private Animator PlayerAnim;
    public bool Hitting;
    private bool landpreover = false;
    private bool jumping = false;
    private bool jumpingup = false;
    private bool jumpingdown = false;
    public float JumpMaxTime;
    private float JumpMaxTimer;
    private bool JumpMax = false;
    private bool EnableJump;
    private bool EnableJumpOver;
    public float JumpDelayTime;
    private float JumpDelayTimer;



    private int moveDir,PlayerFaceDir = 1;
    private Rigidbody2D Player;
    public PlayerDataRun PlayerData;

    // Start is called before the first frame update
    void Start() {
        PlayerAnimState = Player_AnimState.Still;
        Player = GetComponent<Rigidbody2D>();
        Player.gravityScale = PlayerData.PlayerGravityScale;
        PlayerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Run();
        Jumpnew();
        Hit();
        Airf();
        SetAnim();
        //按下E键进行NPC交互
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit2D hit = Physics2D.Raycast(Player.position,new Vector2(PlayerFaceDir,0), 2f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                npc_manager npc = hit.collider.GetComponent<npc_manager>();
                if (npc != null)
                {
                    npc.ShowDialog();
                }
            }
        }
    }
    void Run() {
        float moveDir_ = Input.GetAxisRaw("Horizontal");
        if (moveDir_ > 0.5f) PlayerFaceDir = moveDir = 1;
        else if (moveDir_ < -0.5f) PlayerFaceDir = moveDir = -1;
        else moveDir = 0;
        if (moveDir == -1)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        if (moveDir == 1)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        Player.velocity = new Vector2(moveDir * PlayerData.RunSpeed, Player.velocity.y);
        if (!jumping) {
            if (moveDir != 0) {
                PlayerAnimState = Player_AnimState.Run;
            } else
                PlayerAnimState = Player_AnimState.Still;
        }
    }
    void Jumpnew() {

        if (Input.GetButtonDown("Jump")) {
            JumpDelayTimer = JumpDelayTime;
            EnableJump = true;
        }
        if (Input.GetButtonUp("Jump") && jumpingup)
            EnableJumpOver = true;
        if (JumpDelayTimer > 0)
            JumpDelayTimer -= Time.deltaTime;
        else
            EnableJump = false;
        if (Player.velocity.y > 4f)
            PlayerAnimState = Player_AnimState.Rise;
        if (Player.velocity.y < -4f)
            PlayerAnimState = Player_AnimState.Fall;
        if (EnableJump && CheckGrounded.Instance.Grounded) {
            Player.velocity = new Vector2(Player.velocity.x, PlayerData.JumpSpeed);
        }
    }
    void Jump() {
        if (Input.GetButtonDown("Jump")) {
            JumpDelayTimer = JumpDelayTime;
            EnableJump = true;
        }
        if (Input.GetButtonUp("Jump") && jumpingup)
            EnableJumpOver = true;
        if (JumpDelayTimer > 0)
            JumpDelayTimer -= Time.deltaTime;
        else
            EnableJump = false;
        if (EnableJump && CheckGrounded.Instance.Grounded) {
            landpreover = false;
            EnableJump = false;
            jumping = true;
            JumpMaxTimer = JumpMaxTime;
            JumpMax = false;
            PlayerAnimState = Player_AnimState.landpre;
        }//起跳预备
        if (jumping && landpreover) {
            Player.velocity = new Vector2(Player.velocity.x, PlayerData.JumpSpeed);
            jumpingup = true;
            EnableJumpOver = false;
            JumpDelayTimer = 0;
            landpreover = false;
        }//起跳
        if (Player.velocity.y > 4f)
            PlayerAnimState = Player_AnimState.Rise;
        if (Player.velocity.y < -4f)
            PlayerAnimState = Player_AnimState.Fall;
        if (Mathf.Abs(Player.velocity.x) > 0.1f && jumping && landpreover)
            PlayerAnimState = Player_AnimState.Run_jump;
        if (EnableJumpOver && jumpingup && !JumpMax) {
            Player.velocity = Vector2.Lerp(Player.velocity, new Vector2(Player.velocity.x, 0), 0.8f);
            EnableJumpOver = false;
            jumpingup = false;
            jumpingdown = true;
            JumpMax = true;
            if (Player.velocity.y <= PlayerData.JumpSpeed * 0.2f) JumpMax = true;
        }//跳跃终止，开始下降
        if (jumpingup)
            Player.gravityScale = PlayerData.PlayerGravityScale * 0.5f;
        else
            Player.gravityScale = PlayerData.PlayerGravityScale;
        if (jumping)
            JumpMaxTimer -= Time.deltaTime;
        if (JumpMaxTimer < 0 && !JumpMax) {
            Player.velocity = Vector2.Lerp(Player.velocity, new Vector2(Player.velocity.x, 0.2f * PlayerData.JumpSpeed), 0.1f);
            EnableJumpOver = false;
            jumpingup = false;
            jumpingdown = true;
            if (Player.velocity.y <= PlayerData.JumpSpeed * 0.2f) {
                JumpMax = true;
            }
        }
        if (Player.velocity.y < -15f)
            Player.velocity = new Vector2(Player.velocity.x, -15f);
        if (CheckGrounded.Instance.Grounded && jumpingdown && Mathf.Abs(Player.velocity.y) < 0.1f) {
            PlayerAnimState = Player_AnimState.landpre;
            jumping = false;
            jumpingdown = false;
            if (landpreover) {
                PlayerAnimState = Player_AnimState.Still;
                landpreover = false;
            }
        }
    }
    public void landpreOver() {
        landpreover = true;
    }
    void Hit() {
        if (Hitting) {
            if (!jumping)
                PlayerAnimState = Player_AnimState.Hit;
            else
                Hitting = false;
        }
    }
    void Airf() {
        Player.velocity = new Vector2(Player.velocity.x * 0.9985f, Player.velocity.y);
    }
    void HitOver() {
        Hitting = false;
        PlayerAnimState = Player_AnimState.Still;
    }
    void SetAnim() {
        PlayerAnim.SetInteger("status", ((int)PlayerAnimState));
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("LoadScene")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
