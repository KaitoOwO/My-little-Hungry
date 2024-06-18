using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using Newtonsoft.Json;

public class TimeController : MonoBehaviour
{
    public Light sunLight; // La luz direccional que actúa como el sol
    public float dayDuration = 24f; // Duración del ciclo completo en horas de juego

    private int gmtOffset;

    private const string apiKey = "T9QVCE2OX91P"; // Reemplaza esto con tu clave API de TimeZoneDB
    private const string apiUrl = "http://api.timezonedb.com/v2.1/get-time-zone";

    private void Start()
    {
        if (sunLight == null)
        {
            Debug.LogError("Sun Light is not assigned in the inspector.");
            return;
        }

        StartCoroutine(GetLocation());
    }

    private void Update()
    {
        if (gmtOffset != 0)
        {
            UpdateSunLight();
        }
    }

    private IEnumerator GetLocation()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("Location services are not enabled by user.");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.LogWarning("Timed out waiting for location services.");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location.");
            yield break;
        }

        double latitude = Input.location.lastData.latitude;
        double longitude = Input.location.lastData.longitude;

        StartCoroutine(GetTimeZone(latitude, longitude));

        Input.location.Stop();
    }

    private IEnumerator GetTimeZone(double latitude, double longitude)
    {
        string url = $"{apiUrl}?key={apiKey}&format=json&by=position&lat={latitude}&lng={longitude}";

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
            TimeZoneResponse response = JsonConvert.DeserializeObject<TimeZoneResponse>(jsonResponse);

            gmtOffset = response.gmtOffset;
        }
    }

    private void UpdateSunLight()
    {
        // Obtener la hora UTC y aplicar el desplazamiento GMT
        DateTime utcNow = DateTime.UtcNow;
        DateTime localTime = utcNow.AddSeconds(gmtOffset);

        // Calcular la fracción del día (0 a 1)
        float timeOfDay = (localTime.Hour + localTime.Minute / 60f) / 24f;

        // Ajustar la rotación de la luz del sol según la fracción del día
        float sunAngle = timeOfDay * 360f;
        sunLight.transform.rotation = Quaternion.Euler(new Vector3(sunAngle - 90f, 170f, 0f));

        // Ajustar la intensidad de la luz del sol según la fracción del día
        float intensity = Mathf.Clamp01(Mathf.Cos(timeOfDay * Mathf.PI * 2) * 2 - 1);
        sunLight.intensity = intensity;
    }
}

[Serializable]
public class TimeZoneResponse
{
    public string status;
    public string message;
    public string countryCode;
    public string countryName;
    public string zoneName;
    public string abbreviation;
    public int gmtOffset;
    public int dst;
    public int zoneStart;
    public int zoneEnd;
    public string nextAbbreviation;
    public int timestamp;
    public string formatted;
}
