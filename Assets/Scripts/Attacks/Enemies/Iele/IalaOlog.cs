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
        Vector3 startPos = _caster.transform.position;
        LeanTween.value(_caster.gameObject, 0, 1, 0.3f).setEase(LeanTweenType.easeInExpo)
            .setOnUpdate((v) =>
            {
                Vector3 target = PlayerObject.Instance.transform.position + (PlayerObject.Instance.transform.position - _caster.transform.position).normalized * 2;
                rb.MovePosition(Vector3.Lerp(startPos, target, v));
            });
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out UnitObject target))
        {
            if (target.team != _caster.team)
            {
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
