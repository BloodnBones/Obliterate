using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotCamera : MonoBehaviour
{

    public GameObject Sun;
    public float ScrollSpeed;
    public float ScrollEdge;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.mousePosition.x >= Screen.width * (1 - ScrollEdge))
            {
                // transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.Self);
                transform.RotateAround(Sun.transform.position, Vector3.up, -ScrollSpeed * Time.deltaTime);
            }
            else if (Input.mousePosition.x <= Screen.width * ScrollEdge)
            {
                // transform.Translate(Vector3.right * Time.deltaTime * -ScrollSpeed, Space.Self);
                transform.RotateAround(Sun.transform.position, Vector3.up, ScrollSpeed * Time.deltaTime);
            }
        }
    }
}
