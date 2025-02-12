using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistolScript : ProjWeaponClass
{
    [SerializeField] private ManiaControllerScript mania;
    [SerializeField] private float altFireCooldown;
    [SerializeField] private float altFireDelay;
    private bool altFireReady = true;

    private void OnEnable()
    {
        fireAction.action.started += ShootInput;
        reloadAction.action.started += ReloadInput;
        altFireAction.action.started += AltShootInput;

        bulletsLeft = magazineSize;
        readyToShoot = true;
        altFireReady = true;
        gunSprite.gameObject.SetActive(true);
        ammoCount.gameObject.SetActive(true);
        ammoCount.text = bulletsLeft.ToString();
    }

    private void OnDisable()
    {
        fireAction.action.started -= ShootInput;
        reloadAction.action.started -= ReloadInput;
        altFireAction.action.started -= AltShootInput;
        gunSprite.gameObject.SetActive(false);
        ammoCount.gameObject.SetActive(false);
    }

    private void AltShootInput(InputAction.CallbackContext context)
    {
        if (altFireReady)
        {
            AltShoot();
        }
    }

    private void AltShoot()
    {
        altFireReady = false; 
        readyToShoot = false;
        reloading = true;
        Invoke("AltDecay", altFireDelay);
        Invoke("AltFireReset", altFireCooldown);
    }

    private void AltDecay()
    {
        mania.ManiaDecay(20);
        readyToShoot = true;
        reloading = false;
    }

    private void AltFireReset()
    {
        altFireReady = true;
        readyToShoot = true;
    }
}
