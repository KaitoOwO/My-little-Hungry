using Cinemachine;
using UnityEngine;

public class Freezer : MonoBehaviour
{
    public CambioDollyTrackObjetos dollyTrackManager; // Referencia al script CambioDollyTrackObjetos
    public GameObject activador;
    public Animator freezerAnim;
    public Animator canvasRefrigerador;

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
                        CloseFreezer();
                    }
                    else
                    {
                        // Si está cerrado, abrirlo
                        OpenFreezer();
                    }
                }
            }
        }
    }

    void OpenFreezer()
    {
        if (freezerAnim != null)
        {
            freezerAnim.SetBool("abierto", true);
            canvasRefrigerador.SetBool("canvasActivado", true);
            Debug.Log("Refrigerador abierto");
        }
        isOpen = true;
    }

    void CloseFreezer()
    {
        if (freezerAnim != null)
        {
            freezerAnim.SetBool("abierto", false);
            canvasRefrigerador.SetBool("canvasActivado", false);
            Debug.Log("Refrigerador cerrado");
        }

        // Llamar al método que resetea el dolly track a 0
        if (dollyTrackManager != null)
        {
            dollyTrackManager.ResetDollyTrack();
        }
        
        isOpen = false;
    }
}