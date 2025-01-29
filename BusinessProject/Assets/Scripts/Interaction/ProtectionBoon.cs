using UnityEngine;

public class ProtectionBoon : Interactable
{
    public static event System.Action BoonUpgraded;
    protected override void Handle()
    {
        // Ensure UIManager and MapUiController are accessible
        if (Inventory.inventory && interactionData is ProtectionBoonData pbData)
        { 
            Debug.Log("Handle Boon");
            if(Inventory.inventory.GetAmountOf(interactableID) >= pbData.requiredAmount) Upgarde();  
        }
        else
        {
            Debug.LogError("Inventory not found.");
        }
    }

    private void Upgarde()
    {
        if (Inventory.inventory && interactionData is ProtectionBoonData pbData)
        {
            Debug.Log("Handle Upgrade Protection Boon");
            BoonUpgraded?.Invoke();
            Inventory.inventory.TakeItem(interactableID, pbData.requiredAmount);
        }
    }
}