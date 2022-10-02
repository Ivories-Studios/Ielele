using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    public override void Cast(UnitObject unit, float multiplier)
    {
        base.Cast(unit, multiplier);

        Attack a = Instantiate(gameObject, unit.transform).GetComponent<Attack>();
        a._caster = unit;
        unit.blockTime = _blockTime;
        unit.StepAhead(_stepAhead);
        AudioSource.PlayClipAtPoint(audioClips2[Random.Range(0, audioClips2.Count)], a.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.transform.parent.TryGetComponent(out UnitObject target))
        {
            if (target.team != _caster.team)
            {
                if (Vector3.Distance(target.transform.position, transform.position) > 3)
                {
                    return;
                }
                target.TakeDamage(_power);
                //rget.blockTime = 0.5f;
            }
        }
    }
}
