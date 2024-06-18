using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator petAnimator; // Asigna el Animator de tu mascota desde el Inspector, si es necesario

    public enum PetState { Awake, Asleep }
    public PetState currentPetState;

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
        // Establecer siempre el estado de la mascota a Asleep al iniciar el juego
        currentPetState = PetState.Asleep;
        PlayerPrefs.SetInt("petState", (int)currentPetState);
        PlayerPrefs.Save(); // Asegurar que el estado se guarde

        // Verificar que petAnimator esté asignado antes de usarlo
        if (petAnimator != null)
        {
            // Establecer el parámetro del animator
            petAnimator.SetBool("dormido", true);
        }
    }

    public void SetPetStateToAwake()
    {
        currentPetState = PetState.Awake;
        PlayerPrefs.SetInt("petState", (int)currentPetState);
        PlayerPrefs.Save();

        // Verificar que petAnimator esté asignado antes de usarlo
        if (petAnimator != null)
        {
            petAnimator.SetBool("dormido", false);
        }
    }

    public void SetPetStateToAsleep()
    {
        currentPetState = PetState.Asleep;
        PlayerPrefs.SetInt("petState", (int)currentPetState);
        PlayerPrefs.Save();

        // Verificar que petAnimator esté asignado antes de usarlo
        if (petAnimator != null)
        {
            petAnimator.SetBool("dormido", true);
        }
    }

    public bool IsPetAwake()
    {
        return currentPetState == PetState.Awake;
    }
}