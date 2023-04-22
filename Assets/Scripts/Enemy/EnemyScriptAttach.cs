using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using static AbilityInst;

public class EnemyScriptAttach : Enemy {
    public BossScript BossData;
    public bool isBoss;
    public BossController BossController;
    public Animator BossAnim;
    public Image buffer;

    public EnemyScript EnemyData;
    public SpriteRenderer SpriteRenderer;
    public bool attacking;
    private float stdSpeed;
    private float HitTimer;
    private float decelerateTimer;
    private float stunTimer;
    private float confuseTimer;
    private float poisonTimer;
    private float poisonPauseStand=0.1f;
    private float poisonPause;
    public Material Default;
    public Material Decelerate;
    public Material Stun;
    public Material Confuse;
    public Material Poison;
    // Start is called before the first frame update
    new void Start() {
        StrikeEffectAnim = transform.GetChild(0).GetComponent<Animator>();
        if (!isBoss) {
            base.Start();
            Health = EnemyData.Healthmax;
            HealthSlider.value = Health * 1f / EnemyData.Healthmax;
            stdSpeed = EnemyData.Speed;
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Default = Communicate.Instance._Default;
            Decelerate = Communicate.Instance._Decelerate;
            Stun = Communicate.Instance._Stun;
            Confuse = Communicate.Instance._Confuse;
            Poison = Communicate.Instance._Poison;
        } else {
            BossController.Health = BossData.Healthmax;
        }
    }

    // Update is called once per frame
    new void Update() {
        if (!isBoss) {
            switch (AbilityInst.Instance.shortRangeAttackElementEffect) {
                case ShortRangeAttackElementEffect.decelerate:
                    if (decelerateTimer > Mathf.Epsilon) {
                        decelerateTimer -= Time.deltaTime;
                        EnemyData.Speed = stdSpeed * AbilityData.decelerateRate;
                        SpriteRenderer.material = Decelerate;
                    } else {
                        EnemyData.Speed = stdSpeed;
                        SpriteRenderer.material = Default;
                    }
                    break;
                case ShortRangeAttackElementEffect.stun:
                    if (stunTimer > Mathf.Epsilon) {
                        stunTimer -= Time.deltaTime;
                        EnemyData.Speed = 0;
                        SpriteRenderer.material = Stun;
                    } else {
                        EnemyData.Speed = stdSpeed;
                        SpriteRenderer.material = Default;
                    }
                    break;
                case ShortRangeAttackElementEffect.confuse:
                    if (confuseTimer > Mathf.Epsilon) {
                        confuseTimer -= Time.deltaTime;
                        if (Random.value > 0.96)
                            EnemyData.Speed = -EnemyData.Speed;
                        SpriteRenderer.material = Confuse;
                    } else {
                        EnemyData.Speed = stdSpeed;
                        SpriteRenderer.material = Default;
                    }
                    break;
                case ShortRangeAttackElementEffect.poison:
                    if (poisonTimer > Mathf.Epsilon) {
                        poisonTimer -= Time.deltaTime;
                        if (poisonPause <= 0) {
                            poisonPause = poisonPauseStand;
                            Health = Health - AbilityData.poisonDamage >= 0 ? Health - AbilityData.poisonDamage : 0;
                            HealthSlider.value = 1.0f * Health / EnemyData.Healthmax;
                        }
                        SpriteRenderer.material = Poison;
                        poisonPause -= Time.deltaTime;
                    } else {
                        SpriteRenderer.material = Default;
                    }
                    break;
            }
            if (isHit) {
                EnemyRigidbody.velocity = new Vector2(-RepelDirection * EnemyData.RepelSpeed, EnemyRigidbody.velocity.y);
                HitTimer += Time.deltaTime;
            } else
                HitTimer = 0;
            if (HitTimer > 1.5f) {
                isHit = false;
                HitTimer = 0;
            }
            if (!isHit && Health == 0) {
                Death = true;
                if (!isBoss)
                    Anim.SetTrigger("Death");
                else
                    BossAnim.SetTrigger("Death");
            }
        } else {
            buffer.fillAmount = Mathf.Lerp(BossController.BossHealthBar.value, buffer.fillAmount, 0.9f);
        }
    }
    public void TakeDamage(int damage, int direction, float StaggerTime, bool ElementEffectEnabled) {
        if (!isBoss) {
            if (!Death) {
                Health = Health - damage >= 0 ? Health - damage : 0;
                HealthSlider.value = 1.0f * Health / EnemyData.Healthmax;
                RepelDirection = direction;
                StaggerTimer = StaggerTime;
                if (!attacking) {
                    isHit = true;
                    Anim.SetTrigger("Hit");
                    StrikeEffectAnim.SetTrigger("Hit");
                }
                if (ElementEffectEnabled)
                    switch (AbilityInst.Instance.shortRangeAttackElementEffect) {
                        case ShortRangeAttackElementEffect.decelerate:
                            decelerateTimer = AbilityData.decelerateTime;
                            stdSpeed = EnemyData.Speed;
                            break;
                        case ShortRangeAttackElementEffect.stun:
                            stunTimer = AbilityData.stunTime;
                            break;
                        case ShortRangeAttackElementEffect.confuse:
                            confuseTimer = AbilityData.confuseTime;
                            break;
                        case ShortRangeAttackElementEffect.poison:
                            poisonTimer = AbilityData.poisonTime;
                            break;
                    }
            }
        } else {
            if (!BossController.Death) {
                BossController.Health = BossController.Health - damage >= 0 ? BossController.Health - damage : 0;
                BossController.BossHealthBar.value = 1.0f * BossController.Health / BossData.Healthmax;
                if (!BossController.Attacking) {
                    BossController.isHit = true;
                    BossAnim.SetInteger("state",13);
                    StrikeEffectAnim.SetTrigger("Hit");
                }
            }
        }
    }
    public void HitOver() {
        isHit = false;
    }
}
