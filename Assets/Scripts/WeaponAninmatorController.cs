using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponAninmatorController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerWeaponController pwc;

    Animator baseAnimator;

    void Start()
    {
        baseAnimator = GetComponent<Animator>();    
    }

    void Update()
    {
        //changing animation speed for run animation
        baseAnimator.SetFloat("Speed", playerMovement.speed / 8);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            baseAnimator.SetBool("Moving", true);
        else
            baseAnimator.SetBool("Moving", false);

        //set aim bool
        baseAnimator.SetBool("Aim", pwc.currentWeapon.isAiming);
    }
}
