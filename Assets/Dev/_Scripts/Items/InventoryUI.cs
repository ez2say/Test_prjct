using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private List<Image> _slots = new List<Image>();
    private float _animationSpeed;

    public InventoryUI(Transform inventoryPanel, GameObject slotPrefab, int slotCount, float animationSpeed)
    {
        _animationSpeed = animationSpeed;

        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryPanel);
            Image slotImage = slot.transform.Find("Image").GetComponent<Image>();
            if (slotImage != null)
            {
                _slots.Add(slotImage);
            }
        }
    }

    public void UpdateUI(InteractableItem[] items)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (items[i] != null)
            {
                _slots[i].sprite = items[i].itemIcon;
                _slots[i].color = Color.white;
            }
            else
            {
                _slots[i].sprite = null;
                _slots[i].color = Color.clear;
            }
        }
    }

    public void HighlightSlot(int index)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (i == index)
            {
                _slots[i].transform.DOScale(1.2f, _animationSpeed).SetEase(Ease.OutBack);
                _slots[i].color = Color.white;
            }
            else
            {
                _slots[i].transform.DOScale(1f, _animationSpeed).SetEase(Ease.InBack);
                _slots[i].color = new Color(1, 1, 1, 0.5f);
            }
        }
    }
}