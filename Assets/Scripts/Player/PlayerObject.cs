using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObject : UnitObject
{
    QueuedAction _queuedAction;
    Coroutine _queuedCoroutineAction;

    float _combo;
    float _currentComboExpireTime;
    float _timeSinceLastCombo;
    [HideInInspector] public int comboInARow;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(_queuedAction != null && attacks[_queuedAction.attackIndex].CanCast(this))
        {
            CastAttack(_queuedAction.attackIndex);
            _queuedAction = null;
        }
        _timeSinceLastCombo += Time.deltaTime;
        if(_currentComboExpireTime <= 0)
        {
            _combo = 1;
            comboInARow = 0;
        }
        else
        {
            _currentComboExpireTime -= Time.deltaTime;
        }
    }

    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (CanAttack)
            {
                CastAttack(0, _combo);
            }
            else
            {
                if(_queuedCoroutineAction != null)
                {
                    StopCoroutine(_queuedCoroutineAction);
                }
                _queuedCoroutineAction = StartCoroutine(AddActionToQueue(0));
            }
        }
    }

    public void OnAttack2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (CanAttack)
            {
                CastAttack(1, _combo);
            }
            else
            {
                if (_queuedCoroutineAction != null)
                {
                    StopCoroutine(_queuedCoroutineAction);
                }
                _queuedCoroutineAction = StartCoroutine(AddActionToQueue(1));
            }
        }
    }

    public void OnAttack3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (CanAttack)
            {
                CastAttack(2, _combo);
            }
            else
            {
                if (_queuedCoroutineAction != null)
                {
                    StopCoroutine(_queuedCoroutineAction);
                }
                _queuedCoroutineAction = StartCoroutine(AddActionToQueue(2));
            }
        }
    }

    public void OnAttack4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (CanAttack)
            {
                CastAttack(3, _combo);
            }
            else
            {
                if (_queuedCoroutineAction != null)
                {
                    StopCoroutine(_queuedCoroutineAction);
                }
                _queuedCoroutineAction = StartCoroutine(AddActionToQueue(3));
            }
        }
    }

    IEnumerator AddActionToQueue(int index)
    {
        QueuedAction action = new QueuedAction() { attackIndex = index };
        _queuedAction = action;
        yield return new WaitForSeconds(2f);
        _queuedAction = null;
    }

    public void IncreaseCombo(float amount)
    {
        comboInARow++;
        if(comboInARow >= 3)
        {
            comboInARow = 0;
            amount *= 2;
        }
        if (_timeSinceLastCombo < 0.5f)
        {
            amount *= 2;
        }
        _combo += amount;
        _currentComboExpireTime = 4;
        _timeSinceLastCombo = 0;
    }

    public void MaintainCombo()
    {
        _currentComboExpireTime = 4;
        _timeSinceLastCombo = 0;
    }
}

class QueuedAction
{
    public int attackIndex;
}