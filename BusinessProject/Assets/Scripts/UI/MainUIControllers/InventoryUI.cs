using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private GameObject itemsPanel;
    public void ReinitializeInventory()
    {
        ClearInventoryUI();
        InstantiateInventorySlots();
    }

    private void InstantiateInventorySlots()
    {
        foreach (KeyValuePair<Interactable, int> item in Inventory.inventory.items)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, itemsPanel.transform);
            slot.GetComponentInChildren<Image>().sprite = item.Key.interactionData.icon;
            slot.GetComponentInChildren<TextMeshProUGUI>().text = item.Value.ToString();
        }
    }

    private void ClearInventoryUI()
    {
        // Iterate through each child transform of itemsPanel and destroy the GameObject
        foreach (Transform child in itemsPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
