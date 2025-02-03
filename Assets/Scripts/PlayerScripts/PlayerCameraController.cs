using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivityX = 100f;
    [SerializeField] private float mouseSensitivityY = 100f;

    public Transform orientation;

    private float xRoation;
    private float yRoation;

    [Header ("InputActions")]
    [SerializeField] private InputActionReference lookAction;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = lookAction.action.ReadValue<Vector2>().x * Time.deltaTime * mouseSensitivityX;
        float mouseY = lookAction.action.ReadValue<Vector2>().y * Time.deltaTime * mouseSensitivityY;

        yRoation += mouseX;
        xRoation -= mouseY;

        xRoation = Mathf.Clamp(xRoation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRoation, yRoation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRoation, 0f);
    }
}
