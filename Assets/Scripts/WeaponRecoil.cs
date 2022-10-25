using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public float force = 2f;
    public float returnspeed = 2f;
    public float snappiness = 6f;

    Vector3 targetPosition;
    Vector3 currentPosition;

    void Update()
    {
        targetPosition = Vector3.Lerp(targetPosition, Vector3.zero, returnspeed * Time.deltaTime);
        currentPosition = Vector3.Slerp(currentPosition, targetPosition, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentPosition);
    }

    public void Recoil()
    {
        targetPosition += new Vector3(-force, 0, 0);
    }
}
