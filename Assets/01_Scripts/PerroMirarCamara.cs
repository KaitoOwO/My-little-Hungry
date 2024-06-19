using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerroMirarCamara : MonoBehaviour
{
    void Update()
    {
        // Obtiene la cámara principal
        Camera mainCamera = Camera.main;

        // Asegura que la cámara existe
        if (mainCamera != null)
        {
            // Hace que el objeto mire hacia la cámara
            transform.LookAt(mainCamera.transform);

            // Opcional: Si quieres que el objeto solo rote en el eje Y (para no inclinarlo)
            Vector3 lookDirection = mainCamera.transform.position - transform.position;
            lookDirection.y = 0; // Mantén la rotación solo en el eje Y
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
