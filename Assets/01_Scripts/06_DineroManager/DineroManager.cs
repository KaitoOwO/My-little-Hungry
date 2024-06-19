using UnityEngine;
using TMPro;

public class DineroManager : MonoBehaviour
{
    public int dineroInicial = 0; // Define el dinero inicial
    public int dineroActual; // Variable para almacenar el dinero actual

    public TextMeshProUGUI dineroContador; // Asegúrate de asignar este campo en el Inspector
    public GameObject comidaComprada; // Asegúrate de asignar este campo en el Inspector

    private void Start()
    {
        // Cargar el dinero guardado, si existe, o establecer el valor inicial
        dineroActual = PlayerPrefs.GetInt("Dinero", dineroInicial);

        if (comidaComprada != null)
        {
            comidaComprada.SetActive(false);
        }
        else
        {
            Debug.LogError("La referencia a comidaComprada no está asignada en el Inspector.");
        }
    }

    private void Update()
    {
        UpdateDineroUI();
    }

    private void UpdateDineroUI()
    {
        if (dineroContador != null)
        {
            dineroContador.text = "Scrap: " + dineroActual.ToString();
        }
        else
        {
            Debug.LogError("La referencia a dineroContador no está asignada en el Inspector.");
        }
    }

    void GuardarDinero()
    {
        // Guardar el dinero actual en PlayerPrefs
        PlayerPrefs.SetInt("Dinero", dineroActual);
        PlayerPrefs.Save(); // Asegúrate de guardar los cambios
    }

    public void AnadirDinero(int cantidad)
    {
        dineroActual += cantidad;
        GuardarDinero(); // Guardar el dinero actualizado
    }

    public void RestarDinero(int cantidad)
    {
        dineroActual -= cantidad;
        GuardarDinero(); // Guardar el dinero actualizado
    }

    public int ObtenerDinero()
    {
        return dineroActual;
    }

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

            // Aquí colocarías el código para activar la lógica de comprar la comida
            if (comidaComprada != null)
            {
                comidaComprada.SetActive(true);
            }
            else
            {
                Debug.LogError("La referencia a comidaComprada no está asignada en el Inspector.");
            }
        }
        else
        {
            Debug.Log("No tienes suficiente dinero para comprar comida.");
            // Aquí colocarías el código para mostrar un mensaje al jugador indicando que no tiene suficiente dinero
        }
    }
}


