using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IalaProjectile : Attack
{
    [SerializeField] float speed;
    Rigidbody2D rb;

    public override void Cast(UnitObject unit, float multiplier = 1)
    {
        Attack a = Instantiate(gameObject, unit.transform.position, unit.transform.rotation).GetComponent<Attack>();
        a._caster = unit;
        a._power *= (int)multiplier;
        unit.blockTime = _blockTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out UnitObject target))
        {
            if (target.team != _caster.team)
            {
                target.TakeDamage(_power);
                Vector3 knockbackDir = new Vector3(transform.position.x - target.transform.position.x > 0 ? -1 : 1, 0, 0);
                target.Knockback(knockbackDir, _knockback);
                Destroy(gameObject);
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * (Vector2)transform.right);
    }
}
