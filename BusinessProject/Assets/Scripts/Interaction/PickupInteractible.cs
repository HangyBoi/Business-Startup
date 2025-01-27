using UnityEngine;

public class PickupInteractable : Interactable
{
    protected override void Handle()
    {
        Debug.Log("Handle Pickup");
        
        // Ensure TaskManager and Inventory are accessible
        if (TaskManager.taskManager.GetCurrentTask() && interactableID == TaskManager.taskManager.GetCurrentTask().Data.TaskID)
        {
            TaskManager.taskManager.UpdateTaskProgress(interactableID, 1);
        }
        else
        {
            Debug.Log("No task related to this pickup ID!");
        }
        
        if (Inventory.inventory)
        {
            Inventory.inventory.AddItem(this);
        }
        else
        {
            Debug.Log("Inventory not created!");
        }

        // Destroy or deactivate the GameObject based on interaction data
        if (interactionData is PickupInteractionData pIntData && pIntData.destroyOnInteract)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}