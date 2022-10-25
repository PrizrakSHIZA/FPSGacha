using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class KeepFOV : MonoBehaviour
{
    Camera cam;
    Camera mainCam;
    float lastFov;

    private void Start()
    {
        cam = GetComponent<Camera>();
        mainCam = Camera.main;
        lastFov = mainCam.fieldOfView;
    }

    void Update()
    {
        if (mainCam.fieldOfView != lastFov)
        {
            lastFov = mainCam.fieldOfView;
            cam.fieldOfView = lastFov;
        }
    }
}
