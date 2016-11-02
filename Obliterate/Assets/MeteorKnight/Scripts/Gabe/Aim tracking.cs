using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTracking : MonoBehaviour {

    Transform parent;
   
	// Use this for initialization
	void Start () {
        parent = transform.parent;
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up, Time.deltaTime);


	}
}
