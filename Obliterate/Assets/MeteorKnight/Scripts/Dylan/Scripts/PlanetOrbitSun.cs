using UnityEngine;
using System.Collections;

public class PlanetOrbitSun : MonoBehaviour {

    public float RotationSpeed = 10.0f;

   

	// Use this for initialization
	void Start () {
        float randSpeed = Random.Range(1.0f, 700.0f);
        transform.RotateAround(transform.parent.position, new Vector3(0.0f, 1.0f, 0.0f), randSpeed);
    }
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.parent.position, new Vector3(0.0f, 1.0f, 0.0f), RotationSpeed * Time.deltaTime);

       
    }
}
