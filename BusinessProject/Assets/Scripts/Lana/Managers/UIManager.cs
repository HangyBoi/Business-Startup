using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager { get; private set; }

    [Tooltip("Prefab for the interaction popup UI")]
    public GameObject popupPrefab; // Assign your popup prefab in the Inspector

    private GameObject currentPopup;

    private void Awake()
    {
        //Singleton
        if (uiManager == null) uiManager = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        // Subscribe to InteractionManager events
        if (InteractionManager.interactionManager != null)
        {
            InteractionManager.interactionManager.OnInteractableDetected += HandleInteractableDetected;
            InteractionManager.interactionManager.OnInteractableExited += HandleInteractableExited;
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
    }

    /// <summary>
    /// Handles the event when an interactable is detected.
    /// </summary>
    /// <param name="interactable">The detected interactable object.</param>
    private void HandleInteractableDetected(Interactable interactable)
    {
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
    /// Creates and displays the interaction popup.
    /// </summary>
    /// <param name="text">The text to display in the popup.</param>
    /// <param name="worldPosition">The world position of the interactable.</param>
    /// <param name="icon">Optional icon to display.</param>
    public void ShowPopup(string text, Vector3 worldPosition, Sprite icon = null)
    {
        // Destroy existing popup if any
        if (currentPopup != null) Destroy(currentPopup);

        // Instantiate a new popup
        currentPopup = Instantiate(popupPrefab, transform);

        // Convert world position to screen position for UI placement
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        currentPopup.GetComponent<RectTransform>().position = screenPos;

        // Set the popup text
        Text popupText = currentPopup.GetComponentInChildren<Text>();
        if (popupText != null)
        {
            popupText.text = text;
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

    /// <summary>
    /// Optionally, displays a simple message (e.g., for failed interactions).
    /// </summary>
    /// <param name="message">The message to display.</param>
    public void ShowMessage(string message)
    {
        // Implement message display logic (e.g., temporary popup)
        // This can be similar to ShowPopup but with a different prefab or logic
        Debug.Log("UIManager ShowMessage: " + message);
    }
}
