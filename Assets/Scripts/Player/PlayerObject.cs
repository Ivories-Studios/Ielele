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
    [SerializeField] GameObject errorNoEnergyText = null;

    QueuedAction _queuedAction;
    Coroutine _queuedCoroutineAction;
    string [] punchAnimStrings = { "Punch0", "Punch1", "Punch2" };

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

    float regen = 0;

    public override void Awake()
    {
        base.Awake();
        Instance = this;

    }


    float errorTimer = 0;

    bool stoppedBlockingThisFrame = false;

    private bool holdingBlock = false;
    
    // Update is called once per frame
    public override void Update()
    {
        punchTimer -= Time.deltaTime;
        if (punchTimer <= 0)
        {
            if (currentPunchId != 0)
            {
                currentPunchId = 0;
                animator.SetTrigger("EndPunch");
            }
            animator.ResetTrigger("Punch1");
            animator.ResetTrigger("Punch2");

        }
        stoppedBlockingThisFrame = false;

        if (!isBlocking)
        {
            animator.ResetTrigger("StopBlock");
        }
        if (!holdingBlock && isBlocking)
        {
            stoppedBlockingThisFrame = true;
            isBlocking = false;
            blockTime = 0;
            animator.SetTrigger("StopBlock");
            animator.ResetTrigger("SpecialBlock");
        }

        base.Update();


        if (_queuedAction != null && attacks[_queuedAction.attackIndex].CanCast(this) && CanAttack && !stoppedBlockingThisFrame)
        {
            if (_queuedAction.direction == QueuedDirection.left && GetComponent<PlayerMovement>().isLookingRight)
            {
                GetComponent<PlayerMovement>().TurnRight(false);
            }
            if (_queuedAction.direction == QueuedDirection.right && !GetComponent<PlayerMovement>().isLookingRight)
            {
                GetComponent<PlayerMovement>().TurnRight(true);
            }

            switch (_queuedAction.animName)
            {
                case "Punch":
                    Punch();
                    break;
                default:
                    CastAttack(_queuedAction.attackIndex, 1, _queuedAction.animName);
                    break;
            }
            _queuedAction = null;
        }


        errorTimer -= Time.deltaTime;
        if (errorTimer < 0)
        {
            if(errorNoEnergyText!=null)
                errorNoEnergyText.SetActive(false);
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
        regen += Time.deltaTime;
        if(regen >= 10)
        {
            Heal(1);
            regen = 0;
        }
    }

    float punchTimer;
    int currentPunchId = 0;
    void Punch()
    {
        animator.ResetTrigger("EndPunch");
        punchTimer = 0.7f;
        string animationName = punchAnimStrings[currentPunchId];
        switch (animationName)
        {
            case "Punch0":
                animator.ResetTrigger("Punch1");
                break;
            case "Punch1":
                animator.ResetTrigger("Punch2");
                break;
        }

        CastAttack(0, _comboModifier * (1 + wellBuffs * 0.25f), animationName, currentPunchId == 2 ? (float?)null : 0.4f);

        currentPunchId++;
        if (currentPunchId % 3 == 0) currentPunchId = 0;

        if (currentPunchId == 0)
        {
            blockTime += blockTime / 2;
        }
    }

    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (CanAttack)
            {
                Punch();
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
                CastAttack(1, _comboModifier * (1 + wellBuffs * 0.25f), "Kick");
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
                _queuedAction.animName = "Kick";
            }
        }
    }

    public void OnAttack3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (CanAttack)
            {
                CastAttack(2, _comboModifier * (1 + wellBuffs * 0.25f), "Axe");
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
                _queuedAction.animName = "Axe";
            }
        }
    }

    public void OnAttack4(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            holdingBlock = true;
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
        else if (context.canceled)
        {
            holdingBlock = false;
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
        _comboModifier = 1;//comboFunction.Evaluate(_combo / 100.0f) * 5;
        _currentComboExpireTime = 4;
        _timeSinceLastCombo = 0;
        FloatingText.CreateFloatingTextCombo(_combo, transform.position + transform.right + new Vector3(0, 1f, 0));
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
        yield return new WaitForSeconds(2);
        Die();
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