using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using Cinemachine;

public class TouchManager : MonoBehaviour
{
    public Transform playerCam;
    public float rotateSpeed = 10f;
    public float limitRight = 45f;
    public float limitLeft = -45f;
    public SwipeListener swipeListener;
    public CinemachineVirtualCamera virtualCamera;

    private bool isRotating = false;
    private float rotationDirection = 0f;
    private CinemachineTrackedDolly trackedDolly;

    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }

    private void Start()
    {
        trackedDolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    void Update()
    {
        if (isRotating && CanRotateCamera())
        {
            RotateCamera(rotationDirection * rotateSpeed * Time.deltaTime);
        }
    }

    void OnSwipe(string swipe)
    {
        Debug.Log(swipe);
        if (swipe == "Right" || swipe == "Left")
        {
            if (CanRotateCamera())
            {
                isRotating = true;
                rotationDirection = swipe == "Right" ? -1f : 1f;
            }
            else
            {
                isRotating = false;
            }
        }
        else
        {
            isRotating = false;
        }
    }

    void RotateCamera(float rotationAmount)
    {
        playerCam.Rotate(0, rotationAmount, 0);
        ClampCameraRotation();

        float yRotation = playerCam.eulerAngles.y;
        if (yRotation > 180)
        {
            yRotation -= 360;
        }

        if (yRotation >= -90.5f && yRotation <= -89.5f)
        {
            isRotating = false;
        }
    }

    void ClampCameraRotation()
    {
        // Limitar la rotación en el eje X a 0
        playerCam.eulerAngles = new Vector3(0, playerCam.eulerAngles.y, playerCam.eulerAngles.z);
        
        float yRotation = playerCam.eulerAngles.y;
        if (yRotation > 180)
        {
            yRotation -= 360;
        }

        yRotation = Mathf.Clamp(yRotation, limitLeft, limitRight);
        playerCam.eulerAngles = new Vector3(playerCam.eulerAngles.x, yRotation, playerCam.eulerAngles.z);
    }

    bool CanRotateCamera()
    {
        return trackedDolly != null && trackedDolly.m_PathPosition <= 0.15f;
    }
}