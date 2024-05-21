using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DineroManager : MonoBehaviour
{
    public int dineroInicial = 0; // Define el dinero inicial
    public int dineroActual; // Variable para almacenar el dinero actual

    public TextMeshProUGUI dineroContador;

    public GameObject comidaComprada;

    private void Update()
    {
        UpdateDineroUI();
    }

    void Start()
    {
        // Cargar el dinero guardado, si existe, o establecer el valor inicial
        dineroActual = PlayerPrefs.GetInt("Dinero", dineroInicial);
        comidaComprada.SetActive(false);
    }

    void GuardarDinero()
    {
        // Guardar el dinero actual en PlayerPrefs
        PlayerPrefs.SetInt("Dinero", dineroActual);
        PlayerPrefs.Save(); // Aseg�rate de guardar los cambios
    }
    private void UpdateDineroUI()
    {
        dineroContador.text = "Dinero: " + dineroActual.ToString();
    }

    // M�todo para a�adir dinero
    public void A�adirDinero(int cantidad)
    {
        dineroActual += cantidad;
        GuardarDinero(); // Guardar el dinero actualizado
    }

    // M�todo para restar dinero
    public void RestarDinero(int cantidad)
    {
        dineroActual -= cantidad;
        GuardarDinero(); // Guardar el dinero actualizado
    }

    // M�todo para obtener el dinero actual
    public int ObtenerDinero()
    {
        return dineroActual;
    }

    // M�todo para reiniciar el dinero a cero
    public void ReiniciarDinero()
    {
        dineroActual = 0;
        GuardarDinero(); // Guardar el dinero reiniciado
    }

    public void ComprarComida()
    {
        if (dineroActual >= 50)
        {
            // Restar 50 de dinero al comprar comida
            RestarDinero(50);
            Debug.Log("Comida comprada. Dinero restante: " + dineroActual);
            // Aqu� colocar�as el c�digo para activar la l�gica de comprar la comida

            comidaComprada.SetActive(true);
        }
        else
        {
            Debug.Log("No tienes suficiente dinero para comprar comida.");
            // Aqu� colocar�as el c�digo para mostrar un mensaje al jugador indicando que no tiene suficiente dinero
        }
    }
}

