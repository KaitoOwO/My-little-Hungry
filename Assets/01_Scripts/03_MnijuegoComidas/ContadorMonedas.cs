using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ContadorMonedas : MonoBehaviour
{
    public DineroManager dineroManager; // Referencia al script DineroManager
    public int vidas = 3;
    public GameObject gameOverPanel;
    public TextMeshProUGUI contadorVidas;

    void Start()
    {
        UpdateVidasUI();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
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
            // Añadir una moneda al DineroManager
            dineroManager.AñadirDinero(1);

            // Destruir la moneda
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("CarneMala"))
        {
            PerderVidas();
            Destroy(other.gameObject);
        }
    }

    public void PerderVidas()
    {
        vidas--;
        UpdateVidasUI();
    }

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
