using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField] private GameObject mapPrefab;
    // Start is called before the first frame update
    public void ShowMap()
    {
        //Debug.Log("Show Map");

        if (mapPrefab == null)
        {
            Debug.LogError("Map Prefab is not assigned in UIManager.");
            return;
        }

        UIManager.uiManager.CleanScreen();
        
        UIManager.uiManager.currentUIElement = Instantiate(mapPrefab, transform);

        if (UIManager.uiManager.currentUIElement == null)
        {
            Debug.LogError("Failed to instantiate mapPrefab.");
            return;
        }

        // Set the popup's RectTransform to the center of the Canvas
        RectTransform popupRect =  UIManager.uiManager.currentUIElement.GetComponent<RectTransform>();
        if (popupRect != null)
        {
            // // Option 1: Using anchoredPosition
            // popupRect.anchoredPosition = Vector2.zero;

            // Option 2: If you prefer setting the position in screen space
            popupRect.position = new Vector3(Screen.width / 2, Screen.height / 2 , popupRect.position.z);
        }
        else
        {
            Debug.LogWarning("Map Prefab does not have a RectTransform component.");
        }
    }
}
