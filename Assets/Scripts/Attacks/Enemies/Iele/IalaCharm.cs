using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IalaCharm : Attack
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    Vector3 dir;

    public override void Cast(UnitObject unit, float multiplier = 1)
    {
        Attack a = Instantiate(gameObject, unit.transform.position, unit.transform.rotation).GetComponent<Attack>();
        a._caster = unit;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out UnitObject target))
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
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Start()
    {
        base.Start();
        dir = PlayerObject.Instance.transform.position - transform.position;
        dir.y = 0;
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * (Vector2)dir);
    }
}
