using UnityEngine;
using UnityEngine.Serialization;

public enum InteractionType
{
    Pickup,
    Map,
    Dialogue,
    Protection,
    // Add more interaction types as needed
}
public abstract class InteractionData : ScriptableObject
{
    public string interactableName;
    public Sprite icon;
    public InteractionType interactionType { get; private set; }
    protected void SetInteractionType(InteractionType type)
    {
        interactionType = type;
    }
}