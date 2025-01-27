using UnityEngine;

[CreateAssetMenu(menuName = "Interaction/Map")]
public class MapInteractionData : InteractionData
{
    private void OnValidate()
    {
        SetInteractionType(InteractionType.Map);
    }
}