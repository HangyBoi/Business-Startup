using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody playerRigidbody;
    private Animator animator;

    public bool isActive = true;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on PlayerMovement.");
        }
    }

    private void FixedUpdate()
    {
        if(isActive) Move();
    }

    private void Move()
    {
        // Get input axes
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Create movement vector
        Vector3 moveVector = new Vector3(horizontal, 0, vertical).normalized;

        // Calculate speed for Animator
        float currentSpeed = moveVector.magnitude * moveSpeed;

        // Update Animator's speed parameter
        if (animator != null)
        {
            animator.SetFloat("Speed", currentSpeed);
        }

        if (moveVector == Vector3.zero)
        {
            playerRigidbody.velocity = Vector3.zero;
            return;
        }

        // Convert input vector to camera-relative movement
        Vector3 cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;

        // Calculate the relative movement direction
        Vector3 relativeMoveDirection = (cameraRight * moveVector.x + cameraForward * moveVector.z).normalized;

        // Apply movement
        Vector3 newVelocity = relativeMoveDirection * moveSpeed;
        newVelocity.y = playerRigidbody.velocity.y;

        playerRigidbody.velocity = newVelocity;

        // Rotate the player to face the relative movement direction
        float targetAngle = Mathf.Atan2(relativeMoveDirection.x, relativeMoveDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }
    
}
