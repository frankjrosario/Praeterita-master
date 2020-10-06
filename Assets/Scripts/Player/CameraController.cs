using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public Camera playerCamera;

    [Header("Camera Details")]
    public float yRotationOffset;
    public float mouseSensitivity;
    public float cameraSpeed;
    public bool isAiming;
    public Vector3 zoomedCameraPos;
    public Vector3 cameraOffset;
    public Vector3 aimOffset;

    /*---Camera Positioning---*/
    private Vector3 origCameraPos;
    private float cameraDistance;
    private float cameraDistanceNormal;
    private float cameraDistanceZoomed;

    [Header("Camera Clamping")]
    public float minClamp;
    public float maxClamp;

    private float xRotation = 0f;
    private float xCam, yCam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        origCameraPos = playerCamera.transform.localPosition;
        cameraDistanceNormal = Vector3.Distance(transform.position, playerCamera.transform.position);
        cameraDistanceZoomed = 1f;
    }

    void Update()
    {
        /* All player input should be received in Update */

        CheckAiming();

        xCam = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yCam = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    }

    void LateUpdate()
    {
        AdjustCamera();
        CamMove();
    }

    void CamMove()
    {
        xRotation -= yCam;
        xRotation = Mathf.Clamp(xRotation, minClamp, maxClamp);

        transform.localRotation = Quaternion.Euler(xRotation, yRotationOffset, 0f);
        player.transform.Rotate(Vector3.up * xCam);
    }

    void AdjustCamera()
    {
        /* Sends a raycast out from the player torso to the camera
         * If the raycast does not hit the camers -> camera is moved to the position of the hit point
         * Moves camera if player is aiming or not */

        RaycastHit hit;
        Ray backwardsRay = new Ray((transform.position + cameraOffset), -((transform.position + cameraOffset) - playerCamera.transform.position));

        Debug.DrawRay((transform.position + cameraOffset), -((transform.position + cameraOffset) - playerCamera.transform.position), Color.green);
        if (Physics.Raycast(backwardsRay, out hit, cameraDistance) && hit.collider.gameObject != playerCamera)
        {
            playerCamera.transform.position = hit.point;
            return;
        }
        
        if (isAiming)
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, zoomedCameraPos, cameraSpeed * Time.deltaTime);
        }
        else if (!isAiming)
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, origCameraPos, cameraSpeed * Time.deltaTime);
        }
    }

    void CheckAiming()
    {
        /* Checks aiming input */

        if (Input.GetButton("Fire2"))
        {
            isAiming = true;
            cameraDistance = cameraDistanceZoomed;
        }
        else
        {
            isAiming = false;
            cameraDistance = cameraDistanceNormal;
        }
    }
}
