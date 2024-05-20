using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{
  

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entr� en el trigger tiene la etiqueta "MuerteComida"
        if (other.gameObject.CompareTag("MuerteComida"))
        {
         

            // Destruye el objeto al que est� adjunto este script
            Destroy(gameObject);
            
        }

    }
}