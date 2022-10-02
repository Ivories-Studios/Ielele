using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IalaProjectile : Attack
{
    [SerializeField] float speed;
    Rigidbody rb;
    public Vector3 dir;

    public override void Cast(UnitObject unit, float multiplier = 1)
    {
        Attack a = Instantiate(gameObject, unit.transform.position, unit.transform.rotation).GetComponent<Attack>();
        a._caster = unit;
        ((IalaProjectile)a).dir = new Vector3((PlayerObject.Instance.transform.position.x - a.transform.position.x > 0) ? 1 : -1, 0, 0);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.TryGetComponent(out UnitObject target))
        {
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
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * dir));
    }
}
