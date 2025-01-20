using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody playerRigidbody;
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Create movement vector
        Vector3 moveVector = new Vector3(horizontal, 0, vertical).normalized;
        if (moveVector == Vector3.zero)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
        
        //playerRigidbody.velocity = newVelocity;
        transform.Translate(relativeMoveDirection * moveSpeed * Time.deltaTime, Space.World);
        
        // Rotate the player to face the relative movement direction
        float targetAngle = Mathf.Atan2(relativeMoveDirection.x, relativeMoveDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        //
        // // Rotate the player to face movement direction, including diagonals
        // float angle = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg; // Angle in degrees
        // float snappedAngle = Mathf.Round(angle / 45f) * 45f;
        //transform.rotation = Quaternion.Euler(0, snappedAngle, 0);
        transform.rotation = targetRotation;

    }
}
