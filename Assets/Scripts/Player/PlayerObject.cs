using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerObject : UnitObject
{
    public static PlayerObject Instance;
    [SerializeField] AnimationCurve comboFunction;

    QueuedAction _queuedAction;
    Coroutine _queuedCoroutineAction;

    float _combo;
    float _comboModifier;
    float _currentComboExpireTime;
    float _timeSinceLastCombo;
    [HideInInspector] public int comboInARow;

    public List<InteractableObject> surroundingInteractableObjects = new List<InteractableObject>();

    public int wellBuffs = 0;

    private void Awake()
    {
        Instance = this;
    }

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
            _comboModifier = 1;
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
                CastAttack(0, _comboModifier * (1 + wellBuffs * 0.25f));
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
                CastAttack(1, _comboModifier * (1 + wellBuffs * 0.25f));
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
                CastAttack(2, _comboModifier * (1 + wellBuffs * 0.25f));
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
                CastAttack(3, _comboModifier * (1 + wellBuffs * 0.25f));
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

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started && surroundingInteractableObjects.Count > 0)
        {
            surroundingInteractableObjects[0].Interact(this);
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
        if(_combo > 100)
        {
            _combo = 100;
        }
        _comboModifier = comboFunction.Evaluate(_combo / 100.0f) * 5;
        _currentComboExpireTime = 4;
        _timeSinceLastCombo = 0;
    }

    public void MaintainCombo()
    {
        _currentComboExpireTime = 4;
        _timeSinceLastCombo = 0;
    }

    public override void TakeDamage(int amount)
    {
        amount = (int)(amount * (1 + wellBuffs * 0.5f));
        base.TakeDamage(amount);
        CanvasManager.Instance.SetHealth(amount);
    }

    public void IncreaseEnergyLevel(int amount)
    {
        energy += amount;
        energy = Mathf.Clamp(energy, 0, _maxEnergy);
        CanvasManager.Instance.SetEnergy(energy);
    }

    public override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

class QueuedAction
{
    public int attackIndex;
}