using UnityEngine;

public enum InteractionType
{
    Pickup,
    Talk,
    // Add more interaction types as needed
}
public abstract class InteractionData : ScriptableObject
{
    public string interactionName;
    public string popupText;
    public Sprite icon;
    public InteractionType interactionType;
}

// Pickup InteractionData
[CreateAssetMenu(menuName = "Interaction/Pickup")]
public class PickupInteractionData : InteractionData
{
    public string itemName;
    public bool destroyOnInteract;
}

// Talk InteractionData
[CreateAssetMenu(menuName = "Interaction/Talk")]
public class TalkInteractionData : InteractionData
{
    //public DialogueData dialogueData; // Reference to a Dialogue ScriptableObject
}