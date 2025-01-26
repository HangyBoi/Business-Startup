using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Tooltip("Unique identifier for the interactable")]
    public string interactableID; 

    [Tooltip("Interaction data asset")]
    public InteractionData interactionData;
    public static event System.Action<Interactable> InteractableDestroyed;
    
    private void OnDestroy()
    {
        InteractableDestroyed?.Invoke(this);
    }

    /// <summary>
    /// Executes the interaction based on InteractionData.
    /// </summary>
    public void OnInteract()
    {
        switch (interactionData.interactionType)
        {
            case InteractionType.Pickup:
                HandlePickup();
                break;
            case InteractionType.Map:
                HandleMap();
                break;
            default:
                Debug.LogWarning("Unhandled interaction type: " + interactionData.interactionType);
                break;
        }
    }

    /// <summary>
    /// Returns the text to display in the popup.
    /// </summary>
    public string GetPopupText()
    {
        return interactionData.interactableName;
    }

    #region Interaction Handlers

    private void HandlePickup()
    {
        //Debug.Log("Handle Pickup");
        //if(!TaskManager.taskManager.GetCurrentTask()) Debug.LogAssertion("No current task in task manager!");
        if(TaskManager.taskManager.GetCurrentTask() && interactableID == TaskManager.taskManager.GetCurrentTask().Data.TaskID) TaskManager.taskManager.UpdateTaskProgress(interactableID, 1);
        // else
        // {
        //     Debug.Log("No task related to this pickup id!");
        // }
        
        if(Inventory.inventory) Inventory.inventory.AddItem(this);
        else
        {
            Debug.Log("Inventory not created!");
        }
        if (interactionData is PickupInteractionData pIntData && pIntData.destroyOnInteract)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void HandleMap()
    {
        //Debug.Log("Handle Map");
        if(UIManager.uiManager.mapUiController) UIManager.uiManager.mapUiController.ShowMap();
        TestAcceptTask();
    }

    #endregion
    
    /// <summary>
    /// Delayed test method to accept a task after initialization.
    /// </summary>
    private void TestAcceptTask()
    {
        TaskManager.taskManager.AcceptTask("0");
    }
}
