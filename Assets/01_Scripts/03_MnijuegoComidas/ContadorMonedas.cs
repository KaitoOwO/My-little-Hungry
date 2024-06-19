using TMPro;
using UnityEngine;


public class ContadorMonedas : MonoBehaviour
{
    public DineroManager dineroManager; // Referencia al script DineroManager
    public int vidas = 3;
    public GameObject gameOverPanel;
    public TextMeshProUGUI contadorVidas;

    public AudioSource ladra;
    public AudioSource llora;

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
            // A�adir una moneda al DineroManager
            dineroManager.AnadirDinero(1);

            // Destruir la moneda
            Destroy(other.gameObject);

            ladra.Play();
        }

        if (other.gameObject.CompareTag("CarneMala"))
        {
            PerderVidas();
            Destroy(other.gameObject);

            llora.Play();
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
