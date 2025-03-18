public interface IInventory
{
    void AddItem(InteractableItem item);
    void RemoveItem(InteractableItem item);
    InteractableItem GetCurrentItem();
    void NextItem();
    void DropCurrentItem();
}