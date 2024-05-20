using UnityEngine;

public class TV : MonoBehaviour
{
    public CambioDollyTrackObjetos dollyTrackManager; // Referencia al script CambioDollyTrackObjetos
    public GameObject botonApagar;
    public GameObject botonPrender;
    public Animator tvAnimation;

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
                // Verificar si el objeto clicado es el botónPrender
                if (hit.collider.gameObject == botonApagar)
                {
                    // Llamar al método que resetea el dolly track a 0
                    if (dollyTrackManager != null)
                    {
                        tvAnimation.SetBool("prendido", false);
                        dollyTrackManager.ResetDollyTrack();
                    }
                }
                if (hit.collider.gameObject == botonPrender)
                {
                    tvAnimation.SetBool("prendido", true);
                }
            }
        }
    }
}