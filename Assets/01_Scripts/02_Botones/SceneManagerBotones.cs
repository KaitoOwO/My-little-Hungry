using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerBotones : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void SalirAlInicio()
    {
        SceneManager.LoadScene(0);
    }
    public void SalirAlMenu()
    {
        PlayerPrefs.SetFloat("diversion", 1f);
        SceneManager.LoadScene(1);
    }

    public void Reiniciar()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // Cargar la escena actual
    }
    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void Muerte()
    {
        PlayerPrefs.SetFloat("hambre", 1f);
        PlayerPrefs.SetFloat("carino", 1f);
        PlayerPrefs.SetFloat("diversion", 1f);
        SceneManager.LoadScene(2);
    }
    public void MiniScrap()
    {
        SceneManager.LoadScene(3);
    }
    public void MiniRun()
    {
        SceneManager.LoadScene(4);
    }

}
