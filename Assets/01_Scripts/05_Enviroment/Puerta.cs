using Cinemachine;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public CambioDollyTrackObjetos dollyTrackManager; // Referencia al script CambioDollyTrackObjetos
    public GameObject activador;
    public Animator puertaAnim;
    public Animator canvasPuerta;

    private bool isOpen = false;

    void Update()
    {
        // Verificar si se hace clic con el mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Convertir la posición del clic a un rayo en el mundo
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verificar si el rayo intersecta con el collider de algún objeto
            if (Physics.Raycast(ray, out hit))
            {
                // Verificar si el objeto clicado es el activador
                if (hit.collider.gameObject == activador)
                {
                    if (isOpen)
                    {
                        // Si está abierto, cerrarlo
                        CloseDoor();
                    }
                    else if (!isOpen)
                    {
                        // Si está cerrado, abrirlo
                        OpenDoor();
                    }
                }
            }
        }
    }

    void OpenDoor()
    {
        if (puertaAnim != null)
        {
            puertaAnim.SetBool("abierto", true);
            canvasPuerta.SetBool("canvasActivado", true);
            Debug.Log("Puerta abierta");
        }
        isOpen = true;
    }

    public void CloseDoor()
    {
        if (puertaAnim != null)
        {
            puertaAnim.SetBool("abierto", false);
            canvasPuerta.SetBool("canvasActivado", false);
            Debug.Log("Puerta cerrada");
        }

        // Llamar al método que resetea el dolly track a 0
        if (dollyTrackManager != null)
        {
            dollyTrackManager.ResetDollyTrack();
        }
        isOpen = false;
    }
}
