using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    
    public void HandleInteractableDetected(Interactable interactable)
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

        ShowPopup(interactable.GetPopupText(), interactable.interactionData.icon);
    }


    /// <summary>
    /// Handles the event when an interactable is exited.
    /// </summary>
    /// <param name="interactable">The exited interactable object.</param>
    public void HandleInteractableExited(Interactable interactable)
    {
        UIManager.uiManager.CleanScreen();
    }
    
    public void ShowPopup(string text, Sprite icon = null)
    {
        if (popupPrefab == null)
        {
            Debug.LogError("Popup Prefab is not assigned in InteractableUIManager.");
            return;
        }
        //Debug.Log("Show Popup");

        UIManager.uiManager.CleanScreen();

        // Instantiate a new popup as a child of the Canvas
        UIManager.uiManager.currentUIElement = Instantiate(popupPrefab, transform);

        if (UIManager.uiManager.currentUIElement == null)
        {
            Debug.LogError("Failed to instantiate popupPrefab.");
            return;
        }

        // Set the popup's RectTransform to the center of the Canvas
        RectTransform popupRect = UIManager.uiManager.currentUIElement.GetComponent<RectTransform>();
        if (popupRect != null)
        {
            // // Option 1: Using anchoredPosition
            // popupRect.anchoredPosition = Vector2.zero;

            // Option 2: If you prefer setting the position in screen space
            popupRect.position = new Vector3(Screen.width / 2 + 130, Screen.height / 2 - 55, popupRect.position.z);
        }
        else
        {
            Debug.LogWarning("Popup Prefab does not have a RectTransform component.");
        }

        // Set the popup text
        TextMeshProUGUI popupTextComponent = UIManager.uiManager.currentUIElement.GetComponentInChildren<TextMeshProUGUI>();
        if (popupTextComponent != null)
        {
            popupTextComponent.text = text;
        }
        else
        {
            Debug.LogWarning("Popup Prefab does not have a TextMeshProUGUI component.");
        }

        // Set the popup icon, if any
        Image popupIcon = UIManager.uiManager.currentUIElement.transform.Find("Icon")?.GetComponent<Image>();
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
}
