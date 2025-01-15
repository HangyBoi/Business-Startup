using System;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private Camera mainCam;
    
    [SerializeField] private bool freezeXZAxis = false;

    private void Start()
    {
        if(Camera.main) mainCam = Camera.main;
        else
        {
            Debug.LogWarning("No main Camera!");
        }
    }

    private void Update()
    {
        if (mainCam)
        {
            if(freezeXZAxis) transform.rotation = Quaternion.Euler(0f, mainCam.transform.rotation.eulerAngles.y, 0f);
            else
            {
                transform.rotation = mainCam.transform.rotation;
            }
        }
    }
}
