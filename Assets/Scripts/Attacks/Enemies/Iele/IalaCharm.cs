using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IalaCharm : Attack
{
    [SerializeField] float speed;
    Rigidbody rb;
    Vector3 dir;

    public override void Cast(UnitObject unit, float multiplier = 1)
    {
        Attack a = Instantiate(gameObject, unit.transform.position, unit.transform.rotation).GetComponent<Attack>();
        a._caster = unit;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.TryGetComponent(out UnitObject target))
        {
            if (target.team != _caster.team)
            {
                target.TakeDamage(_power);
                target.Charm(1f);
                Vector3 knockbackDir = new Vector3(transform.position.x - target.transform.position.x > 0 ? -1 : 1, 0, 0);
                target.Knockback(-knockbackDir, _knockback, 1f);
                Destroy(gameObject);
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Start()
    {
        base.Start();
        dir = PlayerObject.Instance.transform.position - transform.position;
        dir.y = 0;
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * dir.normalized);
    }
}
