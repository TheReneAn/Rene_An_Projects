using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";    // Name of the item
    public Sprite icon = null;              // Item icon
    public bool inDfaultItem = false;       // Is the item default wear?

    public virtual void Use()
    {
        // Use the item
        // Someting might happen

        Debug.Log("Using " + name);
    }
}
