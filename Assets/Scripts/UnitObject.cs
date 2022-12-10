using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _maxEnergy;
    public List<Attack> attacks = new List<Attack>();
    [SerializeField] protected Animator animator;
    public UnitAnimWrapper animWrapper;

    public float speed = 5;
    public int team;
    public bool isBlocking = false;
    public float knockbackResistance = 1;
    public int Health 
    {
        get 
        { 
            return health; 
        }
        protected set
        {
            if (isInvincible) return;
            health = value;
        }
    }

    protected int health
    {
        get; private set;
    }

    public bool isInvincible = false;

    public int energy { get; protected set; }

    public bool CanMove
    {
        get
        {
            return blockTime <= 0 && stunTime <= 0 && !inKnockback && danceTime <= 0 && charmTime <= 0 && 
                (MenuManager.Instance == null || MenuManager.Instance.gameRunning) && (DialogueManager.Instance == null || !DialogueManager.Instance.isInDialogue);
        }
    }

    public bool CanAttack
    {
        get
        {
            return blockTime <= 0 && danceTime <= 0 && !inKnockback && danceTime <= 0 && charmTime <= 0 &&
                (MenuManager.Instance == null || MenuManager.Instance.gameRunning) && (DialogueManager.Instance == null || !DialogueManager.Instance.isInDialogue);
        }
    }

    [HideInInspector] public float blockTime;
    [HideInInspector] public float stunTime;
    [HideInInspector] public float danceTime;
    [HideInInspector] public float charmTime;
    [HideInInspector] public float weakened;
    [SerializeField] protected List<AudioClip> death = new List<AudioClip>();
    bool inKnockback;
    public Rigidbody rb;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        health = _maxHealth;
        energy = 0;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (blockTime > 0)
        {
            blockTime -= Time.deltaTime;
        }
        if (stunTime > 0)
        {
            Stun(-Time.deltaTime);
        }
        if (danceTime > 0)
        {
            Dance(-Time.deltaTime);
        }
        if(charmTime > 0)
        {
            Charm(-Time.deltaTime);
        }
        if (weakened > 0)
        {
            Weaken(-Time.deltaTime);
        }
        rb.velocity = Vector3.zero;
    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void CastAttack(int index, float multiplier = 1, string anim = "", float? stepAheadOverride = null, bool instant = false)
    {
        if (attacks[index].CanCast(this))
        {
            if(anim != "")
            {
                animator.SetTrigger(anim);
                if (instant)
                {
                    attacks[index].Cast(this, multiplier * (weakened > 0 ? 0.75f : 1), stepAheadOverride);
                }
                else
                {
                    animWrapper.multiplier = multiplier * (weakened > 0 ? 0.75f : 1);
                    animWrapper.stepAheadOverride = stepAheadOverride;
                }
            }
            else
            {
                attacks[index].Cast(this, multiplier * (weakened > 0 ? 0.75f : 1), stepAheadOverride);
            }
        }
    }

    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
        if(health <= 0)
        {
            animator.SetBool("Die", true);
            LevelManager.PlayClipAtPoint(death[Random.Range(0, death.Count)], transform.position, 1.0f, LevelManager.effectMixer);
            stunTime += 4.1f;
        }
    }

    public virtual void Heal(int amount)
    {
        health += amount;
        if(health > _maxHealth)
        {
            health = _maxHealth;
        }
    }

    public virtual void IncreaseEnergy(int amount)
    {
        energy += amount;
        if(energy > _maxEnergy)
        {
            energy = _maxEnergy;
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void StepAhead(float amount)
    {
        Vector3 start = rb.position;
        LeanTween.value(gameObject, 0, 1, 0.1f).setEase(LeanTweenType.easeOutCubic)
            .setOnUpdate((v) =>
            {
                rb.MovePosition(Vector3.Lerp(start, start + transform.right * amount, v));
            });
    }

    public void Knockback(Vector3 dir, float amount, float duration = 0.1f)
    {
        if (isInvincible)
        {
            return;
        }
        Vector3 start = rb.position;
        amount = amount / knockbackResistance;
        LeanTween.value(gameObject, 0, 1, duration).setEase(LeanTweenType.easeOutCubic)
            .setOnUpdate((v) =>
            {
                rb.MovePosition(Vector3.Lerp(start, start + dir.normalized * amount, v));
            }).setOnStart(() => inKnockback = true).setOnComplete(() => inKnockback = false);
    }

    public void Stun(float amount)
    {
        stunTime += amount;
        if(stunTime <= 0)
        {

        }
        else
        {

        }
    }

    public void Dance(float amount)
    {
        danceTime += amount;
        if (danceTime <= 0)
        {

        }
        else
        {

        }
    }

    public void Charm(float amount)
    {
        charmTime += amount;
        if(charmTime <= 0)
        {

        }
        else
        {

        }
    }

    public void Weaken(float amount)
    {
        weakened += amount;
        if(weakened <= 0)
        {

        }
        else
        {

        }
    }
}
