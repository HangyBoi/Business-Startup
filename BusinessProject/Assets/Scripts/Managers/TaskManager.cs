using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager taskManager { get; private set; }

    // Dictionary to store tasks with TaskID as the key
    private Dictionary<string, Task> tasks = new Dictionary<string, Task>();

    // Current active task (optional, can be multiple)
    private Task currentTask;

    public static event System.Action<Task> OnTaskAccepted;
    public static event System.Action<Task> OnTaskProgressed;
    public static event System.Action<Task> OnTaskCompleted;
    public static event System.Action<Task> OnTaskFinished;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (taskManager == null)
        {
            taskManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; // Early exit to prevent further execution
        }

        // Initialize tasks here to ensure they're available before Start methods run
        //InitializeTasks();
    }

    private void Start()
    {
        InitializeTasks();
    }

    private void InitializeTasks()
    {
        List<Task> taskList = FindObjectsOfType<Task>().ToList();
        if (taskList.Count != 0)
        {
            foreach (Task task in taskList)
            {
                AddTask(task);
            }
        }
        else
        {
            Debug.LogWarning("No tasks found in the scene to initialize.");
        }
    }

    /// <summary>
    /// Adds a new task to the manager.
    /// </summary>
    /// <param name="task">The task to add.</param>
    public void AddTask(Task task)
    {
        if (!tasks.ContainsKey(task.Data.TaskID))
        {
            tasks.Add(task.Data.TaskID, task);
            //Debug.Log($"Task '{task.Data.Title}' added to TaskManager.");
        }
        else
        {
            Debug.LogWarning($"Task with ID '{task.Data.TaskID}' already exists.");
        }
    }

    /// <summary>
    /// Accepts a task by its TaskID.
    /// </summary>
    /// <param name="taskID">The ID of the task to accept.</param>
    public void AcceptTask(string taskID)
    {
        if (tasks.ContainsKey(taskID))
        {
            Task task = tasks[taskID];
            task.Accept();
            currentTask = task;
            OnTaskAccepted?.Invoke(task);
        }
        else
        {
            Debug.LogWarning($"Task with ID '{taskID}' not found.");
        }
    }

    /// <summary>
    /// Completes a task by its TaskID.
    /// </summary>
    /// <param name="taskID">The ID of the task to complete.</param>
    public void CompleteTask(string taskID)
    {
        if (tasks.ContainsKey(taskID))
        {
            Task task = tasks[taskID];
            task.Complete();
            if (currentTask == task)
            {
                currentTask = null;
            }
            OnTaskCompleted?.Invoke(task);
        }
        else
        {
            Debug.LogWarning($"Task with ID '{taskID}' not found.");
        }
    }

    /// <summary>
    /// Updates progress of a task by its TaskID.
    /// </summary>
    /// <param name="taskID">The ID of the task to update.</param>
    /// <param name="amount">The amount to increase progress by.</param>
    public void UpdateTaskProgress(string taskID, int amount)
    {
        if (tasks.ContainsKey(taskID))
        {
            Task task = tasks[taskID];
            task.UpdateProgress(amount);
            OnTaskProgressed?.Invoke(task);
            if(task.Status == TaskStatus.Completed) OnTaskCompleted?.Invoke(task);
        }
        else
        {
            Debug.LogWarning($"Task with ID '{taskID}' not found.");
        }
    }

    /// <summary>
    /// Retrieves a task by its TaskID.
    /// </summary>
    /// <param name="taskID">The ID of the task.</param>
    /// <returns>The Task object, or null if not found.</returns>
    public Task GetTask(string taskID)
    {
        if (tasks.ContainsKey(taskID))
        {
            return tasks[taskID];
        }
        else
        {
            Debug.LogWarning($"Task with ID '{taskID}' not found.");
            return null;
        }
    }

    /// <summary>
    /// Retrieves the current active task.
    /// </summary>
    /// <returns>The current Task, or null if none.</returns>
    public Task GetCurrentTask()
    {
        return currentTask;
    }

    /// <summary>
    /// Retrieves all tasks.
    /// </summary>
    /// <returns>A list of all Task objects.</returns>
    public List<Task> GetAllTasks()
    {
        return new List<Task>(tasks.Values);
    }

    /// <summary>
    /// Resets a task by its TaskID.
    /// </summary>
    /// <param name="taskID">The ID of the task to reset.</param>
    public void ResetTask(string taskID)
    {
        if (tasks.ContainsKey(taskID))
        {
            Task task = tasks[taskID];
            task.ResetTask();
            if (currentTask == task)
            {
                currentTask = null;
            }
        }
        else
        {
            Debug.LogWarning($"Task with ID '{taskID}' not found.");
        }
    }
}
