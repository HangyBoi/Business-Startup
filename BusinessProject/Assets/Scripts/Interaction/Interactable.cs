using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
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
        Handle();
    }
    protected abstract void Handle();

    /// <summary>
    /// Returns the text to display in the popup.
    /// </summary>
    public string GetPopupText()
    {
        return interactionData.interactableName;
    }

}
