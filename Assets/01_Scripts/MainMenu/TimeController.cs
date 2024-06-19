using UnityEngine;
using System;
using UnityEngine.Rendering.PostProcessing;

public class TimeController : MonoBehaviour
{
    public GameObject lightsNight;
    public GameObject lightsDoor;
    public PostProcessVolume postProcessVolume; // El volumen de postprocesamiento
    public Light sunLight; // La luz direccional que actúa como el sol
    public float dayDuration = 24f; // Duración del ciclo completo en horas de juego
    public float morningExposure = 1.0f;
    public float dayExposure = 1.3f;
    public float eveningExposure = 0.8f;
    public float nightExposure = 0.5f;

    public Color morningColor = new Color(1.0f, 0.7f, 0.4f); // Color del sol por la mañana
    public Color dayColor = new Color(1.0f, 1.0f, 1.0f); // Color del sol durante el día
    public Color eveningColor = new Color(1.0f, 0.5f, 0.3f); // Color del sol por la tarde
    public Color nightColor = new Color(0.1f, 0.1f, 0.3f); // Color del sol por la noche

    private Bloom bloomLayer;

    private void Start()
    {
        if (sunLight == null)
        {
            Debug.LogError("Sun Light is not assigned in the inspector.");
            return;
        }

        if (postProcessVolume == null)
        {
            Debug.LogError("Post Process Volume is not assigned in the inspector.");
            return;
        }

        // Obtener el componente de Bloom del volumen de postprocesamiento
        if (!postProcessVolume.profile.TryGetSettings(out bloomLayer))
        {
            Debug.LogError("Bloom not found in Post Process Volume.");
        }
    }

    private void Update()
    {
        UpdateSunLight();
        UpdateSkyboxExposure();
        UpdatePostProcessing();
    }

    private void UpdateSunLight()
    {
        // Obtener la hora local del dispositivo
        DateTime localTime = DateTime.Now;

        // Calcular la fracción del día (0 a 1)
        float timeOfDay = (localTime.Hour + localTime.Minute / 60f) / 24f;

        // Ajustar la rotación de la luz del sol según la fracción del día
        float sunAngle = timeOfDay * 360f;
        sunLight.transform.rotation = Quaternion.Euler(new Vector3(sunAngle - 90f, 170f, 0f));

        // Determinar el color de la luz del sol basado en la hora del día
        if (localTime.Hour >= 6 && localTime.Hour < 12) // Mañana
        {
            Debug.Log("Mañana");
            lightsNight.SetActive(false);
            lightsDoor.SetActive(false);
            sunLight.color = morningColor;
        }
        else if (localTime.Hour >= 12 && localTime.Hour < 18) // Día
        {
            Debug.Log("Día");
            lightsNight.SetActive(false);
            lightsDoor.SetActive(false);
            sunLight.color = dayColor;
        }
        else if (localTime.Hour >= 18 && localTime.Hour < 21) // Tarde
        {
            Debug.Log("Tarde");
            lightsNight.SetActive(true);
            lightsDoor.SetActive(true);
            sunLight.color = eveningColor;
        }
        else // Noche
        {
            Debug.Log("Noche");
            lightsNight.SetActive(true);
            lightsDoor.SetActive(true);
            sunLight.color = nightColor;
        }
    }

    private void UpdateSkyboxExposure()
    {
        // Obtener la hora local del dispositivo
        DateTime localTime = DateTime.Now;

        // Determinar la exposición basada en la hora del día
        float exposure;
        if (localTime.Hour >= 6 && localTime.Hour < 11) // Mañana
        {
            lightsNight.SetActive(false);
            exposure = morningExposure;
        }
        else if (localTime.Hour >= 11 && localTime.Hour < 17) // Día
        {
            lightsNight.SetActive(false);
            exposure = dayExposure;
        }
        else if (localTime.Hour >= 17 && localTime.Hour < 19) // Tarde
        {
            lightsNight.SetActive(true);
            exposure = eveningExposure;
        }
        else // Noche
        {
            lightsNight.SetActive(true);
            exposure = nightExposure;
        }

        // Ajustar la exposición del skybox
        RenderSettings.skybox.SetFloat("_Exposure", exposure);
    }

    private void UpdatePostProcessing()
    {
        // Obtener la hora local del dispositivo
        DateTime localTime = DateTime.Now;

        // Desactivar el bloom durante la mañana y el día, activarlo durante la tarde y la noche
        if (localTime.Hour >= 6 && localTime.Hour < 18)
        {
            bloomLayer.active = false;
        }
        else
        {
            bloomLayer.active = true;
        }
    }
}
