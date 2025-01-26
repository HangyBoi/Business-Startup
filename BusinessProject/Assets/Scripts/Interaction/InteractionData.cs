using UnityEngine;
using UnityEngine.Serialization;

public enum InteractionType
{
    Pickup,
    Map,
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

// Pickup InteractionData
[CreateAssetMenu(menuName = "Interaction/Pickup")]
public class PickupInteractionData : InteractionData
{
    public bool destroyOnInteract; 
    private void OnValidate()
    {
        SetInteractionType(InteractionType.Pickup);
    }
}

// Talk InteractionData
[CreateAssetMenu(menuName = "Interaction/Map")]
public class MapInteractionData : InteractionData
{
    private void OnValidate()
    {
        SetInteractionType(InteractionType.Map);
    }
}