using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraConroller : MonoBehaviour
{
    public float topClamp = -90f;
    public float bottomClamp = 90f;
    public float verticalSensitivity = 500f;
    private float xRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}