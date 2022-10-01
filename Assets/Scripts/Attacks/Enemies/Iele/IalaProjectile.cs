using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IalaProjectile : Attack
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    public Vector3 dir;

    public override void Cast(UnitObject unit, float multiplier = 1)
    {
        Attack a = Instantiate(gameObject, unit.transform.position, unit.transform.rotation).GetComponent<Attack>();
        a._caster = unit;
        ((IalaProjectile)a).dir = new Vector3((PlayerObject.Instance.transform.position.x - a.transform.position.x > 0) ? 1 : -1, 0, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponentInParent<UnitObject>())
        {
            UnitObject target = collision.transform.GetComponentInParent<UnitObject>();
            if (target.team != _caster.team)
            {
                target.TakeDamage(_power);
                Vector3 knockbackDir = new Vector3((transform.position.x - target.transform.position.x > 0) ? -1 : 1, 0, 0);
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
        rb.MovePosition(rb.position + (Vector2)(speed * Time.fixedDeltaTime * dir));
    }
}
