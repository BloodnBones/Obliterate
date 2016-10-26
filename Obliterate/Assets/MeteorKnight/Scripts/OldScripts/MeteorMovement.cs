using UnityEngine;
using System.Collections;

public class MeteorMovement : MonoBehaviour
{
    float maxSpeed = Random.Range(2, 7);
    float rotationAmount = Random.Range(0f, 360f);
    
    // Update is called once per frame

    void Start()
    {
        transform.Rotate(0, 0, rotationAmount);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        //transform.position = pos;
    }
}

