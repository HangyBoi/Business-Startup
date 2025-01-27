using System;
using UnityEngine;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager { get; private set; }

    private InteractableUI interactibleUiController;
    private TasksUI taskUiController;
    private InventoryUI invUiController;
    [NonSerialized] public MapUI mapUiController;

    [NonSerialized] public GameObject currentUIElement;

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
        interactibleUiController = GetComponent<InteractableUI>();
        if (interactibleUiController && InteractionManager.interactionManager != null)
        {
            InteractionManager.interactionManager.OnInteractableDetected += interactibleUiController.HandleInteractableDetected;
            InteractionManager.interactionManager.OnInteractableExited += interactibleUiController.HandleInteractableExited;
        }
        else
        {
            Debug.LogError("Interaction Manager is null!");
        }
        
        // Subscribe to TaskManager events
        taskUiController = GetComponent<TasksUI>();
        if (taskUiController && TaskManager.taskManager != null)
        {
            TaskManager.OnTaskAccepted += taskUiController.HandleTaskAccepted;
            TaskManager.OnTaskProgressed += taskUiController.HandleTaskProgressed;
            TaskManager.OnTaskCompleted += taskUiController.HandleTaskCompleted;
        }
        else
        {
            Debug.LogError("Task Manager is null!");
        }
        
        // Subscribe to Inventory events
        invUiController = GetComponent<InventoryUI>();
        if (invUiController && Inventory.inventory != null)
        {
            Inventory.ItemAddedToInventory += invUiController.ReinitializeInventory;
        }
        else
        {
            Debug.LogError("Inventory is null!");
        }
        
        //Init map controller
        mapUiController = GetComponent<MapUI>();
    }

    private void OnDisable()
    {
        if (interactibleUiController && InteractionManager.interactionManager != null)
        {
            InteractionManager.interactionManager.OnInteractableDetected -= interactibleUiController.HandleInteractableDetected;
            InteractionManager.interactionManager.OnInteractableExited -= interactibleUiController.HandleInteractableExited;
        }

        if (taskUiController && TaskManager.taskManager != null)
        {
            TaskManager.OnTaskAccepted -= taskUiController.HandleTaskAccepted;
            TaskManager.OnTaskProgressed -= taskUiController.HandleTaskProgressed;
            TaskManager.OnTaskCompleted -= taskUiController.HandleTaskCompleted;
        }
        
        if (invUiController && Inventory.inventory != null)
        {
            Inventory.ItemAddedToInventory -= invUiController.ReinitializeInventory;
        }
    }
    
    /// <summary>
    /// Hides and destroys the current interaction popup.
    /// </summary>
    public void CleanScreen()
    {
        if (currentUIElement != null)
        {
            Destroy(currentUIElement);
            currentUIElement = null;
        }
    }
}
