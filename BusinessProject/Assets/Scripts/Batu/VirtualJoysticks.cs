using UnityEngine;

public class MovementJoystick : MonoBehaviour
{
    public RectTransform moveJoystick;           // Joystick handle
    public RectTransform moveJoystickBackground; // Joystick background
    public float moveJoystickRange = 50f;        // Max range of the joystick handle
    public float smoothSpeed = 10f;              // Speed of the smoothing effect

    private Vector2 currentJoystickPosition;     // Current interpolated position

    void Update()
    {
        // Get input from Horizontal and Vertical axes
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveInput != Vector2.zero)
        {
            // Normalize and scale input to joystick range
            Vector2 targetPosition = moveInput.normalized * moveJoystickRange;

            // Smoothly interpolate to the target position
            currentJoystickPosition = Vector2.Lerp(currentJoystickPosition, targetPosition, Time.deltaTime * smoothSpeed);

            // Update joystick position
            moveJoystick.anchoredPosition = currentJoystickPosition;
        }
        else
        {
            // Smoothly return the joystick to center when no input
            currentJoystickPosition = Vector2.Lerp(currentJoystickPosition, Vector2.zero, Time.deltaTime * smoothSpeed);
            moveJoystick.anchoredPosition = currentJoystickPosition;
        }
    }
}
