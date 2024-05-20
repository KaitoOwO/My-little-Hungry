using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorComida : MonoBehaviour
{

    public GameObject comida; // La referencia al prefab del objeto comida que se va a generar
    public float minRatio = 0.5f; // El tiempo m�nimo entre generaciones
    public float maxRatio = 2f; // El tiempo m�ximo entre generaciones

    private float ratio; // El intervalo de tiempo actual entre la generaci�n de comidas


    public GameObject comidaMala; // La referencia al prefab del objeto comida que se va a generar
    public float minRatioComidaMala = 0.5f; // El tiempo m�nimo entre generaciones
    public float maxRatioComidaMala = 2f; // El tiempo m�ximo entre generaciones

    private float ratioComidaMala; // El intervalo de tiempo actual entre la generaci�n de comidas


    void Start()
    {
        // Inicializa el ratio con un valor aleatorio entre minRatio y maxRatio
        ratio = Random.Range(minRatio, maxRatio);

        ratioComidaMala = Random.Range(minRatioComidaMala, maxRatioComidaMala);
    }

    // Update is called once per frame
    void Update()
    {
        // Disminuye el ratio en funci�n del tiempo transcurrido desde el �ltimo frame
        ratio -= Time.deltaTime;

        // Comprueba si el ratio ha llegado a cero o es menor que cero
        if (ratio <= 0)
        {
            // Genera una nueva instancia del objeto comida en la posici�n del spawner
            // y con una rotaci�n espec�fica (90 grados en el eje x)
            Instantiate(comida, transform.position, Quaternion.Euler(90, 0, 0));

            // Resetea el ratio con un valor aleatorio entre minRatio y maxRatio
            ratio = Random.Range(minRatio, maxRatio);
        }

        ratioComidaMala -= Time.deltaTime;

        // Comprueba si el ratio ha llegado a cero o es menor que cero
        if (ratioComidaMala <= 0)
        {
            // Genera una nueva instancia del objeto comida en la posici�n del spawner
            // y con una rotaci�n espec�fica (90 grados en el eje x)
            Instantiate(comidaMala, transform.position, Quaternion.Euler(90, 0, 0));

            // Resetea el ratio con un valor aleatorio entre minRatio y maxRatio
            ratioComidaMala = Random.Range(minRatioComidaMala, maxRatioComidaMala);
        }
    }
    
}
