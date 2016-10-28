using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public GameObject Sun;
    public float orbitSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.RotateAround(Sun.transform.position, new Vector3(0, 0, -1), orbitSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("")
            //transform.RotateAround(new Vector3(0, 0, -1) * rotateSpeed);
            this.transform.RotateAround(Sun.transform.position, new Vector3(0, 0, 1), orbitSpeed * Time.deltaTime);
        }
    }
}
