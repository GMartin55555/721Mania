using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ProjWeaponClass : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootForce, upwardForce;
    [Header("Fire Rate")]
    [SerializeField] private float timeBetweenShooting, spread, reloadTime;
    [Header("Rate of Burst")]
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowButtonHold;

    private int bulletsLeft, bulletsShot;

    private bool readyToShoot, reloading;

    [SerializeField] private Camera fpsCamera;
    [SerializeField] private Transform attackPoint;
    
    [SerializeField] private bool allowInvoke = true;

    public int damage;

    [Header("InputActions")]
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private InputActionReference reloadAction;

    [Header("UI")]
    [SerializeField] private TMP_Text ammoCount;

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
    }

    private void OnDisable()
    {
        fireAction.action.started -= ShootInput;
        reloadAction.action.started -= ReloadInput;
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

    private void ShootInput(InputAction.CallbackContext context)
    {    
        if (readyToShoot && !reloading && bulletsLeft > 0 && !allowButtonHold)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void ReloadInput(InputAction.CallbackContext context)
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
        
        ammoCount.text = bulletsLeft.ToString();

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
