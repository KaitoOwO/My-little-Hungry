using UnityEngine;
using Cinemachine;
public class CambioDollyTrackObjetos : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Referencia a la cámara virtual

    void Update()
    {
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
                    case "Televisor":
                        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2f;
                        break;
                    case "Refrigerador":
                        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2f;
                        break;
                    case "Puerta":
                        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2f;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}