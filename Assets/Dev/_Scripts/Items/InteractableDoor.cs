using UnityEngine;

public class InteractableDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string _keyItemName = "Key";
    [SerializeField] private GameObject _victoryUI;
    private Animator _doorAnimator;
    private bool _isPlayerInRange = false;
    private PlayerController _playerController;

    private void Start()
    {
        _doorAnimator = GetComponent<Animator>();
    }

    public void Interact(IInventory inventory)
    {
        if (!_isPlayerInRange) return;

        var key = inventory.GetCurrentItem();
        if (key != null && key.itemName == _keyItemName)
        {
            OpenDoor();
            ShowVictoryUI();
        }
        else
        {
            Debug.Log("Нужен ключ, чтобы открыть дверь!");
        }
    }

    public void Highlight(bool enable)
    {
        if (enable && _isPlayerInRange)
        {
            Debug.Log("Дверь подсвечена");
        }
    }

    private void OpenDoor()
    {

        _doorAnimator.SetTrigger("Open");
        Debug.Log("Дверь открыта!");
    }

    private void ShowVictoryUI()
    {
        if (_victoryUI != null)
        {
            _victoryUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("В зоне открытия двери");

            _playerController = other.GetComponent<PlayerController>();

            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
            Debug.Log("Дверь больше не интерактивна!");
        }
    }

    private void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (_playerController != null)
            {
                var inventory = _playerController.Inventory;
                if (inventory != null)
                {
                    Interact(inventory);
                }
            }
        }
    }
}