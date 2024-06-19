using UnityEngine;
using Cinemachine;

public class CambioDollyTrackObjetos : MonoBehaviour
{
    public PlayerController playerController;
    public Transform cameraTransform; // Referencia al transform de la cámara
    public CinemachineSmoothPath dollyTrackCenter; // Dolly track para el centro
    public CinemachineSmoothPath dollyTrackRight; // Dolly track para la derecha
    public CinemachineSmoothPath dollyTrackLeft; // Dolly track para la izquierda
    public CinemachineSmoothPath dollyTrackDormido; // Dolly track para el estado dormido
    public CinemachineVirtualCamera virtualCamera; // Referencia a la cámara virtual

    private float rotationY;
    private Quaternion previousRotation;

    void Start()
    {
        var dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_Path = dollyTrackDormido;
        dolly.m_PathPosition = 2f;
        
        previousRotation = cameraTransform.rotation;
        rotationY = cameraTransform.rotation.eulerAngles.y;
    }

    void Update()
    {
        // Verificar si la mascota está despierta
        if (!playerController.IsPetAwake())
        {
            SetDollyTrack(dollyTrackDormido);
            return; // Si la mascota no está despierta, salir del método Update
        }

        if (cameraTransform.rotation != previousRotation)
        {
            rotationY = cameraTransform.rotation.eulerAngles.y;

            if (rotationY > 265 && rotationY < 275)
            {
                SetDollyTrack(dollyTrackCenter);
            }
            else if (rotationY >= 275 && rotationY <= 360 || rotationY >= 0 && rotationY < 85)
            {
                SetDollyTrack(dollyTrackRight);
            }
            else if (rotationY > 85 && rotationY < 265)
            {
                SetDollyTrack(dollyTrackLeft);
            }
        }
        // Actualizar la rotación anterior de la cámara
        previousRotation = cameraTransform.rotation;
        
        if (rotationY > 265 && rotationY < 275)
        {
            SetObjectInteractive("Televisor");
        }
        else if (rotationY >= 275 && rotationY <= 360 || rotationY >= 0 && rotationY < 85)
        {
            SetObjectInteractive("Refrigerador");
        }
        else if (rotationY > 85 && rotationY < 265)
        {
            SetObjectInteractive("Puerta");
        }
    }

    // Método para establecer el dolly track
    void SetDollyTrack(CinemachineSmoothPath dollyTrack)
    {
        var dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        if (dolly.m_Path != dollyTrack)
        {
            dolly.m_Path = dollyTrack;
            dolly.m_PathPosition = 0f;
            Debug.Log($"Dolly track cambiado a {dollyTrack.name}");
        }
    }

    void SetObjectInteractive(string interactiveObject)
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
                    case "Televisor": // Aquí se usa el argumento interactiveObject
                        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2f;
                        Debug.Log("Televisor interactivo");
                        break;
                    case "Refrigerador": // También se usa el argumento interactiveObject
                        virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 3f;
                        Debug.Log("Refrigerador interactivo");
                        break;
                    // Puedes agregar más cases según tus necesidades
                    default:
                        break;
                }
            }
        }
    }

    // Método para restablecer el dolly track a la posición 0
    public void ResetDollyTrack()
    {
        var dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_PathPosition = 0f;
        Debug.Log("Dolly track reseteado a 0");
    }
    
    public void SetToDollyTrackCenter()
    {
        if (playerController.IsPetAwake())
        {
            var dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
            dolly.m_Path = dollyTrackCenter;
            dolly.m_PathPosition = 0f;
            cameraTransform.eulerAngles = new Vector3(0, cameraTransform.eulerAngles.y, cameraTransform.eulerAngles.z);
        }
    }

    public void SetToDollyTrackDormido()
    {
        var dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_Path = dollyTrackDormido;
        dolly.m_PathPosition = 2f;
        cameraTransform.eulerAngles = new Vector3(10, cameraTransform.eulerAngles.y, cameraTransform.eulerAngles.z);
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
