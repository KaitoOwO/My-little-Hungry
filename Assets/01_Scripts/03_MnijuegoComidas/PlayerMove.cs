using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float keyboardSpeed = 5f; // Velocidad de movimiento desde teclado
    public float mouseSpeed = 10f; // Velocidad de movimiento desde el mouse
    public float touchSpeed = 10f; // Velocidad de movimiento desde touch
    public float minX = -11f; // Límite mínimo en el eje X
    public float maxX = 11f; // Límite máximo en el eje X

    void Update()
    {
        // Obtener el movimiento desde teclado
        float moveXKeyboard = Input.GetAxis("Horizontal") * keyboardSpeed;

        // Obtener el movimiento desde el mouse
        float moveXMouse = Input.GetAxis("Mouse X") * mouseSpeed;

        // Obtener el movimiento desde touch
        float moveXTouch = GetTouchMovement() * touchSpeed;

        // Sumar los movimientos
        float moveX = moveXKeyboard + moveXMouse + moveXTouch;

        // Calcular el movimiento total
        float movement = moveX * Time.deltaTime;

        // Calcular la nueva posición del jugador
        Vector3 newPosition = transform.position + new Vector3(movement, 0f, 0f);

        // Aplicar los límites de movimiento en el eje X
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Asignar la nueva posición al jugador
        transform.position = newPosition;
    }

    float GetTouchMovement()
    {
        // Manejar el movimiento desde touch (si hay un solo touch)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return touch.deltaPosition.x;
        }
        return 0f;
    }
}
