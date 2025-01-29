using UnityEngine;

[CreateAssetMenu(menuName = "Interaction/Protection")]
public class ProtectionBoonData : InteractionData
{
    public int requiredAmount;
    private void OnValidate()
    {
        SetInteractionType(InteractionType.Protection);
    }
}