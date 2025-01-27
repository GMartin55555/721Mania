using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivityX = 100f;
    [SerializeField] private float mouseSensitivityY = 100f;

    public Transform orientation;

    private float xRoation;
    private float yRoation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;

        yRoation += mouseX;
        xRoation -= mouseY;

        xRoation = Mathf.Clamp(xRoation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRoation, yRoation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRoation, 0f);
    }
}
