using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : Attack
{
    public override void Cast(UnitObject unit, float mutliplier)
    {
        base.Cast(unit, mutliplier);

        Attack a = Instantiate(gameObject, unit.transform).GetComponent<Attack>();
        a._caster = unit;
        a._power *= (int)mutliplier;
        unit.blockTime = _blockTime;
        unit.StepAhead(_stepAhead);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out Attack enemyAttack))
        {
            if (enemyAttack._caster.team != _caster.team)
            {
                if (_caster is PlayerObject _player)
                {
                    if (_player.comboInARow == 2)
                    {
                        _power = (int)(_power * 0.5f);
                    }
                    _player.RollForHeal();
                }
                enemyAttack._caster.TakeDamage(_power);
                Vector3 knockbackDir = new Vector3(transform.position.x - enemyAttack._caster.transform.position.x > 0 ? -1 : 1, 0, 0);
                enemyAttack._caster.Knockback(knockbackDir, _knockback);
                if (_caster is PlayerObject player)
                {
                    player.MaintainCombo();
                }
            }
        }
    }
}
