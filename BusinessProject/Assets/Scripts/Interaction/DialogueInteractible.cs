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

    enum DialogueType
    {
        Start,
        InProgress,
        Finish,
    }
    
    private PlayerMovement player;
    private int step = 0;
    protected override void Handle()
    {
        if (interactionData is DialogueInteractionData dialogueInteractionData)
        {
            player = FindObjectOfType<PlayerMovement>();
            player.isActive = false;

            //Check if there is a task like that in task manager
            if (TaskManager.taskManager.GetTask(interactableID))
            {
                //Task not accepted
                if (!TaskManager.taskManager.GetCurrentTask() || interactableID != TaskManager.taskManager.GetCurrentTask().Data.TaskID)
                {
                    PlayDialogue(dialogueInteractionData.speakersStart, dialogueInteractionData.dialogueStart, dialogueInteractionData.portraitsStart, DialogueType.Start);
                }
                //Task in progress
                else if (interactableID == TaskManager.taskManager.GetCurrentTask().Data.TaskID && TaskManager.taskManager.GetCurrentTask().Status==TaskStatus.InProgress)
                {
                    PlayDialogue(dialogueInteractionData.speakersInProgress, dialogueInteractionData.dialogueInProgress, dialogueInteractionData.portraitsInProgress, DialogueType.InProgress);
                }
                //Task completed
                else if (interactableID == TaskManager.taskManager.GetCurrentTask().Data.TaskID && TaskManager.taskManager.GetCurrentTask().Status==TaskStatus.Completed)
                {
                    PlayDialogue(dialogueInteractionData.speakersFinish, dialogueInteractionData.dialogueFinish, dialogueInteractionData.portraitsFinish, DialogueType.Finish);
                }
            }
            else
            {
                //If there is no task like that in task manager
                PlayDialogue(dialogueInteractionData.speakersStart, dialogueInteractionData.dialogueStart, dialogueInteractionData.portraitsStart, DialogueType.Start);
            }

        }
        else
        {
            Debug.Log("Dialogue Interactable has wrong data asset set!");
        }
    }

    private void PlayDialogue(string[] Speakers, string[] DialogueLines, Sprite[] Portraits, DialogueType type)
    {
        if (step >= DialogueLines.Length)
        {
            if(type == DialogueType.Start) TaskManager.taskManager.AcceptTask(interactableID);
            if (type == DialogueType.Finish)
            {
                TaskManager.taskManager.FinishTask(interactableID);
                Destroy(this);
            }
            step = 0;
            dialogueCanvas.SetActive(false);
            player.isActive = true;
            Debug.Log("Dialogue finished");
            return;
        }
        Debug.Log("Handle Dialogue");
        dialogueCanvas.SetActive(true);
        speakerText.text = Speakers[step];
        dialogueText.text = DialogueLines[step];
        portraitImage.sprite = Portraits[step];
        step += 1;
        Debug.Log(step);
    }
    
}