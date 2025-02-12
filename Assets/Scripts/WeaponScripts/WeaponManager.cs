using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [Header("WeaponScripts")]
    [SerializeField] private PistolScript pistolScript;
    [SerializeField] private UziScript uziScript;
    private int pistolSlot, uziSlot;
    [Header("SwitchTime")]
    [SerializeField] private float switchTime;

    [Header("InputReferences")]
    [SerializeField] private InputActionReference slot1;
    [SerializeField] private InputActionReference slot2;

    private int currentSlot;

    private IEnumerator setWeapon;

    private void Awake()
    {
        pistolSlot = pistolScript.gunSlot;
        uziSlot = uziScript.gunSlot;
    }

    // Start is called before the first frame update
    void Start()
    {
        pistolScript.enabled = true;
    }

    private void OnEnable()
    {
        slot1.action.performed += SwitchWeapon1;
        slot2.action.performed += SwitchWeapon2;
    }

    private void OnDisable()
    {
        slot1.action.performed -= SwitchWeapon1;
        slot2.action.performed -= SwitchWeapon2;
    }

    private void SwitchWeapon1(InputAction.CallbackContext context)
    {
        if (currentSlot != pistolSlot)
        {
            setWeapon = SetWeapon(pistolSlot);
            currentSlot = pistolSlot;
            StartCoroutine(setWeapon);
        }
    }

    private void SwitchWeapon2(InputAction.CallbackContext context)
    {
        if (currentSlot != uziSlot)
        {
            setWeapon = SetWeapon(uziSlot);
            currentSlot = uziSlot;
            StartCoroutine(setWeapon);
        }
    }

    private IEnumerator SetWeapon(int slot)
    {
        if (slot == pistolSlot)
        {
            uziScript.enabled = false;
            yield return new WaitForSeconds(switchTime);
            pistolScript.enabled = true;
        }
        else if (slot == uziSlot)
        {
            pistolScript.enabled = false;
            yield return new WaitForSeconds(switchTime);
            uziScript.enabled = true;
        }
    }

}