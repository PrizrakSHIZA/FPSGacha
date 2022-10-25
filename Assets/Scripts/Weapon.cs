using DG.Tweening;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float force = 30f;
    public bool rapidFire = false;

    public LayerMask layerMask;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    Camera mainCam;
    float nextTimeToFire = 0f;

    [Header("Aiming")]
    Transform weaponHolder;
    Transform aimPosition;

    public bool isAiming = false;

    PlayerMovement playerMovement;
    Vector3 originalPosition;


    [Header("Recoil")]
    public Recoil recoilScript;
    public WeaponRecoil weaponRecoil;
    
    private void Start()
    {
        // Aiming
        weaponHolder = PlayerWeaponController.Singleton.weaponHolder;
        aimPosition = PlayerWeaponController.Singleton.aimPosition;

        // Recoil
        playerMovement = PlayerWeaponController.Singleton.playerMovement;
        originalPosition = weaponHolder.localPosition;
        mainCam = Camera.main;
        recoilScript = PlayerWeaponController.Singleton.Recoil;
    }

    void Update()
    {
        //hold fire
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && rapidFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        // pressed fire
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && !rapidFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        Aim();
    }

    void Shoot()
    {
        recoilScript.RecoilFire();
        weaponRecoil.Recoil();

        muzzleFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range, layerMask))
        {
            Target target = hit.transform.GetComponent<Target>();
            
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }

            GameObject temp = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(temp, 1f);
        }
    }

    void Aim()
    {
        if (Input.GetMouseButtonDown(1))
        {
            weaponHolder.DOLocalMove(aimPosition.localPosition, .5f);
            mainCam.DOFieldOfView(30, .5f);
            playerMovement.speed -= 3;
            isAiming = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            weaponHolder.DOLocalMove(originalPosition, .5f);
            mainCam.DOFieldOfView(60, .5f);
            playerMovement.speed += 3;
            isAiming = false;
        }
    }
}
