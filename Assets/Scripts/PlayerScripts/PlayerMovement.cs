using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool readyToJump;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundMask;
    bool isGrounded;

    [SerializeField] private Transform orientation;

    float horizontalInput;
    float verticalInput;

    private Vector3 movementDirection;

    [Header ("InputActions")]
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference jumpAction;


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void OnEnable()
    {
        jumpAction.action.started += Jump;
    }

    private void OnDisable()
    {
        jumpAction.action.started -= Jump;
    }

    private void Update()
    {
        MyInput();

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.2f, groundMask);

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = movementAction.action.ReadValue<Vector2>().x;
        verticalInput = movementAction.action.ReadValue<Vector2>().y;
    }

    private void MovePlayer()
    {
        movementDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);
        if (isGrounded)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed, ForceMode.Force);    
        }

        if (!isGrounded)
        {
            rb.AddForce(movementDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && readyToJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void JumpReset()
    {
        readyToJump = true;
    }
}
