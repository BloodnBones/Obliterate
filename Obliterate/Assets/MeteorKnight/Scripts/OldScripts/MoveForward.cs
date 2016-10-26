using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour
{
    public float Speed = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, Speed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;
    }
}