using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private float _raycastDistance;
    private IInteractable _currentInteractable;


    public void Initialize(float raycastDistance)
    {
        _raycastDistance = raycastDistance;
    }
    public IInteractable GetCurrentInteractable()
    {
        return _currentInteractable;
    }

    public void UpdateRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (_currentInteractable != interactable)
                {
                    if (_currentInteractable != null)
                    {
                        _currentInteractable.Highlight(false);
                    }

                    _currentInteractable = interactable;
                    _currentInteractable.Highlight(true);
                }
            }
            else
            {
                if (_currentInteractable != null)
                {
                    _currentInteractable.Highlight(false);
                    _currentInteractable = null;
                }
            }
        }
        else
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.Highlight(false);
                _currentInteractable = null;
            }
        }
    }
}