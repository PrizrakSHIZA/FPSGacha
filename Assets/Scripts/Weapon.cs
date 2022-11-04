using DG.Tweening;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Shoot settings")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float force = 30f;
    public bool rapidFire = false;

    [Header("Elemental settings")]
    [SerializeField] ElementalDamage.ElementalType damageType;
    [SerializeField] float elementalForce;

    [Header("Visual settings")]
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


    [Header("Camera Recoil")]
    //hipfire recoil
    [SerializeField] float recoilX;
    [SerializeField] float recoilY;
    [SerializeField] float recoilZ;
    //aim recoil
    [SerializeField] float aimRecoilX;
    [SerializeField] float aimRecoilY;
    [SerializeField] float aimRecoilZ;
    //settings
    [SerializeField] float snappiness;
    [SerializeField] float returnspeed;

    Recoil recoilScript;

    [Header("Weapon Recoil")]
    public WeaponRecoil weaponRecoil;
    public float recoilForce = 2f;
    public float weaponReturnSpeed = 2f;
    public float weaponSnappiness = 6f;

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
        //sewt camera recoil variables
        recoilScript.recoilX = recoilX;
        recoilScript.recoilY = recoilY;
        recoilScript.recoilZ = recoilZ;
        recoilScript.aimRecoilX = aimRecoilX;
        recoilScript.aimRecoilY = aimRecoilY;
        recoilScript.aimRecoilZ = aimRecoilZ;
        recoilScript.snappiness = snappiness;
        recoilScript.returnspeed = returnspeed;
        //set weapon recoil veriables
        weaponRecoil.force = recoilForce;
        weaponRecoil.returnspeed = weaponReturnSpeed;
        weaponRecoil.snappiness = weaponSnappiness;
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
                target.TakeDamage(damage, elementalForce, damageType);
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
