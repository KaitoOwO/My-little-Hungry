using Cinemachine;
using UnityEngine;

public class DollyTrackManager : MonoBehaviour
{
    public Transform cameraTransform; // Referencia al transform de la cámara
    public CinemachineVirtualCamera virtualCamera; // Referencia a la cámara virtual
    public CinemachineSmoothPath dollyTrackDormido; // Dolly track para el estado dormido
    public CinemachineSmoothPath dollyTrackCenter; // Dolly track para el estado centro
    public PlayerController playerController; // Referencia al script PlayerController

    private float rotationY;
    private Quaternion previousRotation;

    void Start()
    {
        // Al inicio, se establece el dolly track al estado dormido
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = dollyTrackDormido;
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2f;

        // Almacenar la rotación inicial de la cámara
        previousRotation = cameraTransform.rotation;
        rotationY = cameraTransform.rotation.eulerAngles.y;
    }

    void Update()
    {
        // Verificar si la rotación de la cámara ha cambiado desde el último frame
        if (cameraTransform.rotation != previousRotation)
        {
            // Obtener la rotación en el eje Y de la cámara
            rotationY = cameraTransform.rotation.eulerAngles.y;

            // Actualizar la rotación anterior de la cámara
            previousRotation = cameraTransform.rotation;
        }
    }

    public void SetToDollyTrackCenter()
    {
        if (playerController.IsPetAwake())
        {
            virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = dollyTrackCenter;
            virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0f;
        }
    }

    public void SetToDollyTrackDormido()
    {
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = dollyTrackDormido;
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2f;
    }

    public void TogglePetState()
    {
        if (playerController.IsPetAwake())
        {
            playerController.SetPetStateToAsleep();
        }
        else
        {
            playerController.SetPetStateToAwake();
        }

        if (playerController.IsPetAwake())
        {
            SetToDollyTrackCenter();
        }
        else
        {
            SetToDollyTrackDormido();
        }
    }
}
