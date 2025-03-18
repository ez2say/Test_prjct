using UnityEngine;

public class ItemManager : MonoBehaviour, IInventory
{
    private InteractableItem[] _items;
    private int _currentIndex = -1;

    public ItemManager(int slotCount)
    {
        _items = new InteractableItem[slotCount];
    }

    public InteractableItem[] GetItems()
    {
        return _items;
    }

    public void AddItem(InteractableItem item)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = item;
                _currentIndex = i;
                Debug.Log($"Предмет '{item.itemName}' добавлен в слот {i}");
                return;
            }
        }
        Debug.Log("Инвентарь полон!");
    }

    public void RemoveItem(InteractableItem item)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == item)
            {
                _items[i] = null;
                if (i == _currentIndex)
                {
                    _currentIndex = -1;
                }
                return;
            }
        }
    }

    public InteractableItem GetCurrentItem()
    {
        if (_currentIndex >= 0 && _currentIndex < _items.Length && _items[_currentIndex] != null)
        {
            return _items[_currentIndex];
        }
        return null;
    }

    public int GetCurrentIndex()
    {
        return _currentIndex;
    }

    public void SelectItem(int index)
    {
        if (index >= 0 && index < _items.Length && _items[index] != null)
        {
            _currentIndex = index;
        }
    }

    public void NextItem()
    {
        int startIndex = _currentIndex;
        int nextIndex = _currentIndex;

        do
        {
            nextIndex = (nextIndex + 1) % _items.Length;
        } while (_items[nextIndex] == null && nextIndex != startIndex);

        if (_items[nextIndex] != null)
        {
            _currentIndex = nextIndex;
        }
    }

    public void PreviousItem()
    {
        int startIndex = _currentIndex;
        int previousIndex = _currentIndex;

        do
        {
            previousIndex = (previousIndex - 1 + _items.Length) % _items.Length;
        } while (_items[previousIndex] == null && previousIndex != startIndex);

        if (_items[previousIndex] != null)
        {
            _currentIndex = previousIndex;
        }
    }

    public void DropCurrentItem()
    {
        var currentItem = GetCurrentItem();
        if (currentItem != null)
        {
            RemoveItem(currentItem);
        }
    }
}