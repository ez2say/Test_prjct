using UnityEngine;

public class MovementController
{
    private readonly CharacterController _characterController;
    private readonly InputSystem _inputSystem;
    private readonly float _moveSpeed;
    private readonly float _mouseSensitivity;
    private Transform _playerTransform;
    private Transform _cameraPivot;
    private float _rotationX = 0f;

    private Vector3 _moveDirection;


    public MovementController(CharacterController characterController, InputSystem inputSystem, float moveSpeed, float mouseSensitivity,Transform playerTransform, Transform cameraPivot)
    {
        _characterController = characterController;
        _inputSystem = inputSystem;
        _moveSpeed = moveSpeed;
        _mouseSensitivity = mouseSensitivity;
        _playerTransform = playerTransform;
        _cameraPivot = cameraPivot;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HandleMovement()
    {
        _moveDirection = _inputSystem.GetMovementInput();
        _moveDirection = _characterController.transform.TransformDirection(_moveDirection);
        _moveDirection *= _moveSpeed;

        _characterController.Move(_moveDirection * Time.deltaTime);

        float mouseX = _inputSystem.GetMouseX() * _mouseSensitivity;
        float mouseY = _inputSystem.GetMouseY() * _mouseSensitivity;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        _cameraPivot.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);


        _playerTransform.Rotate(Vector3.up * mouseX);
    }
}