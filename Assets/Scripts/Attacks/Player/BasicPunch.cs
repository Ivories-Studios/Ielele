using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPunch : Attack
{
    public override void Cast(UnitObject unit, float multiplier, float? stepAheadOverride = null)
    {
        base.Cast(unit, multiplier);

        Attack a = Instantiate(gameObject, unit.transform).GetComponent<Attack>();
        a._caster = unit;
        a._power *= (int)multiplier;
        unit.blockTime = _blockTime;
        if (unit is PlayerObject _player)
        {
            if (_player.comboInARow == 2)
            {
                unit.blockTime += _blockTime;
            }
        }
        if (stepAheadOverride != null)
        {
            unit.StepAhead(stepAheadOverride.Value);
        }
        else
        {
            unit.StepAhead(_stepAhead);
        }
        LevelManager.PlayClipAtPoint(audioClips2[Random.Range(0, audioClips2.Count)], a.transform.position, group: LevelManager.effectMixer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.TryGetComponent(out UnitObject target))
        {
            if (target.team != _caster.team)
            {
                if (Vector3.Distance(target.transform.position, transform.position) > 3)
                {
                    return;
                }
                if (_caster is PlayerObject _player)
                {
                    if(_player.comboInARow == 2)
                    {
                        _power = (int)(_power * 0.5f);
                    }
                    _player.RollForHeal();
                }
                target.TakeDamage(_power);
                Vector3 knockbackDir = new Vector3(transform.position.x - target.transform.position.x > 0 ? -1 : 1, 0, 0);
                target.Knockback(knockbackDir, _knockback);
                _caster.IncreaseEnergy(1);
                if (_caster is PlayerObject player && !grantedCombo)
                {
                    grantedCombo = true;
                    player.IncreaseCombo(_comboIncrease);
                }
                LevelManager.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Count)], transform.position, group: LevelManager.effectMixer);
            }
        }
    }
}
