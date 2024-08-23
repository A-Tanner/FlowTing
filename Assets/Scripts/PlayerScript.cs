using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20;
    [SerializeField]
    private float _jumpForce = 17;
    [SerializeField]
    private float _gravity = -1f;
    [SerializeField]
    private float _holdAcceleration = 0.6f;
    [SerializeField]
    private float _maxHoldTime = 0.3f;
    private float _currentHoldTime = 0f;
    private float _yAxisMovement = 0;
    private float _mercyCooldown = 0.15f;
    private float _mercyTime = 0f;
    private bool _canJump = false;

    [SerializeField]
    private AudioClip _deathSound;
    [SerializeField]
    private AudioClip _jumpSound;

    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -18)
        {
            Die();
        }

        float xSpeed = Mathf.Sin(Input.GetAxis("horizontal") * Mathf.PI / 2) * Mathf.Cos(Input.GetAxis("vertical") * Mathf.PI / 4);
        float zSpeed = Mathf.Sin(Input.GetAxis("vertical") * Mathf.PI / 2) * Mathf.Cos(Input.GetAxis("horizontal") * Mathf.PI / 4);
        Vector3 movementVector = new(xSpeed, 0, zSpeed);
        movementVector *= _speed;

        if (_characterController.isGrounded)
        {
            _canJump = true;
            _yAxisMovement = 0;
            _mercyTime = 0;
            _currentHoldTime = 0;
        }
        else
        {
            if (_mercyTime >= _mercyCooldown)
            {
                transform.parent = null;
                _canJump = false;
                _mercyTime = 0;
            }
            else
            {
                _mercyTime += Time.deltaTime;
            }

            if (Input.GetAxisRaw("jump") > 0 && _currentHoldTime < _maxHoldTime && _yAxisMovement > 0)
            {
                _currentHoldTime += Time.deltaTime;
                _yAxisMovement += _holdAcceleration * Time.deltaTime;
            }

            _yAxisMovement += (_gravity);
        }

        if (_canJump)
        {
            if (Input.GetAxis("jump") > 0)
            {
                _canJump = false;
                AudioManager.Instance.PlaySoundEffectClip(_jumpSound, transform, 100f);
                _yAxisMovement = 0;
                _yAxisMovement += _jumpForce;
            }

        }

        movementVector.y = _yAxisMovement;
        movementVector *= Time.deltaTime;
        //convert to the direction of the player
        movementVector = transform.TransformDirection(movementVector);
        _characterController.Move(movementVector);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Death")
        {
            AudioManager.Instance.PlaySoundEffectClip(_deathSound, transform, 100f);
            Die();
        }
        else
        if (hit.collider.tag != "Start")
        {
            transform.parent = hit.transform.parent;//.parent;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeathEvent?.Invoke();
    }
    public delegate void OnDeath();
    public static OnDeath OnDeathEvent;
}
