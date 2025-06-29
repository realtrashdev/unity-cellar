using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraHolder;
    private Transform orientation;
    Rigidbody rb;

    #region Movement
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float drag;
    private bool canMove = true;
    private float moveSpeed;
    private bool sprinting;
    #endregion
    #region Ground Check
    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask ground;
    public bool grounded;
    #endregion
    #region Camera
    CinemachineCamera cam;

    [Header("Camera Effects")]
    float camFov;
    [SerializeField] float fieldOfView;
    [SerializeField, Tooltip("How much the fov is increased by when sprinting")] float sprintFovIncrease;
    [SerializeField] float fovSmoothing;

    float inputHorizontal;
    float inputVertical;
    Vector3 moveDirection;
    #endregion
    #region Event Channels
    //[Header("Event Channels")]
    #endregion

    void Start()
    {
        moveSpeed = walkSpeed;

        GetReferences();
        rb.freezeRotation = true;
    }

    void Update()
    {
        GetInput();
        CheckGrounded();
        CheckSprint();
        CameraFOV();
        SetMovementState();

        if (rb.linearVelocity != Vector3.zero)
        {
            SpeedControl();
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            MovePlayer();
        }
    }

    void GetReferences()
    {
        rb = GetComponent<Rigidbody>();
        cam = cameraHolder.GetChild(0).GetComponent<CinemachineCamera>();
        orientation = cameraHolder.GetChild(1);
    }

    #region MovementScripts
    void GetInput()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * inputVertical + orientation.right * inputHorizontal;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    void CheckGrounded()
    {
        //check
        grounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight * 0.5f) + 0.2f, ground);

        //apply movement drag
        switch (grounded)
        {
            case true:
                rb.linearDamping = drag;
                break;
            case false:
                rb.linearDamping = 0f;
                break;
        }
    }

    void CheckSprint()
    {
        if (rb.linearVelocity == Vector3.zero)
        {
            sprinting = false;
            return;
        }

        switch (Input.GetKey(KeyCode.LeftShift))
        {
            case true:
                sprinting = true;
                break;
            case false:
                sprinting = false;
                break;
        }
    }

    void SetMovementState()
    {
        if (sprinting)
        {
            moveSpeed = sprintSpeed;
            camFov = fieldOfView + sprintFovIncrease;
        }

        else
        {
            moveSpeed = walkSpeed;
            camFov = fieldOfView;
        }
    }

    void CameraFOV()
    {
        cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView, camFov, fovSmoothing * Time.deltaTime);
    }

    void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //limit speed if necessary
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }
    #endregion
}
