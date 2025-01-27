using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory { get; private set; }

    public Dictionary<Interactable, int> items = new Dictionary<Interactable, int>();
    
    public static System.Action ItemAddedToInventory;

    private void Awake()
    {
        if (inventory == null) inventory = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(Interactable item)
    {
        foreach (Interactable pItem in items.Keys)
        {
            if (pItem.interactableID == item.interactableID)
            {
                items[pItem]++;
                ItemAddedToInventory?.Invoke();
                return;
            }
        } 
        
        items.Add(item, 1);
        //Debug.Log($"Item '{item}' added to inventory.");
        ItemAddedToInventory?.Invoke();
        // Notify TaskManager or other systems if needed
    }

    public bool ContainsItem(Interactable item)
    {
        foreach (Interactable pItem in items.Keys)
        {
            if (pItem.interactableID == item.interactableID) return true;
        } 
        return false;
    }
}