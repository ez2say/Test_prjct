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
    private ObjectPool<InteractableItem> _itemPool;

    public void Initialize(int slotCount, GameObject slotPrefab, float animationSpeed)
    {
        _slotCount = slotCount;
        _inventorySlotPrefab = slotPrefab;
        _animationSpeed = animationSpeed;

        _itemPool = new ObjectPool<InteractableItem>("InventoryObjectPool");

        InitializeComponent();
    }

    private void InitializeComponent()
    {
        _itemManager = new ItemManager(_slotCount);
        _inventoryUI = new InventoryUI(_inventoryPanel, _inventorySlotPrefab, _slotCount, _animationSpeed);
        _handController = new HandController(_handPoint, _animationSpeed, _itemPool);
    }

    public InteractableItem GetCurrentItem()
    {
        return _itemManager.GetCurrentItem();
    }

    public void AddItem(InteractableItem item)
    {
        _itemPool.AddObject(item);

        _itemManager.AddItem(item);

        _inventoryUI.UpdateUI(_itemManager.GetItems());

        _handController.UpdateHand(item);

        if (!item.gameObject.activeInHierarchy)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void RemoveItem(InteractableItem item)
    {
        _itemManager.RemoveItem(item);

        _inventoryUI.UpdateUI(_itemManager.GetItems());

        _itemPool.ReturnObject(item);
    }

    public void NextItem()
    {
        var currentItem = _itemManager.GetCurrentItem();

        _itemManager.NextItem();

        _inventoryUI.HighlightSlot(_itemManager.GetCurrentIndex());

        _handController.UpdateHand(_itemManager.GetCurrentItem());

        if (currentItem != null)
        {
            _itemPool.ReturnObject(currentItem);
        }
    }

    public void SelectItem(int index)
    {
        var currentItem = _itemManager.GetCurrentItem();

        _itemManager.SelectItem(index);

        _inventoryUI.HighlightSlot(_itemManager.GetCurrentIndex());

        _handController.UpdateHand(_itemManager.GetCurrentItem());

        if (currentItem != null)
        {
            _itemPool.ReturnObject(currentItem);
        }
    }

    public void PreviousItem()
    {
        var currentItem = _itemManager.GetCurrentItem();

        _itemManager.PreviousItem();

        _inventoryUI.HighlightSlot(_itemManager.GetCurrentIndex());

        _handController.UpdateHand(_itemManager.GetCurrentItem());

        if (currentItem != null)
        {
            _itemPool.ReturnObject(currentItem);
        }
    }

    public void DropCurrentItem()
    {
        var currentItem = _itemManager.GetCurrentItem();
        if (currentItem != null)
        {
            GameObject droppedItem = Instantiate(currentItem.gameObject, Camera.main.transform.position + Camera.main.transform.forward * 2, Quaternion.identity);
            droppedItem.SetActive(true);


            _itemPool.RemoveObject(currentItem);
            Destroy(currentItem.gameObject);


            droppedItem.transform.DOMove(Camera.main.transform.position + Camera.main.transform.forward * 5, 0.5f).SetEase(Ease.Linear);


            _itemManager.RemoveItem(currentItem);
            _inventoryUI.UpdateUI(_itemManager.GetItems());
            _handController.UpdateHand(null);
        }
    }
}