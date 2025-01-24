using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraConroller : MonoBehaviour
{
    [SerializeField] private GameObject target;
    
    [Range(0f, 90f)] [SerializeField] private float cameraPitch;
    [Range(-30f, 5f)] [SerializeField] private float zCameraOffset;
    [Range(-5f, 5f)] [SerializeField] private float yCameraOffset;
    [SerializeField] [Range(300f, 600f)] private float horizontalSensitivity = 500f;
    
    private float yRotation = 0f;
    [Range(45f, 90f)] [SerializeField] private float rotationStep = 45f;
    private float mouseXAccum = 0f;
    private float rotationThreshold = 5f; // Adjust as needed
    private bool isRotating = false;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    private void Update()
    {
        if(target) RotateCamera();
    }

    void RotateCamera()
    {
        // Accumulate mouse movement
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        mouseXAccum += mouseX;

        // Check if accumulated movement exceeds the threshold
        if (!isRotating)
        {
            if (mouseXAccum >= rotationThreshold)
            {
                yRotation += rotationStep;
                isRotating = true;
                mouseXAccum = 0f; // Reset accumulator
            }
            else if (mouseXAccum <= -rotationThreshold)
            {
                yRotation -= rotationStep;
                isRotating = true;
                mouseXAccum = 0f; // Reset accumulator
            }
        }
        else
        {
            // Optional: Implement a cooldown or wait until mouse stops moving
            if (Mathf.Abs(mouseXAccum) < rotationThreshold / 2)
            {
                isRotating = false;
            }
        }

        // Ensure yRotation stays within 0-360 degrees
        yRotation = Mathf.Repeat(yRotation, 360f);

        // Create the new rotation
        Quaternion newRotation = Quaternion.Euler(cameraPitch, yRotation, 0);
        
        // Calculate the new position based on the rotation and offsets
        Vector3 newDistanceVec = new Vector3(0.0f, yCameraOffset, zCameraOffset);
        Vector3 newPosition = newRotation * newDistanceVec + target.transform.position;

        // Apply rotation and position to the camera
        transform.rotation = newRotation;
        transform.position = newPosition;
    }
}
