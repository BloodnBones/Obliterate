using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityCalc : MonoBehaviour {

    GameObject[] Planets;
    public float G;
    public float myMass;
    public float maxVelocity;
    public Vector3 TotalAcc;
    public float rotateSpeed;
    public int thrustCount;

	// Use this for initialization
	void Start () {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (GetComponent<SlingShot>().GetFired())
        {
            Vector3 Pos = this.transform.position;
            float Acc = 0.0f;

            for (int i = 0; i < Planets.Length; i++)
            {
                Vector3 Direction = Planets[i].transform.position - Pos;
                if (Direction.magnitude < Planets[i].GetComponent<Orbit>().GetCutoff())
                {
                    float Mass = Planets[i].GetComponent<Orbit>().GetMass();
                    float RadSqr = Direction.sqrMagnitude;
                    Acc = G * (myMass * Mass) / (RadSqr);
                    TotalAcc += Direction.normalized * Acc;
                }//else if(Direction.magnitude < Planets[i].GetComponent<Orbit>().GetCutoff()/5)
                 //{
                 //    float Mass = Planets[i].GetComponent<Orbit>().GetMass();
                 //    float RadSqr = Direction.sqrMagnitude;
                 //    Acc = G * (myMass * Mass)/(RadSqr*0.5f);
                 //    TotalAcc += Direction.normalized * Acc;
                 //}
            }
            GetComponent<Rigidbody>().AddForce(TotalAcc);
            if (GetComponent<Rigidbody>().velocity.magnitude > maxVelocity)
            {
                Vector3 temp = GetComponent<Rigidbody>().velocity.normalized;
                GetComponent<Rigidbody>().velocity = temp * maxVelocity;
            }

            //BULLET ROTATE - buster to adjust flight path
            if (Input.GetKeyDown(KeyCode.Space) && thrustCount > 0)
            {
                Debug.Log("Thrust");
                Vector3 thrustDir = new Vector3(Mathf.Cos(transform.localRotation.eulerAngles.z), Mathf.Sin(transform.localRotation.eulerAngles.z), 0.0f);
                this.GetComponent<Rigidbody>().AddForce(thrustDir * 5000.0f);
                thrustCount--;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Right");
                transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Left");
                transform.Rotate(new Vector3(0, 0, -1) * rotateSpeed);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.tag == "Planet")
        {
          //  Debug.Log(collision.gameObject.tag);
          //  Destroy(this.gameObject);
        }
       
    }
}
