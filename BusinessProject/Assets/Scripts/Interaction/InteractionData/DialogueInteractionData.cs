using UnityEngine;

[CreateAssetMenu(menuName = "Interaction/Dialogue")]
public class DialogueInteractionData : InteractionData
{
    //Dialogue content
    [SerializeField] private string[] speakers;
    [SerializeField] private string[] dialogue;
    [SerializeField] private Sprite[] portraits;
    private void OnValidate()
    {
        SetInteractionType(InteractionType.Dialogue);
    }
    
    // Public getters for encapsulation
    public string[] Speakers => speakers;
    public string[] DialogueLines => dialogue;
    public Sprite[] Portraits => portraits;
}