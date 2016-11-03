using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemOrbitGalaxy : MonoBehaviour {
    public float RotationSpeed = 10.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.parent.position, new Vector3(0.0f, 1.0f, 0.0f), RotationSpeed * Time.deltaTime);
    }
}
