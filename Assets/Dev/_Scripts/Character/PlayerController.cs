using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovementController _movementController;
    private InputSystem _inputSystem;
    private InteractionSystem _interactionSystem;
    private IInventory _inventory;

    public void Initialize(MovementController movementController, InputSystem inputSystem, InteractionSystem interactionSystem, IInventory inventory)
    {
        _movementController = movementController;
        _inputSystem = inputSystem;
        _interactionSystem = interactionSystem;
        _inventory = inventory;
    }

    private void Update()
    {
        _movementController.HandleMovement();
        _interactionSystem.UpdateRaycast();
        HandleInteraction();

        if (_inputSystem.IsNextItemPressed())
        {
            _inventory.NextItem();
        }

        if (_inputSystem.IsDropItemPressed())
        {
            _inventory.DropCurrentItem();
        }
    }

    private void HandleInteraction()
    {
        IInteractable currentInteractable = _interactionSystem.GetCurrentInteractable();

        if (currentInteractable != null && _inputSystem.IsInteractPressed())
        {
            if (currentInteractable is InteractableItem item)
            {
                item.Interact(_inventory);
            }
        }
    }

    
}