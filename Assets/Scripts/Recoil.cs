using UnityEngine;

public class Recoil : MonoBehaviour
{
    [SerializeField] PlayerWeaponController pwc;

    Vector3 currentRotation;
    Vector3 targetRotation;

    //hipfire recoil
    public float recoilX;
    public float recoilY;
    public float recoilZ;

    //aim recoil
    public float aimRecoilX;
    public float aimRecoilY;
    public float aimRecoilZ;

    //settings
    public float snappiness;
    public float returnspeed;

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnspeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        if (pwc.currentWeapon.isAiming)
            targetRotation += new Vector3(aimRecoilX, Random.Range(-aimRecoilY, aimRecoilY), Random.Range(-aimRecoilZ, aimRecoilZ));
        else
            targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
