using UnityEngine;


public class InteractableItem : MonoBehaviour, IInteractable
{
    public string itemName;
    public Sprite itemIcon;

    private Renderer _renderer;
    private Material _originalMaterial;
    public Material outlineMaterial;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMaterial = _renderer.material;
    }

    public void ResetState()
    {
        _renderer.material = _originalMaterial;
        gameObject.SetActive(false);
    }

    public void Interact(IInventory inventory)
    {
        Highlight(false);
        Debug.Log($"Предмет '{itemName}' подобран");
        inventory.AddItem(this);
    }

    public void Highlight(bool enable)
    {
        if (_renderer == null) return;

        _renderer.material = enable ? outlineMaterial : _originalMaterial;
    }
}