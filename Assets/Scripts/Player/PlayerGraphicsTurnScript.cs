using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGraphicsTurnScript : MonoBehaviour
{
    public bool Right { get => right; }
    private bool right = true;

    [SerializeField] float _turnSpeed = 100;
    [SerializeField] Transform _graphics;

    private Transform _parent;
    private Vector3 _offset;

    private float _rotTarget = 0.0f;

    void Start()
    {
        _offset = transform.localPosition;
        _parent = transform.parent;
        transform.parent = null;
    }

    void LateUpdate()
    {
        transform.position = _parent.position + _offset;

        _graphics.eulerAngles = new Vector3(_graphics.eulerAngles.x, _graphics.eulerAngles.y,
                Mathf.LerpAngle(_graphics.eulerAngles.z, _rotTarget, _turnSpeed * Time.deltaTime)
            );
    }

    public void TurnRight()
    {
        _rotTarget = 0.0f;
        right = true;
    }

    public void TurnLeft()
    {
        _rotTarget = -180.0f;
        right = false;
    }
}
