using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityCalc : MonoBehaviour {

    GameObject[] Planets;
    GameObject sun;
    GameObject BlackWhole;
    Interactions flameOn;
    public float G;
    public float myMass;
    public float maxVelocity;
    public Vector3 TotalAcc;
    public float rotateSpeed;
    public int thrustCount;
    public float DecelSpeed;
    public float Mag;
    public bool Near = false;

	// Use this for initialization
	void Start () {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
        BlackWhole = GameObject.FindGameObjectWithTag("Blackhole");
        sun = GameObject.Find("Sun");
        flameOn = GetComponent<Interactions>();
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
                    }
                    if (Direction.magnitude < 30.0f)
                    {
                        Time.timeScale = 0.5f;
                        Time.fixedDeltaTime = 0.02f * Time.timeScale;
                        Near = true;
                    }
                    else
                    {
                        Time.timeScale = 1;
                        Time.fixedDeltaTime = 0.02f;
                        Near = false;
                    }
                }
                if(BlackWhole!= null)
                {
                    Vector3 Direction = BlackWhole.transform.position - Pos;
                    if (Direction.magnitude < BlackWhole.GetComponent<Orbit>().GetCutoff())
                    {
                        float Mass = BlackWhole.GetComponent<Orbit>().GetMass();
                        float RadSqr = Direction.sqrMagnitude;
                        Acc = G * (myMass * Mass) / (RadSqr);
                        TotalAcc += Direction.normalized * Acc;
                    }
                }
                if(sun != null)
                {
                    Vector3 Direction = sun.transform.position - Pos;
                   
                    if (Direction.magnitude < 35.0f)
                    {
                        Time.timeScale = 0.5f;
                        Time.fixedDeltaTime = 0.02f * Time.timeScale;
                        flameOn.Mixtape();
                        Near = true;
                    }
                    else
                    {
                        Time.timeScale = 1;
                        Time.fixedDeltaTime = 0.02f;
                        Near = false;
                    }
                }

                GetComponent<Rigidbody>().AddForce(TotalAcc);
                Mag = GetComponent<Rigidbody>().velocity.magnitude;
                if (GetComponent<Rigidbody>().velocity.magnitude > maxVelocity)
                {
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
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sun")
        {
              Debug.Log(collision.gameObject.tag);
              Destroy(this.gameObject);
        }
       
    }
}
