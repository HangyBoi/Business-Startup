using UnityEngine;

[CreateAssetMenu(fileName = "NewTaskData", menuName = "Task/TaskData")]
public class TaskData : ScriptableObject
{
    [Header("Task Information")]
    public string TaskID; // Unique identifier for the task
    public string Title; // Short title of the task
    [TextArea]
    public string Description; // Detailed description of the task

    [Header("Task Requirements")]
    public int RequiredProgress = 100; // Progress needed to complete the task
    public bool RequiresKeyItem = false; // Indicates if an item is required
    public string RequiredItem; // (Optional) Item required to complete the task

    [Header("Rewards")]
    public string RewardDescription; // Description of the reward
    public Sprite RewardIcon; // (Optional) Icon representing the reward

    // Additional fields can be added here as needed
}