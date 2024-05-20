using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerBotones : MonoBehaviour
{
    public void Salir()
    {
        SceneManager.LoadScene(0);
    }
    public void Riniciar()
    {
        SceneManager.LoadScene(1);
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
