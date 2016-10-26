using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {

    public float rotationSpeed = 0;
    public Vector3 rotationDirection;

	// Use this for initialization
	void Start () {
        rotationSpeed = Random.Range(5.0f, 14.0f);
        rotationDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotationDirection * Time.deltaTime * rotationSpeed);
    }
}
