using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueInteractable : Interactable
{
    //UI References
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image portraitImage;

    private int step;
    protected override void Handle()
    {
        if (interactionData is DialogueInteractionData dialogueInteractionData)
        {
            if (step >= dialogueInteractionData.DialogueLines.Length)
            {
                step = 0;
                dialogueCanvas.SetActive(false);
                Debug.Log("Dialogue finished");
                return;
            }
            Debug.Log("Handle Dialogue");
            dialogueCanvas.SetActive(true);
            speakerText.text = dialogueInteractionData.Speakers[step];
            dialogueText.text = dialogueInteractionData.DialogueLines[step];
            portraitImage.sprite = dialogueInteractionData.Portraits[step];
            step += 1;
        }
        else
        {
            Debug.Log("Dialogue Interactable has wrong data asset set!");
        }
    }
    
}