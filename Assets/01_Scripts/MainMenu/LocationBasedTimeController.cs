using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using Newtonsoft.Json;

public class LocationBasedTimeController : MonoBehaviour
{
    public GameObject lightsNight;
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

    private long? gmtOffset = null; // Inicializar con un valor por defecto

    private const string apiKey = "T9QVCE2OX91P"; // Reemplaza esto con tu clave API de TimeZoneDB
    private const string apiUrl = "https://api.timezonedb.com/v2.1/get-time-zone";

    private void Start()
    {
        if (sunLight == null)
        {
            Debug.LogError("Sun Light is not assigned in the inspector.");
            return;
        }

        StartCoroutine(GetTimeZone());
    }

    private void Update()
    {
        // Solo actualizar la luz solar si gmtOffset ha sido inicializado
        if (gmtOffset.HasValue)
        {
            UpdateSunLight();
            UpdateSkyboxExposure();
        }
    }

    private IEnumerator GetTimeZone()
    {
        string localTimeZoneId = TimeZoneInfo.Local.Id;
        string url = $"{apiUrl}?key={apiKey}&format=json&by=zone&zone={localTimeZoneId}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error getting time zone: " + request.error);
                yield break;
            }

            // Procesar la respuesta JSON
            string jsonResponse = request.downloadHandler.text;
            TimeZoneInfoResponse response = JsonConvert.DeserializeObject<TimeZoneInfoResponse>(jsonResponse);

            if (response.status == "OK")
            {
                gmtOffset = response.gmtOffset;
            }
            else
            {
                Debug.LogError("Error in response: " + response.message);
            }
        }
    }

    private void UpdateSunLight()
    {
        // Obtener la hora UTC y aplicar el desplazamiento GMT
        DateTime utcNow = DateTime.UtcNow;
        DateTime localTime = utcNow.AddSeconds(gmtOffset.Value);

        // Calcular la fracción del día (0 a 1)
        float timeOfDay = (localTime.Hour + localTime.Minute / 60f) / 24f;

        // Ajustar la rotación de la luz del sol según la fracción del día
        float sunAngle = timeOfDay * 360f;
        sunLight.transform.rotation = Quaternion.Euler(new Vector3(sunAngle - 90f, 170f, 0f));

        // Determinar el color de la luz del sol basado en la hora del día
        if (localTime.Hour >= 6 && localTime.Hour < 11) // Mañana
        {
            Debug.Log("mañana");
            sunLight.color = morningColor;
        }
        else if (localTime.Hour >= 11 && localTime.Hour < 18) // Día
        {
            Debug.Log("dia");
            sunLight.color = dayColor;
        }
        else if (localTime.Hour >= 18 && localTime.Hour < 21) // Tarde
        {
            Debug.Log("tarde");
            sunLight.color = eveningColor;
        }
        else if (localTime.Hour >= 21 && localTime.Hour < 6)// Noche
        {
            Debug.Log("noche");
            sunLight.color = nightColor;
        }
    }

    private void UpdateSkyboxExposure()
    {
        // Obtener la hora UTC y aplicar el desplazamiento GMT
        DateTime utcNow = DateTime.UtcNow;
        DateTime localTime = utcNow.AddSeconds(gmtOffset.Value);

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
}

[Serializable]
public class TimeZoneInfoResponse
{
    public string status;
    public string message;
    public string countryCode;
    public string countryName;
    public string zoneName;
    public string abbreviation;
    public long? gmtOffset;   // Cambiado de long a long?
    public int dst;
    public long? zoneStart;   // Cambiado de long a long?
    public long? zoneEnd;     // Cambiado de long a long?
    public string nextAbbreviation;
    public long? timestamp;   // Cambiado de long a long?
    public string formatted;
}
