using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivityX = 100f;
    [SerializeField] private float mouseSensitivityY = 100f;
    [SerializeField] private ManiaControllerScript mania;
    [SerializeField] private float leanSpeed = 5f;

    [SerializeField] private new Camera camera;
    public Transform orientation;

    private float xRoation;
    private float yRoation;
    private float leanValue;

    [Header("InputActions")]
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private InputActionReference moveAction;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float mouseX = lookAction.action.ReadValue<Vector2>().x * Time.deltaTime * mouseSensitivityX;
        float mouseY = lookAction.action.ReadValue<Vector2>().y * Time.deltaTime * mouseSensitivityY;

        yRoation += mouseX;
        xRoation -= mouseY;

        xRoation = Mathf.Clamp(xRoation, -90f, 90f);
        ManiaLean();
        camera.transform.rotation = Quaternion.Euler(xRoation, yRoation, leanValue);
        transform.rotation = Quaternion.Euler(0f, yRoation, 0f);
    }

    private void ManiaLean()
    {
        float desiredLeanValue = 0;
        if (moveAction.action.ReadValue<Vector2>().x > 0)
        {
            desiredLeanValue = -mania.maniaScore / 5;
        }
        else if (moveAction.action.ReadValue<Vector2>().x < 0)
        {
            desiredLeanValue = mania.maniaScore / 5;
        }
        else
        {
            desiredLeanValue = 0;
        }

        leanValue = Mathf.Lerp(leanValue, desiredLeanValue, Time.deltaTime * leanSpeed);

    }
}
