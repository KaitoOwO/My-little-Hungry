using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using Cinemachine;

public class TouchManager : MonoBehaviour
{
    // The variables
    public Transform playerCam;
    public float rotateSpeed = 10f; // Aumenta la velocidad para rotación continua
    public float limitRight = 45f;
    public float limitLeft = -45f;
    public SwipeListener swipeListener;
    public CinemachineVirtualCamera virtualCamera; // Referencia a la cámara virtual

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
        // Obtener el componente CinemachineTrackedDolly de la cámara virtual
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
                rotationDirection = swipe == "Right" ? -1f : 1f; // Positive value for right rotation, negative for left
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

        // Verificar si el ángulo Y está en el rango permitido
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
        // Verificar si la posición del dolly track es menor o igual a 0.25
        return trackedDolly != null && trackedDolly.m_PathPosition <= 0.25f;
    }
}
