using UnityEngine;

[CreateAssetMenu(menuName = "Interaction/Pickup")]
public class PickupInteractionData : InteractionData
{
    public bool destroyOnInteract; 

    private void OnValidate()
    {
        SetInteractionType(InteractionType.Pickup);
    }
}