using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrasDeVida : MonoBehaviour
{
   

    public Image hambreBarra; // Asigna la imagen circular desde el Inspector
    public float hambreDecayRate = 0.1f; // La velocidad a la que disminuye la barra de hambre
    public float hambreIncremento = 100f;


    public Image carinoBarra; // Asigna la imagen circular desde el Inspector
    public float carinoIncremento = 0.1f; // Incremento de la barra de cari�o al tocar la mascota
    public float carinoDecayRate = 0.1f; // La velocidad a la que disminuye la barra de hambre

    public Image diversionBarra; // Asigna la imagen circular desde el Inspector
    public float diversionDecayRate = 0.1f; // La velocidad a la que disminuye la barra de hambre

    public Animator petAnimator; // Asigna el Animator de tu mascota desde el Inspector

    void Start()
    {
        // Inicializa la barra de hambre al m�ximo
        hambreBarra.fillAmount = 1f;
        carinoBarra.fillAmount = 1f;
        diversionBarra.fillAmount = 1f;
    }

    void Update()
    {
        // Reduce la barra de hambre con el tiempo
        hambreBarra.fillAmount -= hambreDecayRate * Time.deltaTime;
        // Reduce la barra de hambre con el tiempo
        carinoBarra.fillAmount -= carinoDecayRate * Time.deltaTime;
        // Reduce la barra de hambre con el tiempo
        diversionBarra.fillAmount -= diversionDecayRate * Time.deltaTime;

        // Comprueba si la barra de hambre ha llegado a cero
        if (hambreBarra.fillAmount <= 0)
        {
            hambreBarra.fillAmount = 0; // Aseg�rate de que no vaya por debajo de cero

            // Reproduce la animaci�n de hambre
            petAnimator.SetTrigger("IsHungry");
        }
    }
    public void SubirHambre()
    {
        hambreBarra.fillAmount += hambreIncremento / 100f;

        // Asegurarse de que la barra no supere 1
        hambreBarra.fillAmount = Mathf.Clamp01(hambreBarra.fillAmount);
    }

    void OnMouseOver()
    {
        // Incrementa la barra de cari�o mientras el mouse est� sobre la mascota
        carinoBarra.fillAmount += carinoIncremento * Time.deltaTime;
    }
}
