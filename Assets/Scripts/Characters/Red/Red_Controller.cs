using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class Red_Controller : MonoBehaviour
{
    public enum Player_AnimState{
        Still,Run,Wink,Rise,Run_jump,Fall,Slide_down,Dodge,Show,Attack1,Attack2,Attack3,Climb,Hit,landpre,jumppre
    };
    //Wink Still
    //Run
    //Jump
    //Attack1 Attack2 Attack3

    private int canStart;
    public Player_AnimState PlayerAnimState;
    public float BonusTimeMax;
    private float BonusTime;
    private int AttackLevel;
    private bool Attacking;
    private bool AttackingEnd = true;
    public bool Hitting;

    private bool EnableJump;
    private bool jumping;
    private bool jumpstop;
    private float jumpstopTime=0.1f;
    private float jumpstopTimer;
    private bool jumpingup;
    private bool jumpingdown;
    private bool landing;
    private bool WolfTime;
    private float EnableJumpTime=0.2f;
    private float EnableJumpTimer;

    public float IdleTime;
    public float IdleTimer;
    private bool Winking;

    public float DodgeCDTimer;
    private bool Dodging;
    private bool Dodgover = true;

    public bool invincible;

    public GameObject Arrow_UI, Arrow, SwordImpact, swordimpactparent, Meteorolite, Hint;
    public float DashAngle;
    public CanvasGroup BlackScreen;
    public Vector3 mousePos1, mousePos2;


    public bool DashMode;
    private bool Dash1;
    public bool Dashing;
    public bool DashInAir;
    public float DashTime;
    public float DashTimer;
    public float DashCDTimer;
    private int moveDir,PlayerFaceDir = 1;
    private Rigidbody2D Player;
    public PlayerScript PlayerData;
    public TextMeshProUGUI StartText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimState = Player_AnimState.Still;
        Player=GetComponent<Rigidbody2D>();
        Player.gravityScale = PlayerData.PlayerGravityScale;
        BlackScreen = GameObject.Find("BlackSpace").GetComponent<CanvasGroup>();
        StartText= GameObject.Find("StartText").GetComponent<TextMeshProUGUI>();
        swordimpactparent = GameObject.Find("bullet");
        IdleTimer = IdleTime;
        PlayerFaceDir = 1;
        if(StartText!=null)
            StartText.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Hint != null && Hint.activeInHierarchy && Input.GetButtonDown("Interact"))
            Hint.SetActive(false);
        if (StartText != null) {
            if (canStart > 0) {
                if (Input.GetButtonDown("changescene"))
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                StartText.enabled = true;
            } else StartText.enabled = false;
        }
        Run();
        Jump();
        Hit();
        if (AbilityInst.Instance != null) {
            if (AbilityInst.Instance.BasicAttack) Attack();
            if (AbilityInst.Instance.FireBallAttack) BulletAttack();
            if (AbilityInst.Instance.Dash) Dash();
        }
        SetWink();
        Dodge();
        Airf();
        SetAnim();
        if (Input.GetButtonDown("Interact")){//”Înpcª•∂Ø
            RaycastHit2D hit = Physics2D.Raycast(Player.position, new Vector2(PlayerFaceDir, 0), 2f, LayerMask.GetMask("NPC"));
            if (hit.collider != null){
                npc_manager npc = hit.collider.GetComponent<npc_manager>();
                if (npc != null)
                    npc.ShowDialog();
            }
        }
    }
    void Run() {
        float moveDir_ = Input.GetAxisRaw("Horizontal");
        if (!Attacking && !DashInAir && !Dodging) {
            if (moveDir_ > 0.5f) moveDir = PlayerFaceDir = 1;
            else if (moveDir_ < -0.5f) moveDir = PlayerFaceDir = -1;
            else moveDir = 0;
        }
        if (moveDir == 0) {
            if (!jumping && !Attacking && !Dodging && AttackingEnd && Dodgover && !Winking && !landing && !DashInAir) {
                PlayerAnimState = Player_AnimState.Still;
                Player.velocity = new Vector2(0, Player.velocity.y);
            }
            if((jumping&&!DashInAir)||landing)
                Player.velocity = new Vector2(0, Player.velocity.y);
        } else {
            if (!Dodging)
                Dodgover = true;
            if (!AttackingEnd)
                AttackingEnd = true;
            if (!Attacking && !DashInAir && Dodgover) {
                Player.velocity = new Vector2(moveDir * PlayerData.RunSpeed, Player.velocity.y);
                transform.localRotation = Quaternion.Euler(0, moveDir == -1 ? 180 : 0, 0);
            }
            if (!jumping && !Attacking && !Dodging) {
                PlayerAnimState = Player_AnimState.Run;
            }
        }
    }
    void Jump() {
        if (jumping && !Attacking) {
            if (Player.velocity.y > -Mathf.Epsilon) {
                jumpingup = true;
                if (Player.velocity.y > 0.1f) {
                    WolfTime = true;
                    if (PlayerAnimState != Player_AnimState.Rise)
                        PlayerAnimState = Player_AnimState.Rise;
                }
            }
            if (Player.velocity.y < -Mathf.Epsilon && WolfTime) {
                PlayerAnimState = Player_AnimState.Fall;
                jumpingup = false;
                jumpingdown = true;
            }
            if (CheckGrounded.Instance.Grounded && jumpingdown) {
                PlayerAnimState = Player_AnimState.landpre;
                jumping = false;
                jumpingdown = false;
                landing = true;
            }
        }
        if (PlayerAnimState != Player_AnimState.landpre && landing)
            landing = false;
        if (Input.GetButtonDown("Jump")) {
            EnableJump = true;
            EnableJumpTimer = EnableJumpTime;
        }
        if (EnableJump) {
            if (!Dodging && !Attacking && CheckGrounded.Instance.Grounded) {
                EnableJump = false;
                EnableJumpTimer = 0;
                jumping = true;
                jumpstop = false;
                WolfTime = false;
                jumpingup = jumpingdown = false;
                PlayerAnimState = Player_AnimState.jumppre;
                jumpstopTimer = jumpstopTime;
                //jump
            } else {
                EnableJumpTimer -= Time.deltaTime;
            }
        }
        if (EnableJumpTimer < 0)
            EnableJump = false;
        if (Input.GetButtonUp("Jump"))
            jumpstop = true;
        if (jumpstopTimer > 0)
            jumpstopTimer -= Time.deltaTime;
        if (jumpstop && jumping && jumpstopTimer <= 0) {
            if (Player.velocity.y > 0.1f) {
                jumpstop = false;
                Player.velocity = new Vector2(Player.velocity.x, Player.velocity.y * 0.5f);
                Debug.Log(Player.velocity.y);
            }
        }
    }
    void Attack() {
        if (Input.GetButtonDown("FAttack")&&!Attacking&&!Hitting&&!Dodging&&(!jumping||(jumping&&WolfTime))) {
            Dodgover = true;
            if (BonusTime < Mathf.Epsilon) {
                Player.velocity = new Vector2(PlayerFaceDir * PlayerData.AttackSpeed, Player.velocity.y);
                PlayerAnimState = Player_AnimState.Attack1;
                if(!jumping)
                    BonusTime = BonusTimeMax;
                Attacking = true;
                AttackingEnd = false;
            } else {
                if (AttackLevel==2) {
                    Player.velocity = new Vector2(PlayerFaceDir * PlayerData.AttackSpeed, Player.velocity.y);
                    PlayerAnimState = Player_AnimState.Attack3;
                    Attacking = true;
                    AttackingEnd = false;
                }
                if (AttackLevel==1) {
                    Player.velocity = new Vector2(PlayerFaceDir * PlayerData.AttackSpeed, Player.velocity.y);
                    PlayerAnimState = Player_AnimState.Attack2;
                    BonusTime = BonusTimeMax;
                    Attacking = true;
                    AttackingEnd = false;
                }
            }
        }
        if (BonusTime > Mathf.Epsilon)
            BonusTime -= Time.deltaTime;
    }
    void Dash() {
        Vector3 playerPos = transform.position;
        Communicate.Instance.DashTimeleft = DashCDTimer;
        if (Input.GetMouseButtonDown(1) && DashCDTimer <= 0) {
            DashCDTimer = PlayerData.DashCDTime;
            Dash1 = true;
            jumping = true;
            mousePos1 = Input.mousePosition;
            DashMode = true;
            Arrow = Instantiate(Arrow_UI, new Vector3(playerPos.x, playerPos.y, playerPos.z), Quaternion.Euler(new Vector3(0, 0, DashAngle)));
            Time.timeScale = 0.1f;
        }
        if (Dash1) {
            if (DashMode) {
                mousePos2 = Input.mousePosition;
                if (mousePos2 == mousePos1)
                    DashAngle = 90;
                else if (mousePos2.x - mousePos1.x < 0)
                    DashAngle = 180 * Mathf.Atan((mousePos2.y - mousePos1.y) / (mousePos2.x - mousePos1.x)) / Mathf.PI + 180;
                else
                    DashAngle = 180 * Mathf.Atan((mousePos2.y - mousePos1.y) / (mousePos2.x - mousePos1.x)) / Mathf.PI;
                BlackScreen.alpha = 0.3f;
                Arrow.transform.position = Player.transform.position;
                Arrow.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, DashAngle));
                DashTimer = DashTime;
            }
            if (Input.GetMouseButtonUp(1)) {
                Destroy(Arrow);
                DashMode = false;
                BlackScreen.alpha = 0;
                Time.timeScale = 1f;
            }
            if (DashTimer > 0 && DashMode == false) {
                if (!CheckGrounded.Instance.Grounded)
                    DashInAir = true;
                Dashing = true;
                DashTimer -= Time.deltaTime;
                Player.velocity = Vector2.Lerp(Player.velocity, new Vector2(PlayerData.DashSpeed * Mathf.Cos(DashAngle * Mathf.PI / 180), PlayerData.DashSpeed * Mathf.Sin(DashAngle * Mathf.PI / 180)), 5);
            } else
                Dashing = false;
            if (CheckGrounded.Instance.Grounded) {
                DashInAir = false;
            }
        }
        DashCDTimer -= Time.deltaTime;
    }
    void Hit() {
        if (Hitting) {
            if(!jumping)
                PlayerAnimState = Player_AnimState.Hit;
            else
                Hitting = false;
            Attacking = false;
        }
    }
    void SetWink() {
        if (!Input.anyKey)
            IdleTimer -= Time.deltaTime;
        else {
            Winking = false;
            IdleTimer = IdleTime;
        }
        if (IdleTimer < 0) {
            Winking = true;
            PlayerAnimState = Player_AnimState.Wink;
            IdleTimer = IdleTime;
        }
    }
    void Dodge() {
        Communicate.Instance.DodgeTimeleft = DodgeCDTimer;
        if (Input.GetButtonDown("Dodge")&&!Attacking&&!jumping&& DodgeCDTimer<=0 && !DashInAir &&!Hitting) {
            Dodging = invincible = true;
            AttackingEnd = true;
            Dodgover = false;
            DodgeCDTimer = PlayerData.DodgeCDTime;
            PlayerAnimState = Player_AnimState.Dodge;
            Player.velocity = new Vector2(PlayerFaceDir*PlayerData.DodgeSpeed, Player.velocity.y);
        }else
            DodgeCDTimer -= Time.deltaTime;
    }
    void Airf() {
        Player.velocity = new Vector2(Player.velocity.x*0.9983f,Mathf.Max(Player.velocity.y,-14f));
    }
    public void CreateSwordImpact() {
        if (AbilityInst.Instance.shortRangeAttackMagicEffect == AbilityInst.ShortRangeAttackMagicEffect.swordimpact) {
            Instantiate(SwordImpact, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.Euler(new Vector3(0, 0, PlayerFaceDir == -1 ? 180 : 0)), swordimpactparent.transform);
            BulletControl.Instance.swordimpactDirection = PlayerFaceDir;
        }
        if (AbilityInst.Instance.shortRangeAttackMagicEffect == AbilityInst.ShortRangeAttackMagicEffect.decreaseCD) {
            if (Random.value > 0.6) {
                DodgeCDTimer -= 1;
                DashCDTimer -= 1;
                BulletControl.Instance.ShootingCDTimer -= 1;
            }
        }
    }
    public void CreateMeteorolite() {
        if (AbilityInst.Instance.shortRangeAttackMagicEffect == AbilityInst.ShortRangeAttackMagicEffect.meteorolite) {
            BulletControl.Instance.MeteoroliteAngle = PlayerFaceDir == -1 ? 300 : 240;
            Instantiate(Meteorolite, new Vector3(transform.position.x + 17f * PlayerFaceDir, transform.position.y + 17.32f, transform.position.z), Quaternion.Euler(new Vector3(0, 0, BulletControl.Instance.MeteoroliteAngle)), swordimpactparent.transform);
        }
    }
    public void jumppreover() {
        PlayerAnimState = Player_AnimState.Rise;
        jumpingup = true;
        Player.velocity = new Vector2(Player.velocity.x, PlayerData.JumpSpeed);
    }
    public void landpreover() {
        landing = false;
        PlayerAnimState = Player_AnimState.Still;
    }
    public void WinkOver() {
        Winking = false;
    }
    public void DodgingOver() {
        Dodging = invincible = false;
        Player.velocity = new Vector2(0, Player.velocity.y);
    }
    public void DodgeOver() {
        Dodgover = true;
    }
    void HitOver() {
        Hitting = false;
        PlayerAnimState = Player_AnimState.Still;
    }
    void AttackOver(int atklevel) {
        Attacking = false;
        AttackLevel = atklevel;
    }
    void AttackEndOver() {
        AttackingEnd = true;
    }
    void BulletAttack() {
        BulletControl.Instance.BulletController(transform.position);
    }
    void SetAnim() {
        AnimSetter.Instance.SetAnim(PlayerAnimState);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("LoadScene"))
            canStart++;
        if (collision.CompareTag("Hint")) {
            Hint.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("LoadScene"))
            canStart--;
    }
}
