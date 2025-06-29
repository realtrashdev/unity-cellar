using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region References
    [SerializeField] private Transform orientation;
    CinemachineCamera cam;
    CinemachineBasicMultiChannelPerlin noise;
    #endregion
    #region Settings
    [Header("Settings")]
    [SerializeField] private bool canMove;
    [SerializeField] private bool smoothRotation;

    private float xRotation;
    private float yRotation;
    #endregion
    #region Movement
    [Header("Movement")]
    [SerializeField] private float xSens;
    [SerializeField] private float ySens;
    [SerializeField, Tooltip("Higher = More Responsive")] private float lookSmoothing;
    #endregion
    #region EventChannels

    #endregion

    void Start()
    {
        cam = GetComponent<CinemachineCamera>();
        noise = cam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) return;
        GetInput();
        RotateCamera();
    }

    public void UpdateState(BoolEvent state)
    {
        canMove = state.Value;
    }

    #region Camera Movement
    void GetInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySens;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
    }

    void RotateCamera()
    {
        if (orientation != null) RotateExtra();

        Quaternion camRotation = Quaternion.Euler(xRotation, yRotation, 0);

        switch (smoothRotation)
        {
            case true:
                //smooth camera movement
                transform.rotation = Quaternion.Lerp(transform.rotation, camRotation, lookSmoothing * Time.deltaTime);
                break;
            case false:
                //precise camera movement
                transform.rotation = camRotation;
                break;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    void RotateExtra()
    {
        Quaternion holderRotation = Quaternion.Euler(0, yRotation, 0);

        orientation.rotation = holderRotation;
    }

    public void SetRotation(Vector3 eulerRotation)
    {
        transform.eulerAngles = eulerRotation;
    }
    #endregion
    #region Juice
    
    #endregion
}
