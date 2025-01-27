using UnityEngine;

public class MapInteractable : Interactable
{
    protected override void Handle()
    {
        Debug.Log("Handle Map");
        
        // Ensure UIManager and MapUiController are accessible
        if (UIManager.uiManager.mapUiController)
        {
            UIManager.uiManager.mapUiController.ShowMap();
        }
        else
        {
            Debug.LogError("MapUiController not found in UIManager.");
        }

        // Accept a specific task, adjust as needed
        TestAcceptTask();
    }

    /// <summary>
    /// Delayed test method to accept a task after initialization.
    /// </summary>
    private void TestAcceptTask()
    {
        TaskManager.taskManager.AcceptTask("0"); // Ensure "0" corresponds to the desired TaskID
    }
}