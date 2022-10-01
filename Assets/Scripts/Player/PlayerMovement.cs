using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] CinemachineVirtualCamera _virtualCamera;

    Rigidbody _rigidbody;
    Vector2 _movementDelta;
    CinemachineFramingTransposer _composer;
    int _deadZoneId;
    PlayerObject _playerObject;
    bool isLookingRight = true;

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
        
    }

    private void FixedUpdate()
    {
        if (_playerObject.CanMove)
        {
            Vector3 targetPosition = _rigidbody.position + _speed * Time.fixedDeltaTime * new Vector3(_movementDelta.x, 0, _movementDelta.y);
            _rigidbody.MovePosition(targetPosition);
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
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _movementDelta = context.ReadValue<Vector2>();
        if (!_playerObject.CanMove)
        {
            return;
        }
        if (_movementDelta != Vector2.zero)
        {
            LeanTween.cancel(_deadZoneId);
            _composer.m_DeadZoneWidth = 0.2f;
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
