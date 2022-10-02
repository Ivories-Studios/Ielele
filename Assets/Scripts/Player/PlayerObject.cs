using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class PlayerObject : UnitObject
{
    public static PlayerObject Instance;
    [SerializeField] AnimationCurve comboFunction;

    QueuedAction _queuedAction;
    Coroutine _queuedCoroutineAction;

    int _combo;
    float _comboModifier;
    float _currentComboExpireTime;
    float _timeSinceLastCombo;
    [HideInInspector] public int comboInARow;

    public List<InteractableObject> surroundingInteractableObjects = new List<InteractableObject>();

    public int wellBuffs = 0;

    [SerializeField] Volume volume;
    [SerializeField] List<AudioClip> hurtAudio = new List<AudioClip>();
    [SerializeField] List<AudioClip> shieldBlockAudio = new List<AudioClip>();
    [SerializeField] AudioSource audioSource;

    public override void Awake()
    {
        base.Awake();
        Instance = this;

    }


    bool stoppedBlockingThisFrame = false;
    // Update is called once per frame
    public override void Update()
    {
        stoppedBlockingThisFrame = false;

        if (!isBlocking)
        {
            animator.ResetTrigger("StopBlock");
        }
        if (!Input.GetKey(KeyCode.DownArrow) && isBlocking)
        {
            stoppedBlockingThisFrame = true;
            isBlocking = false;
            blockTime = 0;
            animator.SetTrigger("StopBlock");
            animator.ResetTrigger("SpecialBlock");
        }

        base.Update();

        //INPUT QUEUEING
        if(_queuedAction != null && attacks[_queuedAction.attackIndex].CanCast(this) && CanAttack && !stoppedBlockingThisFrame)
        {
            if(_queuedAction.direction == QueuedDirection.left && GetComponent<PlayerMovement>().isLookingRight)
            {
                GetComponent<PlayerMovement>().TurnRight(false);
            }
            if (_queuedAction.direction == QueuedDirection.right && !GetComponent<PlayerMovement>().isLookingRight)
            {
                GetComponent<PlayerMovement>().TurnRight(true);
            }

            CastAttack(_queuedAction.attackIndex,1, _queuedAction.animName);
            _queuedAction = null;
        }


        //COMBOS
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
        if (context.performed)
        {
            if (CanAttack)
            {
                CastAttack(0, _comboModifier * (1 + wellBuffs * 0.25f), "Punch");
            }
            else
            {
                if(_queuedCoroutineAction != null)
                {
                    StopCoroutine(_queuedCoroutineAction);
                }
                
                

                _queuedCoroutineAction = StartCoroutine(AddActionToQueue(0));
                if (Input.GetKey(KeyCode.A)) _queuedAction.direction = QueuedDirection.left;
                if (Input.GetKey(KeyCode.D)) _queuedAction.direction = QueuedDirection.right;
                _queuedAction.animName = "Punch";
            }
        }
    }

    public void OnAttack2(InputAction.CallbackContext context)
    {
        if (context.performed)
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
                if (Input.GetKey(KeyCode.A)) _queuedAction.direction = QueuedDirection.left;
                if (Input.GetKey(KeyCode.D)) _queuedAction.direction = QueuedDirection.right;
            }
        }
    }

    public void OnAttack3(InputAction.CallbackContext context)
    {
        if (context.performed)
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
                if (Input.GetKey(KeyCode.A)) _queuedAction.direction = QueuedDirection.left;
                if (Input.GetKey(KeyCode.D)) _queuedAction.direction = QueuedDirection.right;
            }
        }
    }

    public void OnAttack4(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (CanAttack)
            {
                CastAttack(3, _comboModifier * (1 + wellBuffs * 0.25f), "Block");
            }
            else
            {
                if (_queuedCoroutineAction != null)
                {
                    StopCoroutine(_queuedCoroutineAction);
                }

                _queuedCoroutineAction = StartCoroutine(AddActionToQueue(3));
                if (Input.GetKey(KeyCode.A)) _queuedAction.direction = QueuedDirection.left;
                if (Input.GetKey(KeyCode.D)) _queuedAction.direction = QueuedDirection.right;
                _queuedAction.animName = "Block";
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
        yield return new WaitForSeconds(0.5f);
        _queuedAction = null;
    }

    public void IncreaseCombo(int amount)
    {
        //comboInARow++;
        //if(comboInARow >= 3)
        //{
        //    comboInARow = 0;
        //    amount *= 2;
        //}
        //if (_timeSinceLastCombo < 0.5f)
        //{
        //    amount *= 2;
        //}
        _combo += 1;
        if(_combo > 100)
        {
            _combo = 100;
        }
        _comboModifier = comboFunction.Evaluate(_combo / 100.0f) * 5;
        _currentComboExpireTime = 4;
        _timeSinceLastCombo = 0;
        FloatingText.CreateFloatingTextCombo(_combo, transform.position + new Vector3(0, 1, 0));
    }

    public void MaintainCombo()
    {
        _currentComboExpireTime = 2.7f;
        _timeSinceLastCombo = 0;
    }

    public override void TakeDamage(int amount)
    {
        if (isBlocking)
        {
            audioSource.PlayOneShot(shieldBlockAudio[Random.Range(0, shieldBlockAudio.Count)]);

            animator.SetTrigger("SpecialBlock");
            if(Random.Range(0,100) < 30)
                energy += 1;

            return;
        }

        amount = (int)(amount * (1 + wellBuffs * 0.5f));
        base.TakeDamage(amount);
        CanvasManager.Instance.SetHealth(health);
        audioSource.PlayOneShot(hurtAudio[Random.Range(0, hurtAudio.Count)]);
        if(health <= 0)
        {
            StartCoroutine(ActivatePostProcess());
            animator.SetTrigger("Die");
            AudioSource.PlayClipAtPoint(death[Random.Range(0, death.Count)], transform.position);
        }
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        CanvasManager.Instance.SetHealth(health);
    }

    public override void IncreaseEnergy(int amount)
    {
        base.IncreaseEnergy(amount);
        energy = Mathf.Clamp(energy, 0, _maxEnergy);
        CanvasManager.Instance.SetEnergy(energy);
    }

    IEnumerator ActivatePostProcess()
    {
        NTSCEncode_RLPRO ntsc;
        if(volume.profile.TryGet(out ntsc))
        {
            ntsc.active = true;
        }
        yield return new WaitForSeconds(1f);
        Jitter_RLPRO jitter;
        if(volume.profile.TryGet(out jitter))
        {
            jitter.active = true;
        }
        yield return new WaitForSeconds(1f);
        Bleed_RLPRO_HDRP bleed;
        if(volume.profile.TryGet(out bleed))
        {
            bleed.active = true;
        }
    }

    public override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RollForHeal()
    {
        int randomNumber = Random.Range(0, 1000);
        if(randomNumber + _combo / 2 > 990)
        {
            Heal(1);
            CanvasManager.Instance.SetHealth(health);
        }
    }
}

class QueuedAction
{
    public int attackIndex;
    public string animName = "";
    public QueuedDirection direction;
}

enum QueuedDirection
{
    none,
    left,
    right
}