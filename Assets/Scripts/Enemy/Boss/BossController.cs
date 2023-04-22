using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class BossController : MonoBehaviour {
    public enum Boss_AnimState {
        StaticSleep, Wake, Idle, Move, TurnAroundToLeft, TurnAroundToRight, RangeAttack, SpinCharge, SpinChargeEnd, Buff, MeleeAttack, SuperAttack, Death, Hit
    }
    public BossScript BossData;
    public GameObject BossHealthBarGameObject;
    public Slider BossHealthBar;
    public Boss_AnimState BossAnimState=Boss_AnimState.StaticSleep;
    private Animator BossAnim;
    private Rigidbody2D Boss;
    Vector3 PlayerPos;
    private int Level = 1;
    private string LastSkill = "";

    private bool canAttack, isFacingRight = false;
    public bool canWake, InAttackArea, Attacking;
    public float AttackCDTime;
    public float AttackCDTimer;

    public float MoveSpeed, ChargeSpeed;

    public int Health;
    public bool Death;
    public bool isHit;
    public bool Buff;

    public LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start() {
        BossAnim = GetComponent<Animator>();
        Boss = GetComponent<Rigidbody2D>();
        BossHealthBar=BossHealthBarGameObject.GetComponent<Slider>();
        BossHealthBar.value = 1f;
        BossHealthBarGameObject.SetActive(false);
        BossAnimState = Boss_AnimState.StaticSleep;
        BossAnim.SetInteger("state", (int)BossAnimState);
        Health = BossData.Healthmax;
    }

    // Update is called once per frame
    void Update() {
        if (isHit)
            BossAnimState = Boss_AnimState.Hit;
        if (Health >= BossData.Healthmax / 3f && Health <= BossData.Healthmax * 2f / 3f && Level == 1) {
            Level = 2;
            LastSkill = "";
            Buff = true;
        }
        if (Health <= BossData.Healthmax / 3f && Health > 0 && Level == 2) {
            Level = 3;
            LastSkill = "";
            Buff = true;
        }
        if (Health <= 0) {
            Death = true;
            BossAnimState = Boss_AnimState.Death;
        }
        if (Buff && !Attacking) {
            BossAnimState = Boss_AnimState.Buff;
        }
        PlayerPos = GameObject.Find("Red").transform.position;
        CheckifWake();
        if (!Death && BossAnimState != Boss_AnimState.StaticSleep && BossAnimState != Boss_AnimState.Wake) {
            if (!Attacking && !canAttack && BossAnimState != Boss_AnimState.Buff)
                Move();
            if (InAttackArea && !Attacking && canAttack && BossAnimState != Boss_AnimState.Buff)
                Attack();
            if (!canAttack) {
                AttackCDTimer -= Time.deltaTime;
                if (AttackCDTimer <= 0)
                    if (InAttackArea)
                        canAttack = true;
            }
        }
        SetAnim();
    }
    void CheckifWake() {
        if (canWake && BossAnimState == Boss_AnimState.StaticSleep) {
            BossAnimState = Boss_AnimState.Wake;
            BossHealthBarGameObject.SetActive(true);
        }
    }
    void Move() {
        Flip();
        if (!InAttackArea) {
            MoveTowardsPlayer();
            if(!isHit)
                BossAnimState = Boss_AnimState.Move;
        } else {
            StayIdle();
            if (!isHit)
                BossAnimState = Boss_AnimState.Idle;
        }
    }
    void Flip() {
        if (PlayerPos.x > transform.position.x && !isFacingRight) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = !isFacingRight;
        }
        if (PlayerPos.x < transform.position.x && isFacingRight) {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            isFacingRight = !isFacingRight;
        }
    }
    void MoveTowardsPlayer() {
        if (PlayerPos.x > transform.position.x)
            Boss.velocity = new Vector2(MoveSpeed, Boss.velocity.y);
        else
            Boss.velocity = new Vector2(-MoveSpeed, Boss.velocity.y);
    }
    void StayIdle() {
        Boss.velocity = new Vector2(0, Boss.velocity.y);
    }
    void Attack() {
        isHit = false;
        switch (NextSkill()) {
            case "RangeAttack": RangeAttack(); break;
            case "SpinCharge": SpinCharge(); break;
            case "MeleeAttack": MeleeAttack(); break;
            case "SuperAttack": SuperAttack(); break;
            case "MeleeAttack2": MeleeAttack2(); break;
        }
        InAttackArea = false;
    }
    public string NextSkill() {
        switch (Level) {
            case 1:
                switch (LastSkill) {
                    case "": return "RangeAttack";
                    case "RangeAttack": return "SpinCharge";
                    case "SpinCharge": return "RangeAttack";
                }
                break;
            case 2:
                switch (LastSkill) {
                    case "": return "RangeAttack";
                    case "RangeAttack": return "SpinCharge";
                    case "SpinCharge": return "MeleeAttack";
                    case "MeleeAttack": return "RangeAttack";
                }
                break;
            case 3:
                switch (LastSkill) {
                    case "": return "SuperAttack";
                    case "SuperAttack": return "SpinCharge";
                    case "SpinCharge": return "MeleeAttack2";
                    case "MeleeAttack2": return "SuperAttack";
                }
                break;
        }
        return null;
    }
    void RangeAttack() {
        Attacking = true;
        BossAnimState = Boss_AnimState.RangeAttack;
        LastSkill = "RangeAttack";
    }
    void SpinCharge() {
        Attacking = true;
        BossAnimState = Boss_AnimState.SpinCharge;
        Boss.velocity = new Vector2(ChargeSpeed * (isFacingRight?1:-1), Boss.velocity.y);
        LastSkill = "SpinCharge";
    }
    void MeleeAttack() {
        Attacking = true;
        BossAnimState = Boss_AnimState.MeleeAttack;
        LastSkill = "MeleeAttack";
    }
    void SuperAttack() {
        Attacking = true;
        BossAnimState = Boss_AnimState.SuperAttack;
        LastSkill = "SuperAttack";
    }
    void MeleeAttack2() {
        Attacking = true;
        BossAnimState = Boss_AnimState.MeleeAttack;
        LastSkill = "MeleeAttack2";
    }
    void CreateLackey() {
        if (LastSkill == "MeleeAttack") {
            MeleeAttackBehavior.Instance.CreateBombDroid(new Vector2(transform.position.x - 3f, 6.32f));
            MeleeAttackBehavior.Instance.CreateBombDroid(new Vector2(transform.position.x + 3f, 6.32f));
        } else {
            MeleeAttackBehavior.Instance.CreateBombDroid(new Vector2(transform.position.x - 5f, 6.32f));
            MeleeAttackBehavior.Instance.CreateBombDroid(new Vector2(transform.position.x, 6.32f));
            MeleeAttackBehavior.Instance.CreateBombDroid(new Vector2(transform.position.x + 5f, 6.32f));
        }
    }
    void SpinChargeOver() {
        BossAnimState = Boss_AnimState.SpinChargeEnd;
    }
    public void AttackOver() {
        Attacking = false;
        canAttack = false;
        AttackCDTimer = AttackCDTime;
        BossAnimState = Boss_AnimState.Idle;
    }
    public void WakeOver() {
        BossHealthBarGameObject.SetActive(true);
        BossAnimState = Boss_AnimState.Idle;
    }
    public void HitOver() {
        isHit = false;
        if(!Attacking)
            BossAnimState = Boss_AnimState.Idle;
    }
    public void BuffOver() {
        Buff = false;
        if (!Attacking)
            BossAnimState = Boss_AnimState.Idle;
    }
    void DeathOver() {
        Destroy(gameObject);
        levelLoader.loadnextlevel = true;
    }
    void SetAnim() {
        BossAnim.SetInteger("state",(int)BossAnimState);
    }
}
