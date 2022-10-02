using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IalaOlog : Attack
{
    Rigidbody2D rb;

    public override void Cast(UnitObject unit, float multiplier = 1)
    {
        Attack a = Instantiate(gameObject, unit.transform.position, unit.transform.rotation).GetComponent<Attack>();
        a._caster = unit;
        Vector3 startPos = a._caster.transform.position;
        LeanTween.value(a._caster.gameObject, 0, 1, 0.3f).setEase(LeanTweenType.easeInExpo)
            .setOnUpdate((v) =>
            {
                Vector3 target = PlayerObject.Instance.transform.position + (PlayerObject.Instance.transform.position - a._caster.transform.position).normalized * 2;
                unit.rb.MovePosition(Vector3.Lerp(startPos, target, v));
            });
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.TryGetComponent(out UnitObject target))
        {
            if (target.team != _caster.team)
            {
                if (Vector3.Distance(target.transform.position, transform.position) > 3)
                {
                    return;
                }
                target.TakeDamage(_power);
                target.Weaken(5);
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
        rb.MovePosition(_caster.transform.position);
    }
}
