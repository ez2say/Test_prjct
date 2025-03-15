using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovementController _movementController;
    private InputSystem _inputSystem;

    public void Initialize(MovementController movementController)
    {
        _movementController = movementController;
    }

    private void Update()
    {
        _movementController.HandleMovement();
    }
}