using System.Collections;
using System.Collections.Generic;
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
        SceneManager.LoadScene(2);
        
    }

}
