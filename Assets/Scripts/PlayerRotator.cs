using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _sensivity = 5.0f;
    [SerializeField] private float _verticalAngleMaxAbsValue = 89.0f;

    private InputManager _inputManager;

    private float _currentVerticalAngle = 0;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0, _inputManager.MouseInput.x * _sensivity * Time.deltaTime, 0);

        _currentVerticalAngle -= _inputManager.MouseInput.y * _sensivity * Time.deltaTime;
        _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, -_verticalAngleMaxAbsValue, _verticalAngleMaxAbsValue);
        _cameraTransform.localEulerAngles = new Vector3(_currentVerticalAngle, 0, 0);
    }
}
