using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _jumpForce = 1000;
    [SerializeField]
    private float _gravity = -2;
    [SerializeField]
    private float _holdAcceleration = 20;
    [SerializeField]
    private float _maxHoldTime = 0.5f;
    private float _currentHoldTime = 0f;
    private float _yAxisMovement = 0;

    [SerializeField]
    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xSpeed = Mathf.Sin(Input.GetAxis("horizontal") * Mathf.PI / 2) * Mathf.Cos(Input.GetAxis("vertical") * Mathf.PI / 4);
        float zSpeed = Mathf.Sin(Input.GetAxis("vertical") * Mathf.PI / 2) * Mathf.Cos(Input.GetAxis("horizontal") * Mathf.PI / 4);
        Vector3 movementVector = new(xSpeed, 0, zSpeed);
        movementVector *= _speed;

        if (_characterController.isGrounded)
        {
            _yAxisMovement = 0;
            _currentHoldTime = 0;
            if (Input.GetAxis("jump") > 0)
            {
                _yAxisMovement += _jumpForce;
            }
        }
        else
        {
            if (Input.GetAxisRaw("jump") > 0 && _currentHoldTime < _maxHoldTime)
            { _currentHoldTime += Time.deltaTime;
                _yAxisMovement += _holdAcceleration;
            }
            _yAxisMovement += (_gravity);
        }

        movementVector.y = _yAxisMovement;
        movementVector *= Time.deltaTime;

        _characterController.Move(movementVector);
    }
}
