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

    public ManiaControllerScript mania;
    [Header("Shake")]
    public float shakeValue;
    private float shake = 0;
    public float shakeCount = 0;
    public GameObject reticle;

    public bool allowInvoke = true;

    public float damage;

    [Header("InputActions")]
    public InputActionReference fireAction;
    public InputActionReference reloadAction;
    public InputActionReference altFireAction;

    [Header("UI")]
    [SerializeField] public TMP_Text ammoCount;
    public Image gunSprite;
    public Animator gunAnim;
    [Header("Gun Slot")]
    public int gunSlot;

    [Header("Audio")]
    public AudioSource gunAudio;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public float shootPitchVariation = 0.2f;

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
        ammoCount.text = (bulletsLeft / bulletsPerTap).ToString();

        reticle = GameObject.FindGameObjectWithTag("reticle");
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

        ammoCount.text = (bulletsLeft / bulletsPerTap).ToString();

        if (readyToShoot && !reloading && bulletsLeft > 0 && allowButtonHold && fireAction.action.ReadValue<float>() == 1)
        {
            bulletsShot = 0;
            Shoot();
        }

        if (shake > 0)
        {
            ScreenShake();
            shake -= Time.deltaTime;
        }
        else
        {
            shake = 0;
            ScreenShakeStop();
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
        if (reloading || bulletsLeft == magazineSize) return;
        if (bulletsLeft < magazineSize)
        {
            Reload();
        }
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

        shake = shakeCount;
        gunAudio.clip = shootSound;
        gunAudio.pitch = 1 + (Random.Range(-shootPitchVariation, (shootPitchVariation/2)) * (1 + (mania.maniaScore / 50f)));
        gunAudio.Play(0);

        gunAnim.SetBool("Shot", true);
        //gunAnim.SetBool("idle", false);

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
        ScreenShakeStop();
        readyToShoot = true;
        allowInvoke = true;
        gunAnim.SetBool("Shot", false);
        //gunAnim.SetBool("idle", true);
        Debug.Log("Shot Reset");
    }

    private void Reload()
    {
        reloading = true;
        gunAnim.SetBool("Reloading", true);
        gunAudio.clip = reloadSound;
        gunAudio.pitch = 1;
        gunAudio.Play(0);
        ammoCount.text = "???";
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        ammoCount.text = bulletsLeft.ToString();
        reloading = false;
        gunAnim.SetBool("Reloading", false);
    }

    private void ScreenShake()
    {
        float shakeAmount = shakeValue * (1 + (mania.maniaScore / 75f));
        fpsCamera.transform.localPosition += new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(0, shakeAmount), 0);
        reticle.transform.localPosition += new Vector3(Random.Range(-shakeAmount * 20f, shakeAmount * 20f), Random.Range(-shakeAmount * 20f, shakeAmount * 20f), 0);
    }

    private void ScreenShakeStop()
    {
        fpsCamera.transform.localPosition = Vector3.zero;
        reticle.transform.localPosition = Vector3.zero;
    }
}
