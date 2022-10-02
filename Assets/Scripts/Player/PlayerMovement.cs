using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _acceleration = 1;
    float _currentSpeed;
    [SerializeField] CinemachineVirtualCamera _virtualCamera;

    [Space]
    [SerializeField] PlayerGraphicsTurnScript _graphicsTurnScript;

    Rigidbody _rigidbody;
    Vector2 _movementDelta;
    CinemachineFramingTransposer _composer;
    int _deadZoneId;
    PlayerObject _playerObject;
    bool isLookingRight = true;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _composer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        _playerObject = GetComponent<PlayerObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerObject.CanMove)
        {
            if (_movementDelta != Vector2.zero)
            {
                _audioSource.UnPause();
                _animator.SetBool("Moving", true);
            }
            else
            {
                _audioSource.Pause();
                _animator.SetBool("Moving", false);
            }

            _currentSpeed += Mathf.Max(Mathf.Abs(_movementDelta.x), Mathf.Abs(_movementDelta.y)) * Time.deltaTime * _acceleration;
            needsToMove = true;
            if (_movementDelta.x == 0 && _movementDelta.y == 0)
            {
                _currentSpeed = 0;
                needsToMove = false;
            }

            _currentSpeed = Mathf.Min(_speed, _currentSpeed);

            if (_movementDelta.x > 0)
            {
                if (!isLookingRight)
                {
                    //anim call TurnRight
                    TurnRight(true);
                }
            }
            else if (_movementDelta.x < 0)
            {
                if (isLookingRight)
                {
                    //anim call TurnRight
                    TurnRight(false);
                }
            }
        }
        else
        {
            _audioSource.Pause();
            _animator.SetBool("Moving", false);
        }

    }
    bool needsToMove = false;
    private void FixedUpdate()
    {
        if (_playerObject.CanMove)
        {
            if (needsToMove)
            {
                Vector3 targetPosition = _rigidbody.position + _currentSpeed * Time.fixedDeltaTime * new Vector3(_movementDelta.x, 0, _movementDelta.y * 1.768f);
                _rigidbody.MovePosition(targetPosition);
            }
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _movementDelta = context.ReadValue<Vector2>();
        if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.D)) _movementDelta = Vector2.zero;

        if (!_playerObject.CanMove)
        {
            return;
        }
        if (_movementDelta != Vector2.zero)
        {
            if (_movementDelta.x < 0)
            {
                _graphicsTurnScript.TurnLeft();
            }
            else if (_movementDelta.x > 0)
            {
                _graphicsTurnScript.TurnRight();
            }

            LeanTween.cancel(_deadZoneId);
            _composer.m_DeadZoneWidth = 0.3f;
        }
        else
        {
            _deadZoneId = LeanTween.value(gameObject, _composer.m_DeadZoneWidth, 0, 3)
                .setEase(LeanTweenType.easeOutExpo).setDelay(2)
                .setOnUpdate((v) =>
                {
                    _composer.m_DeadZoneWidth = v;
                }).uniqueId;
        }
    }

    public void TurnRight(bool right)
    {
        isLookingRight = right;
        transform.rotation = Quaternion.Euler(0, right ? 0 : 180, 0);
    }
}
