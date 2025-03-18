using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Inventory : MonoBehaviour, IInventory
{   
    [SerializeField] private Transform _inventoryPanel;
    private int _slotCount;
    private GameObject _inventorySlotPrefab;
    private float _animationSpeed;

    public Transform _handPoint;

    private ItemManager _itemManager;
    private InventoryUI _inventoryUI;
    private HandController _handController;

    public void Initialize(int slotCount, GameObject slotPrefab, float animationSpeed)
    {
        _slotCount = slotCount;
        _inventorySlotPrefab = slotPrefab;
        _animationSpeed = animationSpeed;
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        _itemManager = new ItemManager(_slotCount);
        _inventoryUI = new InventoryUI(_inventoryPanel, _inventorySlotPrefab, _slotCount, _animationSpeed);
        _handController = new HandController(_handPoint, _animationSpeed);
    }

    public InteractableItem GetCurrentItem()
    {
        return _itemManager.GetCurrentItem();
    }

    public void AddItem(InteractableItem item)
    {
        _itemManager.AddItem(item);
        _inventoryUI.UpdateUI(_itemManager.GetItems());
        _handController.UpdateHand(_itemManager.GetCurrentItem());
    }

    public void RemoveItem(InteractableItem item)
    {
        _itemManager.RemoveItem(item);
        _inventoryUI.UpdateUI(_itemManager.GetItems());
        _handController.UpdateHand(_itemManager.GetCurrentItem());
    }

    public void NextItem()
    {
        _itemManager.NextItem();
        _inventoryUI.HighlightSlot(_itemManager.GetCurrentIndex());
        _handController.UpdateHand(_itemManager.GetCurrentItem());
    }

    public void SelectItem(int index)
    {
        _itemManager.SelectItem(index);
        _inventoryUI.HighlightSlot(_itemManager.GetCurrentIndex());
        _handController.UpdateHand(_itemManager.GetCurrentItem());
    }

    public void PreviousItem()
    {
        _itemManager.PreviousItem();
        _inventoryUI.HighlightSlot(_itemManager.GetCurrentIndex());
        _handController.UpdateHand(_itemManager.GetCurrentItem());
    }

    public void DropCurrentItem()
    {
        var currentItem = _itemManager.GetCurrentItem();
        if (currentItem != null)
        {
            RemoveItem(currentItem);

            GameObject droppedItem = Instantiate(currentItem.gameObject, Camera.main.transform.position + Camera.main.transform.forward * 2, Quaternion.identity);
            droppedItem.SetActive(true);

            Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = droppedItem.AddComponent<Rigidbody>();
            }
            rb.isKinematic = false;
            rb.velocity = Camera.main.transform.forward * 10f;
        }
    }
}