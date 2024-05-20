using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;

public class TouchManager : MonoBehaviour
{
    // The variables
    public Transform playerCam;
    public float rotateSpeed = 10f; // Aumenta la velocidad para rotación continua
    public float limitRight = 45f;
    public float limitLeft = -45f;
    public SwipeListener swipeListener;

    private bool isRotating = false;
    private float rotationDirection = 0f;

    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }

    void Update()
    {
        if (isRotating)
        {
            RotateCamera(rotationDirection * rotateSpeed * Time.deltaTime);
        }
    }

    void OnSwipe(string swipe)
    {
        Debug.Log(swipe);
        if (swipe == "Right")
        {
            isRotating = true;
            rotationDirection = -1f; // Positive value for right rotation
        }
        else if (swipe == "Left")
        {
            isRotating = true;
            rotationDirection = 1f; // Negative value for left rotation
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
}
