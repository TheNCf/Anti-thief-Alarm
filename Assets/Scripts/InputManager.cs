using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private string _horizontalAxis;
    [SerializeField] private string _verticalAxis;
    [SerializeField] private string _horizontalMouseAxis;
    [SerializeField] private string _verticalMouseAxis;
    [SerializeField] private KeyCode _jumpButton;

    public event Action JumpButtonPressed;

    public Vector3 MovementInput { get; private set; }
    public Vector2 MouseInput { get; private set; }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        float horizontalInput = Input.GetAxis(_horizontalAxis);
        float verticalInput = Input.GetAxis(_verticalAxis);
        MovementInput = new Vector3(horizontalInput, 0, verticalInput);

        float horizontalMouseInput = Input.GetAxis(_horizontalMouseAxis);
        float verticalMouseInput = Input.GetAxis(_verticalMouseAxis);
        MouseInput = new Vector2(horizontalMouseInput, verticalMouseInput);

        if (Input.GetKeyDown(_jumpButton))
            JumpButtonPressed?.Invoke();
    }
}
