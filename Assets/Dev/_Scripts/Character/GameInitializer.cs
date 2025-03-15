using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    [SerializeField] private CharacterController _characterController;

    private void Awake()
    {
        

        PlayerController playerController = _characterController.GetComponent<PlayerController>();
        Transform playerTransform = playerController.transform;
        Transform cameraPivot = playerTransform.Find("Head");

        InputSystem inputSystem = new InputSystem();
        MovementController movementController= new MovementController(_characterController, inputSystem, _gameConfig.MoveSpeed, _gameConfig.MouseSensitivity,playerTransform, cameraPivot);
        playerController.Initialize(movementController);
    }
}