using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [Range(0f, 90f)][SerializeField] private float cameraPitch = 20f;
    [Range(-30f, 5f)][SerializeField] private float zCameraOffset = -5f;
    [Range(-5f, 5f)][SerializeField] private float yCameraOffset = 2f;
    [Range(45, 90f)][SerializeField] private float rotationStep = 90f;

    private bool isRotating = false;
    [SerializeField] private float rotationDuration = 0.2f; // Duration of the rotation animation

    private float yRotation = 0f;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        if (target != null)
        {
            // Initialize yRotation based on current camera rotation
            yRotation = transform.eulerAngles.y;
        }
    }

    private void Update()
    {
        if (target)
        {
            HandleRotationInput();
            UpdateCameraPosition();
        }
    }

    private void HandleRotationInput()
    {
        if (isRotating) return; // Prevent new rotations while one is in progress

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(RotateCameraCoroutine(rotationStep));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(RotateCameraCoroutine(-rotationStep));
        }
    }

    private IEnumerator RotateCameraCoroutine(float step)
    {
        isRotating = true;
        float startRotation = yRotation;
        float targetRotation = NormalizeAngle(yRotation + step);
        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / rotationDuration);
            yRotation = Mathf.LerpAngle(startRotation, targetRotation, t);
            yield return null;
        }

        yRotation = targetRotation;
        isRotating = false;
    }

    private void UpdateCameraPosition()
    {
        // Create the new rotation
        Quaternion newRotation = Quaternion.Euler(cameraPitch, yRotation, 0);

        // Calculate the new position based on the rotation and offsets
        Vector3 newDistanceVec = new Vector3(0.0f, yCameraOffset, zCameraOffset);
        Vector3 newPosition = newRotation * newDistanceVec + target.transform.position;

        // Apply rotation and position to the camera
        transform.rotation = newRotation;
        transform.position = newPosition;
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0f)
            angle += 360f;
        return angle;
    }
}
