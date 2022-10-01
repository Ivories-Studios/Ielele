using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] CinemachineVirtualCamera _virtualCamera;

    [Space]
    [SerializeField] PlayerGraphicsTurnScript _graphicsTurnScript;

    Rigidbody _rigidbody;
    Vector2 _movementDelta;
    CinemachineFramingTransposer _composer;
    int _deadZoneWidthCoroutine;
    int _deadZoneId;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _composer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
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
        Vector3 targetPosition = _rigidbody.position + new Vector3(_movementDelta.x, 0, _movementDelta.y) * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(targetPosition);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _movementDelta = context.ReadValue<Vector2>();
        if(_movementDelta != Vector2.zero)
        {
            LeanTween.cancel(_deadZoneId);
            _composer.m_DeadZoneWidth = 0.2f;

            // Graphics turning
            if (_movementDelta.x > 0)
            {
                _graphicsTurnScript.TurnLeft();
            }
            else if (_movementDelta.x < 0)
            {
                _graphicsTurnScript.TurnRight();
            }
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

    public void OnAttack1(InputAction.CallbackContext context)
    {

    }

    public void OnAttack2(InputAction.CallbackContext context)
    {

    }

    public void OnAttack3(InputAction.CallbackContext context)
    {

    }

    public void OnAttack4(InputAction.CallbackContext context)
    {

    }
}
