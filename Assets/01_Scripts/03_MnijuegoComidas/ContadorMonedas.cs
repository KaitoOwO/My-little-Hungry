using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ContadorMonedas : MonoBehaviour
{
    // Variable para almacenar el número de monedas
    private int coinCount = 0;

    public int vidas = 3;

    public GameObject gameOverPanel;

    // Referencia al componente de TextMeshProUGUI para mostrar el contador
    public TextMeshProUGUI coinCounterText;
    public TextMeshProUGUI contadorVidas;

    void Start()
    {
        // Inicializar el contador de monedas en la UI
        UpdateCoinCounterUI();
        UpdateVidasUI();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Actualizar el contador de vidas en la UI
        UpdateVidasUI();

        if (vidas == 0)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Carne"))
        {
            // Incrementar el contador de monedas
            coinCount++;

            // Actualizar la UI
            UpdateCoinCounterUI();

            // Destruir la moneda
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("CarneMala"))
        {
            // Restar una vida
            PerderVidas();

            // Destruir la comida mala
            Destroy(other.gameObject);
        }
    }

    public void PerderVidas()
    {
        // Restar una vida
        vidas--;

        // Actualizar el contador de vidas en la UI
        UpdateVidasUI();
    }

    // Función para actualizar el contador de monedas en la UI
    private void UpdateCoinCounterUI()
    {
        coinCounterText.text = "Monedas: " + coinCount.ToString();
    }

    // Función para actualizar el contador de vidas en la UI
    private void UpdateVidasUI()
    {
        contadorVidas.text = "Vidas: " + vidas.ToString();
    }

    private void GameOver()
    {
       gameOverPanel.SetActive(true);
       Time.timeScale = 0f;
    }
}
