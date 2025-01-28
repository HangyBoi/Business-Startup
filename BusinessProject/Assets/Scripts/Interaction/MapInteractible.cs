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
    }
}