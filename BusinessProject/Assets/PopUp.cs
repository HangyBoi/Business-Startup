using UnityEngine;

public class MapOverlayController : MonoBehaviour
{
    [SerializeField] private GameObject mapOverlay; // The map UI overlay (including background).
    [SerializeField] private PlayerMovement playerController; // Reference to your player controller script.

    private bool isMapOpen = false; // Tracks if the map is currently open.

    private void Update()
    {
        // Toggle the map overlay when the "M" key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMapOverlay();
        }
    }

    public void ToggleMapOverlay()
    {
        // Toggle the active state of the map overlay
        isMapOpen = !isMapOpen;
        mapOverlay.SetActive(isMapOpen);

        if (isMapOpen)
        {
            // Unlock and show the cursor for UI interaction
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Pause the game (optional)
            Time.timeScale = 0f;

            // Disable player movement
            if (playerController != null)
            {
                playerController.SetCanMove(false);
            }
        }
        else
        {
            // Lock and hide the cursor for gameplay
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Resume the game (optional)
            Time.timeScale = 1f;

            // Enable player movement
            if (playerController != null)
            {
                playerController.SetCanMove(true);
            }
        }
    }
}
