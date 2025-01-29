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
    
    public void TakeItem(string id, int amount)
    {
        foreach (Interactable pItem in items.Keys)
        {
            if (pItem.interactableID == id)
            {
                items[pItem]-=amount;
                if (items[pItem] <= 0)
                {
                    items.Remove(pItem);
                }
                ItemAddedToInventory?.Invoke();
                return;
            }
        } 
    }

    public bool ContainsItem(Interactable item)
    {
        foreach (Interactable pItem in items.Keys)
        {
            if (pItem.interactableID == item.interactableID) return true;
        } 
        return false;
    }
    
    public bool ContainsItemByID(string id)
    {
        foreach (Interactable pItem in items.Keys)
        {
            if (pItem.interactableID == id) return true;
        } 
        return false;
    }
    
    public int GetAmountOf(string id)
    {
        foreach (Interactable pItem in items.Keys)
        {
            if (pItem.interactableID == id) return items[pItem];
        } 
        return 0;
    }
}