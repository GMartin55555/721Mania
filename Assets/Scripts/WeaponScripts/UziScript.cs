using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UziScript : ProjWeaponClass
{
    [SerializeField] private float altFireCooldown;
    [SerializeField] private float altFireDelay;
    private bool altFireReady = true;

    [Header("AltProjectile")]
    [SerializeField] private GameObject altProjectile;
    public AudioClip altFireSound;

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

        Vector3 directionWithoutSpread = targetPoint - transform.position;

        gunAudio.clip = altFireSound;
        gunAudio.pitch = 1 + (Random.Range(-shootPitchVariation, (shootPitchVariation / 2)) * (1 + (mania.maniaScore / 50f)));
        gunAudio.Play(0);


        GameObject currentBullet = Instantiate(altProjectile, transform.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithoutSpread.normalized;
        Debug.Log("Alt Fire");
    }
}
