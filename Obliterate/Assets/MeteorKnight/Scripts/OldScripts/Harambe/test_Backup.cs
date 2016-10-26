using UnityEngine;
using System.Collections;

public class test_Backup : MonoBehaviour {

    float RotSpeed = 100.0f;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown("up"))
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f) * RotSpeed);
            Debug.Log("Up");
        }
        if (Input.GetKeyDown("down"))
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, -1.0f) * RotSpeed);
            Debug.Log("Down");
        }

    }
}
