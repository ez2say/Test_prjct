using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandController
{
    private Transform _handPoint;
    private float _animationSpeed;
    private ObjectPool<InteractableItem> _itemPool;

    public HandController(Transform handPoint, float animationSpeed, ObjectPool<InteractableItem> itemPool)
    {
        _handPoint = handPoint;
        _animationSpeed = animationSpeed;
        _itemPool = itemPool;
    }

    public void UpdateHand(InteractableItem currentItem)
    {
        if (_handPoint.childCount > 0)
        {
            Transform oldItem = _handPoint.GetChild(0);
            InteractableItem oldInteractable = oldItem.GetComponent<InteractableItem>();

            oldItem.DOScale(0, _animationSpeed).OnComplete(() =>
            {
                oldItem.SetParent(_itemPool._poolContainer);
                _itemPool.ReturnObject(oldInteractable);
            });
        }

        if (currentItem != null)
        {
            currentItem.transform.SetParent(_handPoint);
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localRotation = Quaternion.identity;

            currentItem.gameObject.SetActive(true);

            currentItem.transform.localScale = Vector3.zero;
            currentItem.transform.DOScale(1, _animationSpeed).SetEase(Ease.OutBack);
        }
    }
}