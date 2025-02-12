using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [Header("WeaponScripts")]
    [SerializeField] private PistolScript pistolScript;
    [SerializeField] private ShotgunScript shotgunScript;
    private int pistolSlot, shotgunSlot;

    [Header("InputReferences")]
    [SerializeField] private InputActionReference slot1;
    [SerializeField] private InputActionReference slot2;

    private int currentSlot;

    private void Awake()
    {
        pistolSlot = pistolScript.gunSlot;
        shotgunSlot = shotgunScript.gunSlot;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetWeapon(pistolSlot);
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
            currentSlot = pistolSlot;
            SetWeapon(pistolSlot);
        }
    }

    private void SwitchWeapon2(InputAction.CallbackContext context)
    {
        if (currentSlot != shotgunSlot)
        {
            currentSlot = shotgunSlot;
            SetWeapon(shotgunSlot);
        }
    }

    private void SetWeapon(int slot)
    {
        if (slot == pistolSlot)
        {
            shotgunScript.enabled = false;
            pistolScript.enabled = true;
        }
        else if (slot == shotgunSlot)
        {
            pistolScript.enabled = false;
            shotgunScript.enabled = true;
        }
    }

}