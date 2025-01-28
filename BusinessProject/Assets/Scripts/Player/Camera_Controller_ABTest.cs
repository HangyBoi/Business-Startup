using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_ABTest : MonoBehaviour

{
    [SerializeField] private GameObject target;

    [Range(0f, 90f)][SerializeField] private float cameraPitch;
    [Range(-30f, 5f)][SerializeField] private float zCameraOffset;
    [Range(-5f, 5f)][SerializeField] private float yCameraOffset;
    [SerializeField][Range(300f, 600f)] private float horizontalSensitivity = 500f;

    private float yRotation = 0f;

    //personally added
    [SerializeField] private float rotationAmmount = 90f;
    [SerializeField] private float smoothSpeed = 5f;
    public bool lockedCam = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    private void Update()
    {
        if (target) RotateCamera();
    }

    void RotateCamera()
    {
        //Free cam

        if (!lockedCam)
        {
            float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
            yRotation += mouseX;

            Quaternion newRotation = Quaternion.Euler(cameraPitch, yRotation, 0);
            Vector3 newDistanceVec = new Vector3(0.0f, yCameraOffset, zCameraOffset);
            Vector3 newPosition = newRotation * newDistanceVec;
            transform.rotation = newRotation;
            transform.position = newPosition + target.transform.position;
        }



        //Locked Cam

        if (lockedCam)
        {
            //personally added
            if (Input.GetKeyDown(KeyCode.E))
            {
                yRotation += rotationAmmount;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                yRotation -= rotationAmmount;
            }

            Quaternion targetRotation = Quaternion.Euler(cameraPitch, yRotation, 0);
            Vector3 targetDistanceVec = new Vector3(0.0f, yCameraOffset, zCameraOffset);
            Vector3 targetPosition = targetRotation * targetDistanceVec + target.transform.position;
            // Smoothly interpolate to the target position and rotation
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
    }
}