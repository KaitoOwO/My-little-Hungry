using Cinemachine;
using UnityEngine;

public class DollyTrackManager : MonoBehaviour
{
    public Transform cameraTransform; // Referencia al transform de la cámara
    public CinemachineVirtualCamera virtualCamera; // Referencia a la cámara virtual
    public CinemachineSmoothPath dollyTrackLeft; // Dolly track para el lado izquierdo
    public CinemachineSmoothPath dollyTrackCenter; // Dolly track para el centro
    public CinemachineSmoothPath dollyTrackRight; // Dolly track para el lado derecho

    private Quaternion previousRotation;

    void Start()
    {
        // Almacenar la rotación inicial de la cámara
        previousRotation = cameraTransform.rotation;
    }

    void Update()
    {
        // Verificar si la rotación de la cámara ha cambiado desde el último frame
        if (cameraTransform.rotation != previousRotation)
        {
            // Obtener la rotación en el eje Y de la cámara
            float rotationY = cameraTransform.rotation.eulerAngles.y;

            // Verificar el rango de rotación y asignar el dolly track correspondiente
            if (rotationY < 240)
            {
                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = dollyTrackLeft;
                Debug.Log("Dolly track left asignado");
            }
            else if (rotationY >= 265 && rotationY <= 275)
            {
                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = dollyTrackCenter;
                Debug.Log("Dolly track Center asignado");
            }
            else if (rotationY > 300)
            {
                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = dollyTrackRight;
                Debug.Log("Dolly track right asignado");
            }

            // Actualizar la rotación anterior de la cámara
            previousRotation = cameraTransform.rotation;
        }
    }
}