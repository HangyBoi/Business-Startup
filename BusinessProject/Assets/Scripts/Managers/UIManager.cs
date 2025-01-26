using UnityEngine;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager { get; private set; }

    [SerializeField] private InteractableUI interactibleUiController;
    [SerializeField] private TasksUI taskUiController;
    [SerializeField] private InventoryUI invUiController;
    public MapUI mapUiController;

    public GameObject currentUIElement;

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
        if (invUiController && Inventory.inventory != null)
        {
            Inventory.ItemAddedToInventory += invUiController.ReinitializeInventory;
        }
        else
        {
            Debug.LogError("Inventory is null!");
        }
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
