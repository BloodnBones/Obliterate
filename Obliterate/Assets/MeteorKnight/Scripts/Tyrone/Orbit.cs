﻿using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

    public GameObject Sun;
    public float orbitSpeed;
    public float Mass;
    public float gravCutoff;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, gravCutoff);
	}

    void FixedUpdate()
    {
        //tyrone scene
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), orbitSpeed * Time.deltaTime);

        //gabe scene
        //transform.RotateAround(transform.parent.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }

   public float GetMass()
    {
        return Mass;
    }

    public float GetCutoff()
    {
        return gravCutoff;
    }
}
