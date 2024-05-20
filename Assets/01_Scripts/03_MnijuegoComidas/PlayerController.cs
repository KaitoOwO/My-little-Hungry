using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    private float initialY;
    private float initialZ;

   

    // Límites fijos para el movimiento en el eje X
    private float minX = -4.7f;
    private float maxX = 4.6f;

    void Start()
    {
        // Obtener la cámara principal
        mainCamera = Camera.main;

        // Guardar la posición inicial en Y y Z del GameObject
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }

    void Update()
    {
        // Si hay una entrada táctil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
            {
                // Convertir la posición del toque en coordenadas de mundo
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -mainCamera.transform.position.z));
                MoveGameObject(touchPosition.x);
            }
        }
        // Si es entrada de mouse
        else if (Input.GetMouseButton(0))
        {
            // Convertir la posición del mouse en coordenadas de mundo
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
            MoveGameObject(mousePosition.x);
        }
    }

    private void MoveGameObject(float xPosition)
    {
        // Limitar la posición en X dentro de los límites fijos
        xPosition = Mathf.Clamp(xPosition, minX, maxX);

        // Mover el GameObject solo en el eje X, manteniendo Y y Z constantes
        transform.position = new Vector3(xPosition, initialY, initialZ);
    }
}
