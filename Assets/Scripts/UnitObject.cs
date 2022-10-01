using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _maxEnergy;
    [SerializeField] protected List<Attack> attacks = new List<Attack>();
    public int team;
    public int health { get; protected set; }
    public int energy { get; protected set; }

    public bool CanMove
    {
        get
        {
            return blockTime <= 0 && stunTime <= 0 && !inKnockback && danceTime <= 0;
        }
    }

    public bool CanAttack
    {
        get
        {
            return blockTime <= 0 && danceTime <= 0 && !inKnockback && danceTime<= 0;
        }
    }

    [HideInInspector] public float blockTime;
    [HideInInspector] public float stunTime;
    [HideInInspector] public float danceTime;
    bool inKnockback;
    protected Rigidbody rb;

    private void Awake()
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
        rb.velocity = Vector3.zero;
    }

    public virtual void CastAttack(int index, float multiplier = 1)
    {
        if (attacks[index].CanCast(this))
        {
            attacks[index].Cast(this, multiplier);
        }
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if(health < 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void StepAhead(float amount)
    {
        LeanTween.value(gameObject, 0, amount, 0.3f).setEase(LeanTweenType.easeInExpo)
            .setOnUpdate((v) =>
            {
                rb.MovePosition(rb.position + Time.deltaTime * v * transform.right);
            });
    }

    public void Knockback(Vector3 dir, float amount)
    {
        LeanTween.value(gameObject, 0, amount, 0.5f).setEase(LeanTweenType.easeInCubic)
            .setOnUpdate((v) =>
            {
                rb.MovePosition(rb.position + Time.deltaTime * v * dir);
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
}
