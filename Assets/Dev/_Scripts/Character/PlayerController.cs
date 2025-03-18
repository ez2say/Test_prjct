using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public IInventory Inventory { get; private set; }

    private MovementController _movementController;
    private InputSystem _inputSystem;
    private InteractionSystem _interactionSystem;

    public void Initialize(MovementController movementController, InputSystem inputSystem, InteractionSystem interactionSystem, IInventory inventory)
    {
        _movementController = movementController;
        _inputSystem = inputSystem;
        _interactionSystem = interactionSystem;
        Inventory = inventory;
    }

    private void Update()
    {
        _movementController.HandleMovement();
        _interactionSystem.UpdateRaycast();
        HandleInteraction();

        if (_inputSystem.IsMouseScrollUp())
        {
            Inventory.NextItem();
        }
        else if (_inputSystem.IsMouseScrollDown())
        {
            Inventory.PreviousItem();
        }

        int numberKeyPressed = _inputSystem.GetNumberKeyPressed();
        if (numberKeyPressed != -1)
        {
            Inventory.SelectItem(numberKeyPressed);
        }

        if (_inputSystem.IsDropItemPressed())
        {
            Inventory.DropCurrentItem();
        }
    }

    private void HandleInteraction()
    {
        IInteractable currentInteractable = _interactionSystem.GetCurrentInteractable();

        if (currentInteractable != null && _inputSystem.IsInteractPressed())
        {
            if (currentInteractable is InteractableItem item)
            {
                item.Interact(Inventory);
            }
        }
    }
}