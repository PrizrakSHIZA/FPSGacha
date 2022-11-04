using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerWeaponController : MonoBehaviour
{
    public static PlayerWeaponController Singleton;

    //Links
    public Transform weaponHolder;
    public Transform aimPosition;
    public PlayerMovement playerMovement;
    public Recoil Recoil;

    //Weapon part
    public Weapon currentWeapon;


    private void Awake()
    {
        Singleton = this;
    }
}
