using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeFall : Attack
{
    public override void Cast(UnitObject unit, float mutliplier)
    {
        Attack a = Instantiate(gameObject, unit.transform).GetComponent<Attack>();
        a._caster = unit;
        a._power *= (int)mutliplier;
        unit.blockTime = _blockTime;
        if (unit is PlayerObject _player)
        {
            if (_player.comboInARow == 2)
            {
                unit.blockTime += _blockTime;
            }
        }
        unit.StepAhead(_stepAhead);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out UnitObject target))
        {
            if (target.team != _caster.team)
            {
                if (_caster is PlayerObject _player)
                {
                    if (_player.comboInARow == 2)
                    {
                        _power = (int)(_power * 0.5f);
                    }
                }
                target.TakeDamage(_power);
                Vector3 knockbackDir = new Vector3(transform.position.x - target.transform.position.x > 0 ? -1 : 1, 0, 0);
                target.Knockback(knockbackDir, _knockback);
                if (_caster is PlayerObject player)
                {
                    player.IncreaseCombo(_comboIncrease);
                    player.IncreaseEnergyLevel(1);
                }
            }
        }
    }
}
