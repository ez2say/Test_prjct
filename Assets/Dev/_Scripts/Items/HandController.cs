using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class HandController : MonoBehaviour
{
    private Transform _handPoint;
    private float _animationSpeed;

    public HandController(Transform handPoint, float animationSpeed)
    {
        _handPoint = handPoint;
        _animationSpeed = animationSpeed;
    }

    public void UpdateHand(InteractableItem currentItem)
    {
        if (_handPoint.childCount > 0)
        {
            Transform oldItem = _handPoint.GetChild(0);
            oldItem.DOScale(0, _animationSpeed).OnComplete(() => Destroy(oldItem.gameObject));
        }

        if (currentItem != null)
        {
            GameObject itemInstance = Instantiate(currentItem.gameObject, _handPoint);

            Rigidbody rb = itemInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            itemInstance.transform.localPosition = Vector3.zero;
            itemInstance.transform.localRotation = Quaternion.identity;

            itemInstance.transform.localScale = Vector3.zero;
            itemInstance.transform.DOScale(1, _animationSpeed).SetEase(Ease.OutBack);

            itemInstance.SetActive(true);
        }
    }
}