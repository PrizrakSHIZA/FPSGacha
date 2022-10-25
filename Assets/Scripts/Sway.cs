using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float intesity = 1f;
    public float smooth = 1f;

    Quaternion originRotation;

    private void Start()
    {
        originRotation = transform.localRotation;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Quaternion adjestmentX = Quaternion.AngleAxis(-intesity * mouseX, Vector3.up);
        Quaternion adjestmentY = Quaternion.AngleAxis(intesity * mouseY, Vector3.right);
        Quaternion adjestmentZ = Quaternion.AngleAxis((intesity * mouseX)*0.5f, Vector3.forward);

        Quaternion targetRotation = originRotation * adjestmentX * adjestmentY * adjestmentZ;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
}
