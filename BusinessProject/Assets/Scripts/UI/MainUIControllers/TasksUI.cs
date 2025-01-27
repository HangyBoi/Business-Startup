using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TasksUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI taskTitleText;
    [SerializeField] private TextMeshProUGUI taskProgressionText;

    public void HandleTaskAccepted(Task task)
    {
        //Debug.Log($"UIManager: Task '{task.Data.Title}' accepted.");
        taskTitleText.text = task.Data.Title;
        taskProgressionText.text = $"{task.CurrentProgress}/{task.Data.RequiredProgress}";
        // Additional UI updates can be handled here
    }
    public void HandleTaskProgressed(Task task)
    {
        //Debug.Log($"UIManager: Task '{task.Data.Title}' progressed.");
        taskProgressionText.text = $"{task.CurrentProgress}/{task.Data.RequiredProgress}";
        // Additional UI updates can be handled here
    }
    public void HandleTaskCompleted(Task task)
    {
        //Debug.Log($"UIManager: Task '{task.Data.Title}' completed.");
        taskProgressionText.text = "Completed!";
    }
}
