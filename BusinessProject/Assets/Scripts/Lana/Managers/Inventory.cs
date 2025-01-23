using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory { get; private set; }

    private List<Interactable> items = new List<Interactable>();

    private void Awake()
    {
        if (inventory == null) inventory = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(Interactable item)
    {
        items.Add(item);
        Debug.Log($"Item '{item}' added to inventory.");
        // Notify TaskManager or other systems if needed
    }

    public bool ContainsItem(Interactable item)
    {
        return items.Contains(item);
    }
}