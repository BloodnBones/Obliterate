using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityCalc : MonoBehaviour {

    GameObject[] Planets;
    public float G;
    public float myMass;
    public float maxVelocity;
    public Vector3 TotalAcc;

	// Use this for initialization
	void Start () {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        Vector3 Pos = this.transform.position;
        float Acc = 0.0f;

        for(int i = 0; i < Planets.Length; i++)
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
        if(GetComponent<Rigidbody>().velocity.magnitude > maxVelocity)
        {
           Vector3 temp = GetComponent<Rigidbody>().velocity.normalized;
            GetComponent<Rigidbody>().velocity = temp * maxVelocity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.tag == "Planet")
        {
          //  Debug.Log(collision.gameObject.tag);
            Destroy(this.gameObject);
        }
       
    }
}
