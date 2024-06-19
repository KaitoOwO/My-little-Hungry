using UnityEngine;
using UnityEngine.UI;

public class BarrasDeVida : MonoBehaviour
{
    public Image hambreBarra; // Asigna la imagen circular desde el Inspector
    public float hambreDecayRate = 0.1f; // La velocidad a la que disminuye la barra de hambre
    public float hambreIncremento = 100f;

    public Image carinoBarra; // Asigna la imagen circular desde el Inspector
    public float carinoIncremento = 0.1f; // Incremento de la barra de cariño al tocar la mascota
    public float carinoDecayRate = 0.1f; // La velocidad a la que disminuye la barra de cariño

    public Image diversionBarra; // Asigna la imagen circular desde el Inspector
    public float diversionDecayRate = 0.1f; // La velocidad a la que disminuye la barra de diversión

    // Animator
    public Animator animator; // Referencia al componente Animator del objeto
    public AnimatorOverrideController overrideController; // Referencia al Animator Override Controller

    private float stateModifier = 1f;

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
            PlayerPrefs.SetInt("petState", (int)PetState.Asleep);

            // Guardar los cambios
            PlayerPrefs.Save();
        }
    }

    void Start()
    {
        // Asignar el Animator Override Controller al componente Animator
        if (animator != null && overrideController != null)
        {
            animator.runtimeAnimatorController = overrideController;
        }

        // Inicializa las barras 
        hambreBarra.fillAmount = PlayerPrefs.GetFloat("hambre");
        carinoBarra.fillAmount = PlayerPrefs.GetFloat("carino");
        diversionBarra.fillAmount = PlayerPrefs.GetFloat("diversion");

        // Recuperar el estado de la mascota
        currentPetState = (PetState)PlayerPrefs.GetInt("petState", (int)PetState.Asleep);
        stateModifier = currentPetState == PetState.Asleep ? 0.5f : 1f;
    }

    void Update()
    {
        // Ajustar la tasa de decaimiento según el estado de la mascota
        stateModifier = currentPetState == PetState.Asleep ? 0.5f : 1f;

        // Reduce las barras con el tiempo
        hambreBarra.fillAmount -= hambreDecayRate * stateModifier * Time.deltaTime;
        PlayerPrefs.SetFloat("hambre", hambreBarra.fillAmount);

        carinoBarra.fillAmount -= carinoDecayRate * stateModifier * Time.deltaTime;
        PlayerPrefs.SetFloat("carino", carinoBarra.fillAmount);

        diversionBarra.fillAmount -= diversionDecayRate * stateModifier * Time.deltaTime;
        PlayerPrefs.SetFloat("diversion", diversionBarra.fillAmount);

        // Comprueba si las barras han llegado a cero
        if (hambreBarra.fillAmount <= 0)
        {
            hambreBarra.fillAmount = 0; // Asegúrate de que no vaya por debajo de cero
            PlayerPrefs.SetFloat("hambre", hambreBarra.fillAmount);
            // Reproduce la animación de hambre
            animator.SetBool("IsHungry",true);
        }
    }

    public void SubirHambre()
    {
        hambreBarra.fillAmount += hambreIncremento / 100f;
        PlayerPrefs.SetFloat("hambre", hambreBarra.fillAmount);

        // Asegurarse de que la barra no supere 1
        hambreBarra.fillAmount = Mathf.Clamp01(hambreBarra.fillAmount);
        PlayerPrefs.SetFloat("hambre", hambreBarra.fillAmount);
    }

    void OnMouseOver()
    {
        aumentarCarino();
    }

    private void aumentarCarino()
    {
        carinoBarra.fillAmount += carinoIncremento * Time.deltaTime;
        PlayerPrefs.SetFloat("carino", carinoBarra.fillAmount);
    }

    public enum PetState
    {
        Asleep,
        Awake
    }

    public PetState currentPetState; // Estado actual de la mascota
}
