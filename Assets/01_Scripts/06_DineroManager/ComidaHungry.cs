using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComidaHungry : MonoBehaviour
{
    public BarrasDeVida barrasDeVida; // Referencia al script BarrasDeVida

    private void OnMouseDown()
    {
        // Desactivar este GameObject
        gameObject.SetActive(false);

        // Subir la barra de hambre
        barrasDeVida.SubirHambre();
    }
}
