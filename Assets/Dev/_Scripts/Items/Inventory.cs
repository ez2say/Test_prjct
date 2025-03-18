using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Inventory : MonoBehaviour, IInventory
{
    [SerializeField] private int _slotCount = 5;
    [SerializeField] private GameObject _inventorySlotPrefab;
    [SerializeField] private Transform _inventoryPanel;

    private InteractableItem[] _items;
    private List<Image> _slots = new List<Image>();
    private int _currentIndex = -1;

    public Transform HandPoint;

    private void Start()
    {
        _items = new InteractableItem[_slotCount];
        CreateInventorySlots();
    }

    private void CreateInventorySlots()
    {
        foreach (Transform child in _inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        _slots.Clear();

        for (int i = 0; i < _slotCount; i++)
        {
            GameObject slot = Instantiate(_inventorySlotPrefab, _inventoryPanel);
            Image slotImage = slot.GetComponentInChildren<Image>();
            if (slotImage != null)
            {
                _slots.Add(slotImage);
            }
        }
    }

    public void AddItem(InteractableItem item)
    {
        for (int i = 0; i < _slotCount; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = item;
                _currentIndex = i;

                UpdateUI();
                UpdateUIWithAnimation(-1, _currentIndex);
                UpdateHand();

                Debug.Log($"Предмет '{item.itemName}' добавлен в слот {i}.");
                return;
            }
        }

        Debug.Log("Инвентарь полон!");
    }

    public void RemoveItem(InteractableItem item)
    {
        for (int i = 0; i < _slotCount; i++)
        {
            if (_items[i] == item)
            {
                _items[i] = null;

                if (i == _currentIndex)
                {
                    _currentIndex = -1;
                }

                UpdateUI();
                return;
            }
        }
    }

    public InteractableItem GetCurrentItem()
    {
        if (_currentIndex >= 0 && _currentIndex < _slotCount && _items[_currentIndex] != null)
        {
            return _items[_currentIndex];
        }
        return null;
    }

    public void NextItem()
    {
        if (_items.Length == 0) return;

        int startIndex = _currentIndex;
        int nextIndex = _currentIndex;

        do
        {
            nextIndex = (nextIndex + 1) % _slotCount;
        } while (_items[nextIndex] == null && nextIndex != startIndex);

        if (_items[nextIndex] != null)
        {
            int previousIndex = _currentIndex;
            _currentIndex = nextIndex;

            UpdateUIWithAnimation(previousIndex, _currentIndex);
            UpdateHand();
        }
    }

    private void UpdateUIWithAnimation(int previousIndex, int currentIndex)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (i == currentIndex)
            {
                Image slot = _slots[i];
                slot.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);
                slot.color = Color.white;
            }
            else if (i == previousIndex)
            {
                Image slot = _slots[i];
                slot.transform.DOScale(1f, 0.2f).SetEase(Ease.InBack);
                slot.color = new Color(1, 1, 1, 2f);
            }
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_items[i] != null)
            {
                _slots[i].sprite = _items[i].itemIcon;
                _slots[i].color = Color.white;
            }
            else
            {
                _slots[i].sprite = _inventorySlotPrefab.GetComponentInChildren<Image>().sprite;
                _slots[i].color = Color.white;
            }
        }
    }

    private void UpdateHand()
    {
        if (HandPoint.childCount > 0)
        {
            Transform oldItem = HandPoint.GetChild(0);
            oldItem.DOScale(0, 0.2f).OnComplete(() => Destroy(oldItem.gameObject));
        }

        var currentItem = GetCurrentItem();
        if (currentItem != null)
        {
            GameObject itemInstance = Instantiate(currentItem.gameObject, HandPoint);

            Rigidbody rb = itemInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            itemInstance.transform.localPosition = Vector3.zero;
            itemInstance.transform.localRotation = Quaternion.identity;

            itemInstance.transform.localScale = Vector3.zero;
            itemInstance.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);

            itemInstance.SetActive(true);
        }
    }

    public void DropCurrentItem()
    {
        var currentItem = GetCurrentItem();
        if (currentItem != null)
        {
            if (HandPoint.childCount > 0)
            {
                Destroy(HandPoint.GetChild(0).gameObject);
            }

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