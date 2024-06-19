using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opciones : MonoBehaviour
{
   public GameObject panelOpciones;

    public GameObject botonOpciones;
   public void ActivarPanel()
   {
        panelOpciones.SetActive(true);
        botonOpciones.SetActive(false);
   }

    public void DesactivarPanel()
    {
        panelOpciones.SetActive(false);
        botonOpciones.SetActive(true);
    }
}
