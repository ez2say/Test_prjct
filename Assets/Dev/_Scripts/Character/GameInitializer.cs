using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    [SerializeField] private CharacterController _characterController;

    [SerializeField] private Inventory _inventoryPrefab;


    private void Awake()
    {

        PlayerController playerController = _characterController.GetComponent<PlayerController>();
        Transform playerTransform = playerController.transform;
        Transform cameraPivot = playerTransform.Find("Head");
        InteractionSystem interactionSystem = new InteractionSystem();
        interactionSystem.Initialize(_gameConfig.RayCastDistance);

        InputSystem inputSystem = new InputSystem();
        MovementController movementController= new MovementController(_characterController, inputSystem, _gameConfig.MoveSpeed, _gameConfig.MouseSensitivity,playerTransform, cameraPivot);
        playerController.Initialize(movementController, inputSystem, interactionSystem, _inventoryPrefab);
    }
}