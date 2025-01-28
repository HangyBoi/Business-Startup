using UnityEngine;

[CreateAssetMenu(menuName = "Interaction/Dialogue")]
public class DialogueInteractionData : InteractionData
{
    //Dialogue content
    [SerializeField] public string[] speakersStart;
    [SerializeField] public string[] dialogueStart;
    [SerializeField] public Sprite[] portraitsStart;
    
    [SerializeField] public string[] speakersInProgress;
    [SerializeField] public string[] dialogueInProgress;
    [SerializeField] public Sprite[] portraitsInProgress;
    
    [SerializeField] public string[] speakersFinish;
    [SerializeField] public string[] dialogueFinish;
    [SerializeField] public Sprite[] portraitsFinish;
    private void OnValidate()
    {
        SetInteractionType(InteractionType.Dialogue);
    }
}