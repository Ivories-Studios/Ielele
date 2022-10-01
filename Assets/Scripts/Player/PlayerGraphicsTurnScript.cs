using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGraphicsTurnScript : MonoBehaviour
{
    [SerializeField] float _turnSpeed = 100;

    private Transform _parent;
    private Vector3 _offset;

    private float _rotTarget = 0;

    void Start()
    {
        _offset = transform.localPosition;
        _parent = transform.parent;
        transform.parent = null;
    }

    void LateUpdate()
    {
        transform.position = _parent.position + _offset;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
                Mathf.LerpAngle(transform.eulerAngles.z, _rotTarget, _turnSpeed * Time.deltaTime)
            );
    }

    public void TurnRight()
    {
        _rotTarget = 0.0f;
    }

    public void TurnLeft()
    {
        _rotTarget = -180.0f;
    }
}
