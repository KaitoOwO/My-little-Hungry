using UnityEngine;
using Cinemachine;

public class CambioDollyTrackObjetos : MonoBehaviour
{
    public PlayerController playerController;
    public Transform cameraTransform; // Referencia al transform de la cámara
    public CinemachineSmoothPath dollyTrackCenter; // Dolly track para el centro
    public CinemachineVirtualCamera virtualCamera; // Referencia a la cámara virtual
    private float rotationY;
    private Quaternion previousRotation;

    void Update()
    {
        // Verificar si la mascota está despierta
        if (!playerController.IsPetAwake())
        {
            return; // Si la mascota no está despierta, salir del método Update
        }

        if (cameraTransform.rotation != previousRotation)
        {
            rotationY = cameraTransform.rotation.eulerAngles.y;

            if (rotationY > 265 && rotationY < 275)
            {
                virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = dollyTrackCenter;
                Debug.Log("Dolly track center asignado");

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
                        }
                    }
                }
            }
        }

        // Actualizar la rotación anterior de la cámara
        previousRotation = cameraTransform.rotation;
    }

    // Método para restablecer el dolly track a la posición 0
    public void ResetDollyTrack()
    {
        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0f;
        Debug.Log("Dolly track reseteado a 0");
    }
}
