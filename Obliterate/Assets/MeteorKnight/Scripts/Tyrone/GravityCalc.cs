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
    public float DecelSpeed;
    public float Mag;

	// Use this for initialization
	void Start () {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
	}
	
	// Update is called once per frame
	void Update () {
        if (TotalAcc.x != 0.0f)
        {
            TotalAcc.x -= Mathf.Sign(TotalAcc.x) * DecelSpeed;
        }
        if (TotalAcc.y != 0.0f)
        {
            TotalAcc.y -= Mathf.Sign(TotalAcc.y) * DecelSpeed;
        }
        if (TotalAcc.z != 0.0f)
        {
            TotalAcc.z -= Mathf.Sign(TotalAcc.z) * DecelSpeed;
        }
    }

    void FixedUpdate()
    {
       // Planets = GameObject.FindGameObjectsWithTag("Planet");
        if (GetComponent<SlingShot>().GetFired())
        {
            Vector3 Pos = this.transform.position;
            float Acc = 0.0f;

            for (int i = 0; i < Planets.Length; i++)
            {
                if (Planets[i] != null)
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
            }
            GetComponent<Rigidbody>().AddForce(TotalAcc);
            Mag = GetComponent<Rigidbody>().velocity.magnitude;
            if (GetComponent<Rigidbody>().velocity.magnitude > maxVelocity)
            {
                Debug.Log("What");
                Vector3 temp = GetComponent<Rigidbody>().velocity.normalized;
                GetComponent<Rigidbody>().velocity = temp * maxVelocity;
            }
            //BULLET ROTATE - buster to adjust flight path
            if (Input.GetKeyDown(KeyCode.Space) && thrustCount > 0)
            {
                Debug.Log("Thrust");
                Vector3 thrustDir = new Vector3(Mathf.Cos(transform.localRotation.eulerAngles.y), Mathf.Sin(transform.localRotation.eulerAngles.y), 0.0f);
                this.GetComponent<Rigidbody>().AddForce(thrustDir * 50000.0f);
                thrustCount--;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Right");
                transform.Rotate(Vector3.up * rotateSpeed);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Left");
                transform.Rotate(Vector3.down * rotateSpeed);
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
