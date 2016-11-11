using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotCamera : MonoBehaviour
{

    public GameObject Sun;
    public GameObject LeftPanel;
    public GameObject RightPanel;
    public float ScrollSpeed;
    public float ScrollEdge;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.mousePosition.x >= Screen.width * (1 - ScrollEdge))
        {
            if (Input.GetMouseButton(1))
            {
                // transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.Self);
                transform.RotateAround(Sun.transform.position, Vector3.up, -ScrollSpeed * Time.deltaTime);
            }
            RightPanel.gameObject.SetActive(true);
        }
        else
        {
            RightPanel.gameObject.SetActive(false);
        }
        if (Input.mousePosition.x <= Screen.width * ScrollEdge)
        {
            if (Input.GetMouseButton(1))
            {
                // transform.Translate(Vector3.right * Time.deltaTime * -ScrollSpeed, Space.Self);
                transform.RotateAround(Sun.transform.position, Vector3.up, ScrollSpeed * Time.deltaTime);
            }
            LeftPanel.gameObject.SetActive(true);
        }
        else
        {
            LeftPanel.gameObject.SetActive(false);
        }
    }
}
