using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public GameObject front;
    public GameObject back;
    public GameObject center;

    public float environmentVelocity = 15.0f;

    public float centerFactor;
    public float backFactor;

    public Transform start;
    public Transform end;

    public List<GameObject> props;

    public List<GameObject> obstaculos;

    private void FixedUpdate()
    {
        ManageMovement();
        ManageParallax();
        ManageSpawnCicle();
    }
    void ManageMovement()
    {
        front.transform.position = new Vector3(front.transform.position.x - environmentVelocity * Time.deltaTime, front.transform.position.y, front.transform.position.z);
    }
    void ManageParallax()
    {
        center.transform.position = new Vector3(center.transform.position.x - (environmentVelocity * centerFactor) * Time.deltaTime, center.transform.position.y, center.transform.position.z);

        back.transform.position = new Vector3(back.transform.position.x - (environmentVelocity * backFactor) * Time.deltaTime, back.transform.position.y, back.transform.position.z);
    }
    void ManageSpawnCicle()
    {
        foreach (GameObject prop in props)
        {
            if(prop.transform.position.x <= end.position.x)
            {
                prop.transform.position = new Vector3(start.position.x, prop.transform.position.y, prop.transform.position.z);
            }
        }
    }
}

