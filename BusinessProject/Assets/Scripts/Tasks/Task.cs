using Unity.VisualScripting;
using UnityEngine;
// TaskStatus.cs
public enum TaskStatus
{
    NotAccepted,
    InProgress,
    Completed,
    Finished
}

public class Task : MonoBehaviour
{
    [Tooltip("Task data asset")]
    public TaskData Data;

    public TaskStatus Status { get; private set; }
    public int CurrentProgress { get; private set; }

    private void Awake()
    {
        // Ensure Data is assigned
        if (Data == null)
        {
            Debug.LogError($"Task on '{gameObject.name}' is missing TaskData.");
            return;
        }

        // Automatically assign a unique TaskID if not set
        if (string.IsNullOrEmpty(Data.TaskID))
        {
            Data.TaskID = System.Guid.NewGuid().ToString();
            Debug.Log($"Assigned new TaskID '{Data.TaskID}' to Task '{Data.Title}'.");
        }

        // Initialize task status
        Status = TaskStatus.NotAccepted;
        CurrentProgress = 0;
    }

    /// <summary>
    /// Accepts the task, changing its status to Accepted.
    /// </summary>
    public void Accept()
    {
        if (Status == TaskStatus.NotAccepted)
        {
            Status = TaskStatus.InProgress;
            //Debug.Log($"Task '{Data.Title}' accepted.");
        }
        else
        {
            Debug.LogWarning($"Cannot accept Task '{Data.Title}' because it is already {Status}.");
        }
    }

    /// <summary>
    /// Updates the task's progress by a specified amount.
    /// </summary>
    /// <param name="amount">Amount to increase progress by.</param>
    public void UpdateProgress(int amount)
    {
        if (Status == TaskStatus.InProgress)
        {
            CurrentProgress += amount;
            CurrentProgress = Mathf.Clamp(CurrentProgress, 0, Data.RequiredProgress);

            // Update status based on progress
            if (CurrentProgress > 0 && CurrentProgress < Data.RequiredProgress)
            {
                Status = TaskStatus.InProgress;
            }
            else if (CurrentProgress >= Data.RequiredProgress)
            {
                Status = TaskStatus.Completed;
                CurrentProgress = Data.RequiredProgress;
                //Debug.Log($"Task '{Data.Title}' completed!");
                // Optionally, trigger rewards or notifications here
            }

            //Debug.Log($"Task '{Data.Title}' progress: {CurrentProgress}/{Data.RequiredProgress}");
        }
        else
        {
            Debug.LogWarning($"Cannot update progress for Task '{Data.Title}' because it is {Status}.");
        }
    }

    /// <summary>
    /// Manually completes the task, regardless of progress.
    /// </summary>
    public void Complete()
    {
        if (Status != TaskStatus.Completed)
        {
            CurrentProgress = Data.RequiredProgress;
            Status = TaskStatus.Completed;
            Debug.Log($"Task '{Data.Title}' manually marked as completed.");
            // Optionally, trigger rewards or notifications here
        }
        else
        {
            Debug.LogWarning($"Task '{Data.Title}' is already completed.");
        }
    }
    public void Finish()
    {
        if (Status == TaskStatus.Completed)
        {
            Status = TaskStatus.Finished;
            Debug.Log($"Task '{Data.Title}' finished.");
            // Optionally, trigger rewards or notifications here
        }
        else
        {
            Debug.LogWarning($"Task '{Data.Title}' is not yet completed.");
        }
    }

    /// <summary>
    /// Resets the task to its initial state.
    /// </summary>
    public void ResetTask()
    {
        CurrentProgress = 0;
        Status = TaskStatus.NotAccepted;
        Debug.Log($"Task '{Data.Title}' has been reset.");
    }
}
