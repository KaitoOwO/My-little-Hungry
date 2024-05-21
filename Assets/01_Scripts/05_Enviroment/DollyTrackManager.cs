using Cinemachine;
using UnityEngine;

public class DollyTrackManager : MonoBehaviour
{
    public Transform cameraTransform; // Referencia al transform de la cámara
    public CinemachineVirtualCamera virtualCamera; // Referencia a la cámara virtual
    public CinemachineSmoothPath dollyTrackLeft; // Dolly track para el lado izquierdo
    public CinemachineSmoothPath dollyTrackCenter; // Dolly track para el centro
    public CinemachineSmoothPath dollyTrackRight; // Dolly track para el lado derecho
    private float rotationY;

    private Quaternion previousRotation;

    void Start()
    {
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = dollyTrackCenter;
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

            // Verificar el rango de rotación y asignar el dolly track correspondiente
            if (rotationY < 240)
            {
                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = dollyTrackLeft;
                Debug.Log("Dolly track left asignado");
                // Verificar si se hace clic con el mouse
                if (Input.GetMouseButtonDown(0))
                {
                    // Convertir la posición del clic a un rayo en el mundo
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    // Verificar si el rayo intersecta con el collider de algún objeto interactivo
                    if (Physics.Raycast(ray, out hit))
                    {
                        // Verificar el nombre del objeto clicado y modificar el dolly track correspondiente
                        switch (hit.collider.gameObject.tag)
                        {
                            case "Refrigerador":
                                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2f;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else if (rotationY > 310)
            {
                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = dollyTrackRight;
                Debug.Log("Dolly track right asignado");
                // Verificar si se hace clic con el mouse
                if (Input.GetMouseButtonDown(0))
                {
                    // Convertir la posición del clic a un rayo en el mundo
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    // Verificar si el rayo intersecta con el collider de algún objeto interactivo
                    if (Physics.Raycast(ray, out hit))
                    {
                        // Verificar el nombre del objeto clicado y modificar el dolly track correspondiente
                        switch (hit.collider.gameObject.tag)
                        {
                            case "Puerta":
                                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2f;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            // Actualizar la rotación anterior de la cámara
            previousRotation = cameraTransform.rotation;
        }
    }
}