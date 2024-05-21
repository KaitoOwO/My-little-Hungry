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
    void Awake()
    {
        // Verificar si es la primera vez que se ejecuta la aplicación
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            // Establecer la bandera de la primera vez
            PlayerPrefs.SetInt("FirstTime", 1);

            // Asignar los valores predeterminados
            PlayerPrefs.SetFloat("hambre", 1f);
            PlayerPrefs.SetFloat("carino", 1f);
            PlayerPrefs.SetFloat("diversion", 1f);

            // Guardar los cambios
            PlayerPrefs.Save();
        }
    }
    void Start()
    {
        // Inicializa la barra de hambre al m�ximo
        hambreBarra.fillAmount = PlayerPrefs.GetFloat("hambre");
        carinoBarra.fillAmount = PlayerPrefs.GetFloat("carino");
        diversionBarra.fillAmount = PlayerPrefs.GetFloat("diversion");
    }

    void Update()
    {
        // Reduce la barra de hambre con el tiempo
        hambreBarra.fillAmount -= hambreDecayRate * Time.deltaTime;
        PlayerPrefs.SetFloat("hambre",hambreBarra.fillAmount);
        // Reduce la barra de hambre con el tiempo
        carinoBarra.fillAmount -= carinoDecayRate * Time.deltaTime;
        PlayerPrefs.SetFloat("carino",hambreBarra.fillAmount);
        // Reduce la barra de hambre con el tiempo
        diversionBarra.fillAmount -= diversionDecayRate * Time.deltaTime;
        PlayerPrefs.SetFloat("diversion",hambreBarra.fillAmount);

        // Comprueba si la barra de hambre ha llegado a cero
        if (hambreBarra.fillAmount <= 0)
        {
            hambreBarra.fillAmount = 0; // Aseg�rate de que no vaya por debajo de cero
            PlayerPrefs.SetFloat("hambre",hambreBarra.fillAmount);
            // Reproduce la animaci�n de hambre
            petAnimator.SetTrigger("IsHungry");
        }
    }
    public void SubirHambre()
    {
        hambreBarra.fillAmount += hambreIncremento / 100f;
        PlayerPrefs.SetFloat("hambre",hambreBarra.fillAmount);

        // Asegurarse de que la barra no supere 1
        hambreBarra.fillAmount = Mathf.Clamp01(hambreBarra.fillAmount);
        PlayerPrefs.SetFloat("hambre",hambreBarra.fillAmount);
    }

    void OnMouseOver()
    {
        aumentarCariño();
    }

    private void aumentarCariño()
    {
        carinoBarra.fillAmount += carinoIncremento * Time.deltaTime;
        PlayerPrefs.SetFloat("carino",carinoBarra.fillAmount);
    }
}
