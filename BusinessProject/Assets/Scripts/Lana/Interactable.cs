// Interactable.cs
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Tooltip("Unique identifier for the interactable")]
    public string interactableID; // Unique identifier (e.g., UUID)

    [Tooltip("Interaction data asset")]
    public InteractionData interactionData;

    // private void Start()
    // {
    //     // Optionally, check if this interactable has already been interacted with
    //     if (GameStateManager.Instance.HasInteracted(interactableID))
    //     {
    //         gameObject.SetActive(false);
    //     }
    // }

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
            case InteractionType.Talk:
                HandleTalk();
                break;
            // Add more cases for additional interaction types
            default:
                Debug.LogWarning("Unhandled interaction type: " + interactionData.interactionType);
                break;
        }

        // Register the interaction to manage state
        //GameStateManager.Instance.RegisterInteraction(interactableID);
    }

    /// <summary>
    /// Returns the text to display in the popup.
    /// </summary>
    public string GetPopupText()
    {
        return interactionData.popupText;
    }

    /// <summary>
    /// Returns the world position for UI popup placement.
    /// </summary>
    public Vector3 GetUIPosition() => transform.position;

    #region Interaction Handlers

    private void HandlePickup()
    {
        Debug.Log("Handle Pickup");
        // Example: Add item to inventory
        // Inventory.Instance.AddItem(interactionData.itemName);
        //
        // Decide to destroy or deactivate based on InteractionData
        if (interactionData is PickupInteractionData pIntData && pIntData.destroyOnInteract)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void HandleTalk()
    {
        Debug.Log("Handle Talk");
        // Example: Start dialogue
        //DialogueManager.Instance.StartDialogue(interactionData.dialogueData);
    }

    #endregion
}
