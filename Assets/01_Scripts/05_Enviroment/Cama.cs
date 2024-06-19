using UnityEngine;

public class Cama : MonoBehaviour
{
    public CambioDollyTrackObjetos dollyTrackManager; // Referencia al script DollyTrackManager

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
                // Verificar si el objeto clicado es la cama
                if (hit.collider.gameObject == gameObject)
                {
                    // Alternar entre los estados de dormido y despierto
                    dollyTrackManager.TogglePetState();
                }
            }
        }
    }
}