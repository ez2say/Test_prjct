using UnityEngine;

[RequireComponent(typeof(Renderer))]
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

    public void Interact(IInventory inventory)
    {
        Highlight(false);
        Debug.Log($"Предмет '{itemName}' подобран!");
        inventory.AddItem(this);
        gameObject.SetActive(false);
    }

    public void Highlight(bool enable)
    {
        if (_renderer == null) return;

        _renderer.material = enable ? outlineMaterial : _originalMaterial;
    }
}