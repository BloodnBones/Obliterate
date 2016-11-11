using UnityEngine;
using System.Collections;

public class shooter : MonoBehaviour
{

    public bool hasShot;
    public bool cursorLOCK = false;
    GameObject[] shots;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        shots = GameObject.FindGameObjectsWithTag("shot");
        if (shots.Length == 0)
        {
            hasShot = false;
        }

    }
}
