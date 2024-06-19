using UnityEngine;

public class TV : MonoBehaviour
{
    public CambioDollyTrackObjetos dollyTrackManager; // Referencia al script CambioDollyTrackObjetos
    public GameObject botonApagar;
    public GameObject botonPrender;
    public Animator tvAnimation;

    private bool isOn = false;

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
                // Verificar si el objeto clicado es el botón de encendido o apagado
                if (hit.collider.gameObject == botonPrender)
                {
                    if (!isOn)
                    {
                        // Si la TV está apagada, encenderla
                        TurnOnTV();
                    }
                }
                else if (hit.collider.gameObject == botonApagar)
                {
                    if (isOn)
                    {
                        // Si la TV está encendida, apagarla
                        TurnOffTV();
                    }
                }
            }
        }
    }

    void TurnOnTV()
    {
        if (tvAnimation != null)
        {
            tvAnimation.SetBool("prendido", true);
            Debug.Log("TV encendida");
        }

        isOn = true;
    }

    public void TurnOffTV()
    {
        if (tvAnimation != null)
        {
            tvAnimation.SetBool("prendido", false);
            Debug.Log("TV apagada");
        }

        // Llamar al método que resetea el dolly track a 0
        if (dollyTrackManager != null)
        {
            dollyTrackManager.ResetDollyTrack();
        }

        isOn = false;
    }
}
