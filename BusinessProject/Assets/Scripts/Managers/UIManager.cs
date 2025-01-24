// UIManager.cs
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager { get; private set; }

    [Tooltip("Prefab for the interaction popup UI")]
    public GameObject popupPrefab; // Assign your popup prefab in the Inspector

    [SerializeField] private TextMeshProUGUI taskTitleText;
    [SerializeField] private TextMeshProUGUI taskProgressionText;

    private GameObject currentPopup;

    private void Awake()
    {
        // Singleton Pattern
        if (uiManager == null)
        {
            uiManager = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Subscribe to InteractionManager events
        if (InteractionManager.interactionManager != null)
        {
            InteractionManager.interactionManager.OnInteractableDetected += HandleInteractableDetected;
            InteractionManager.interactionManager.OnInteractableExited += HandleInteractableExited;
        }
        else
        {
            Debug.LogError("Interaction Manager is null!");
        }

        // Subscribe to TaskManager events
        if (TaskManager.taskManager != null)
        {
            TaskManager.OnTaskAccepted += HandleTaskAccepted;
            TaskManager.OnTaskProgressed += HandleTaskProgressed;
            TaskManager.OnTaskCompleted += HandleTaskCompleted;

            // Delay the test call to ensure tasks are initialized
            Invoke(nameof(TestAcceptTask), 0.1f);
        }
        else
        {
            Debug.LogError("Task Manager is null!");
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from InteractionManager events
        if (InteractionManager.interactionManager != null)
        {
            InteractionManager.interactionManager.OnInteractableDetected -= HandleInteractableDetected;
            InteractionManager.interactionManager.OnInteractableExited -= HandleInteractableExited;
        }

        if (TaskManager.taskManager != null)
        {
            TaskManager.OnTaskAccepted -= HandleTaskAccepted;
            TaskManager.OnTaskProgressed -= HandleTaskProgressed;
            TaskManager.OnTaskCompleted -= HandleTaskCompleted;
        }
    }

    /// <summary>
    /// Delayed test method to accept a task after initialization.
    /// </summary>
    private void TestAcceptTask()
    {
        string testTaskID = "0"; // Ensure this ID exists or use a valid one
        if (TaskManager.taskManager.GetTask(testTaskID) != null)
        {
            TaskManager.taskManager.AcceptTask(testTaskID);
        }
        else
        {
            Debug.LogWarning($"Test Task with ID '{testTaskID}' does not exist.");
        }
    }

    /// <summary>
    /// Handles the event when an interactable is detected.
    /// </summary>
    /// <param name="interactable">The detected interactable object.</param>
    private void HandleInteractableDetected(Interactable interactable)
    {
        if (interactable == null)
        {
            Debug.LogError("HandleInteractableDetected called with a null interactable.");
            return;
        }

        if (interactable.interactionData == null)
        {
            Debug.LogError($"Interactable '{interactable.name}' has no InteractionData assigned.");
            return;
        }

        if (interactable.interactionData.icon == null)
        {
            Debug.LogWarning($"Interactable '{interactable.name}' InteractionData has no icon assigned. Popup will be shown without an icon.");
        }

        ShowPopup(interactable.GetPopupText(), interactable.GetUIPosition(), interactable.interactionData.icon);
    }


    /// <summary>
    /// Handles the event when an interactable is exited.
    /// </summary>
    /// <param name="interactable">The exited interactable object.</param>
    private void HandleInteractableExited(Interactable interactable)
    {
        HidePopup();
    }

    /// <summary>
    /// Handles the event when a task is accepted.
    /// </summary>
    /// <param name="task">The accepted task.</param>
    private void HandleTaskAccepted(Task task)
    {
        Debug.Log($"UIManager: Task '{task.Data.Title}' accepted.");
        taskTitleText.text = task.Data.Title;
        taskProgressionText.text = $"{task.CurrentProgress}/{task.Data.RequiredProgress}";
        // Additional UI updates can be handled here
    }

    /// <summary>
    /// Handles the event when a task is progressed.
    /// </summary>
    /// <param name="task">The progressed task.</param>
    private void HandleTaskProgressed(Task task)
    {
        Debug.Log($"UIManager: Task '{task.Data.Title}' progressed.");
        taskProgressionText.text = $"{task.CurrentProgress}/{task.Data.RequiredProgress}";
        // Additional UI updates can be handled here
    }

    /// <summary>
    /// Handles the event when a task is completed.
    /// </summary>
    /// <param name="task">The completed task.</param>
    private void HandleTaskCompleted(Task task)
    {
        Debug.Log($"UIManager: Task '{task.Data.Title}' completed.");
        taskProgressionText.text = "Completed!";
    }

    /// <summary>
    /// Creates and displays the interaction popup.
    /// </summary>
    /// <param name="text">The text to display in the popup.</param>
    /// <param name="worldPosition">The world position of the interactable.</param>
    /// <param name="icon">Optional icon to display.</param>
    public void ShowPopup(string text, Vector3 worldPosition, Sprite icon = null)
    {
        Debug.Log("Show Popup");

        if (popupPrefab == null)
        {
            Debug.LogError("Popup Prefab is not assigned in UIManager.");
            return;
        }

        // Destroy existing popup if any
        if (currentPopup != null) Destroy(currentPopup);

        // Instantiate a new popup as a child of the Canvas
        currentPopup = Instantiate(popupPrefab, transform);

        if (currentPopup == null)
        {
            Debug.LogError("Failed to instantiate popupPrefab.");
            return;
        }

        // Convert world position to screen position for UI placement
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found. Ensure there is a camera tagged as 'MainCamera' in the scene.");
            return;
        }

        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        currentPopup.GetComponent<RectTransform>().position = screenPos;

        // Set the popup text
        TextMeshProUGUI popupTextComponent = currentPopup.GetComponentInChildren<TextMeshProUGUI>();
        if (popupTextComponent != null)
        {
            popupTextComponent.text = text;
        }
        else
        {
            Debug.LogWarning("Popup Prefab does not have a TextMeshProUGUI component.");
        }

        // Set the popup icon, if any
        Image popupIcon = currentPopup.transform.Find("Icon")?.GetComponent<Image>();
        if (popupIcon != null)
        {
            if (icon != null)
            {
                popupIcon.sprite = icon;
                popupIcon.enabled = true;
            }
            else
            {
                popupIcon.enabled = false;
            }
        }
        else
        {
            Debug.LogWarning("Popup Prefab does not have an Image component named 'Icon'.");
        }
    }


    /// <summary>
    /// Hides and destroys the current interaction popup.
    /// </summary>
    public void HidePopup()
    {
        if (currentPopup != null)
        {
            Destroy(currentPopup);
            currentPopup = null;
        }
    }
    
    // public void DisplayCurrentTask()
    // {
    //     Task currentTask = TaskManager.taskManager.GetCurrentTask();
    //     if (currentTask != null)
    //     {
    //         // Update UI elements with current task details
    //         taskTitleText.text = currentTask.Data.Title;
    //         taskProgressionText.text = $"{currentTask.CurrentProgress}/{currentTask.Data.RequiredProgress}";
    //         // Uncomment and set up a Progress Bar if desired
    //         // taskProgressBar.value = (float)currentTask.CurrentProgress / currentTask.Data.RequiredProgress;
    //     }
    //     else
    //     {
    //         // Clear or hide UI elements when no task is active
    //         taskTitleText.text = "No active tasks";
    //         taskProgressionText.text = "";
    //         // taskProgressBar.value = 0f;
    //     }
    // }
}
