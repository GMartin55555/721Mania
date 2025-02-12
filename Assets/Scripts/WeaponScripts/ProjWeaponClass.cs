using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ProjWeaponClass : MonoBehaviour
{
    public GameObject projectile;
    public float shootForce, upwardForce;
    [Header("Fire Rate")]
    public float timeBetweenShooting, spread, reloadTime;
    [Header("Rate of Burst")]
    public float timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public int bulletsLeft, bulletsShot;

    public bool readyToShoot, reloading;

    public Camera fpsCamera;
    public Transform attackPoint;
    
    public bool allowInvoke = true;

    public float damage;

    [Header("InputActions")]
    public InputActionReference fireAction;
    public InputActionReference reloadAction;
    public InputActionReference altFireAction;

    [Header("UI")]
    [SerializeField] public TMP_Text ammoCount;
    public RawImage gunSprite;
    [Header("Gun Slot")]
    public int gunSlot;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        ammoCount.text = bulletsLeft.ToString();
    }

    private void OnEnable()
    {
        fireAction.action.started += ShootInput;
        reloadAction.action.started += ReloadInput;

        bulletsLeft = magazineSize;
        readyToShoot = true;
        gunSprite.gameObject.SetActive(true);
        ammoCount.gameObject.SetActive(true);
        ammoCount.text = bulletsLeft.ToString();
    }

    private void OnDisable()
    {
        fireAction.action.started -= ShootInput;
        reloadAction.action.started -= ReloadInput;
        gunSprite.gameObject.SetActive(false);
        ammoCount.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (bulletsLeft <= 0)
        {
            Reload();
        }

        if(readyToShoot && !reloading && bulletsLeft > 0 && allowButtonHold && fireAction.action.ReadValue<float>() == 1)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    public void ShootInput(InputAction.CallbackContext context)
    {    
        if (readyToShoot && !reloading && bulletsLeft > 0 && !allowButtonHold)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    public void ReloadInput(InputAction.CallbackContext context)
    {
        Reload();
    }

    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(projectile, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.up * upwardForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;
        
        ammoCount.text = (bulletsLeft/bulletsPerTap).ToString();

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        ammoCount.text = "???";
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        ammoCount.text = bulletsLeft.ToString();
        reloading = false;
    }
}
