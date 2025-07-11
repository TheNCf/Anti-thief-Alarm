using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputManager))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 6.0f;
    [SerializeField] private float _jumpStrength = 5.0f;
    [SerializeField] private Transform _groundPoint;

    private CharacterController _characterController;
    private InputManager _inputManager;

    private Vector3 _velocity;
    private Vector3 _gravity = new Vector3(0.0f, -9.81f, 0.0f);
    private float _groundCheckRadius = 0.3f;
    private bool _jumpInputBuffer;
    private float _jumpInputBufferTime = 0.2f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputManager = GetComponent<InputManager>();

        _inputManager.JumpButtonPressed += RegisterJumpInput;
    }

    private void Update()
    {
        Jump();
        Move();
    }

    private void Move()
    {
        _velocity += _gravity * Time.deltaTime;

        if (_characterController.isGrounded)
            _velocity.y = 0.0f;

        Vector3 direction = _inputManager.MovementInput * _movementSpeed;
        direction = transform.TransformDirection(direction);
        Vector3 movement = new Vector3(direction.x, _velocity.y, direction.z) * Time.deltaTime;

        _characterController.Move(movement);
    }

    private void Jump()
    {
        if (_jumpInputBuffer && Physics.OverlapSphere(_groundPoint.position, _groundCheckRadius).Length > 1)
            _velocity.y = _jumpStrength;
    }

    private void RegisterJumpInput()
    {
        StopCoroutine(JumpBufferCoroutine());
        StartCoroutine(JumpBufferCoroutine());
    }

    private IEnumerator JumpBufferCoroutine()
    {
        _jumpInputBuffer = true;
        yield return new WaitForSeconds(_jumpInputBufferTime);
        _jumpInputBuffer = false;
    }
}
