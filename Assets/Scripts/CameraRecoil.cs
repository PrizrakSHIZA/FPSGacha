using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    public PlayerWeaponController pwc;

    public float rotationSpeed = 6f;
    public float returnSpeed = 25f;

    public Vector3 recoilRotation = new Vector3(2f, 2f, 2f);
    public Vector3 recoilRotationAiming = new Vector3(.5f, .5f, 1.5f);

    Vector3 currentRotation;
    Vector3 rotation;

    private void FixedUpdate()
    {
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        rotation = Vector3.Slerp(rotation, currentRotation, rotationSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(rotation);
    }

    public void Recoil()
    {
        if (pwc.currentWeapon.isAiming)
        {
            currentRotation += new Vector3(-recoilRotationAiming.x, Random.Range(-recoilRotationAiming.y, recoilRotationAiming.y), 
                Random.Range(-recoilRotationAiming.z, recoilRotationAiming.z));
        }
        else
        {
            currentRotation += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), 
                Random.Range(-recoilRotation.z, recoilRotation.z));
        }
    }

    /*
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Recoil();
        }
    }*/
}
