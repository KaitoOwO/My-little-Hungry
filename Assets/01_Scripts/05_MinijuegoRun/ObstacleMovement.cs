using System;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del obstáculo
    public string endObjectName = "End"; // Nombre del gameobject "End"
    private GameObject end; // Referencia al gameobject "End"

    private void Start()
    {
        end = GameObject.Find(endObjectName);
        if (end == null)
        {
            Debug.LogError("No se pudo encontrar el gameobject con el nombre: " + endObjectName);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(gameObject.transform.position.x <= end.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}