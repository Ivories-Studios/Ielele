using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _maxEnergy;
    [SerializeField] protected List<Attack> attacks = new List<Attack>();
    [SerializeField] protected Animator animator;

    public float speed = 5;
    public int team;
    public bool isBlocking = false;
    public int Health 
    {
        get 
        { 
            return health; 
        }
        protected set
        {
            if (isInvicible) return;
            health = value;
        }
    }

    protected int health
    {
        get; private set;
    }

    public bool isInvicible = false;

    public int energy { get; protected set; }

    public bool CanMove
    {
        get
        {
            return blockTime <= 0 && stunTime <= 0 && !inKnockback && danceTime <= 0 && charmTime <= 0 && MenuManager.Instance.gameRunning && !DialogueManager.Instance.isInDialogue;
        }
    }

    public bool CanAttack
    {
        get
        {
            return blockTime <= 0 && danceTime <= 0 && !inKnockback && danceTime <= 0 && charmTime <= 0 && MenuManager.Instance.gameRunning && !DialogueManager.Instance.isInDialogue;
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

    public virtual void CastAttack(int index, float multiplier = 1, string anim = "")
    {
        if (attacks[index].CanCast(this))
        {
            if(anim != "")
            {
                animator.SetTrigger(anim);
            }
            attacks[index].Cast(this, multiplier * (weakened > 0 ? 0.75f : 1));
        }
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if(health < 0)
        {
            //animator.SetTrigger("Die");
            AudioSource.PlayClipAtPoint(death[Random.Range(0, death.Count)], transform.position);
            Destroy(gameObject);
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

    public void Knockback(Vector3 dir, float amount, float duration = 0.2f)
    {
        Vector3 start = rb.position;
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
