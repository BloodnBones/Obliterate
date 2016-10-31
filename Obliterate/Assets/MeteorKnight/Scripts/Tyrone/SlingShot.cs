using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SlingShot : MonoBehaviour {

    public GameObject Sun;
    public GameObject parent;
    Ray ray;
    RaycastHit hit;
    bool gotClicked;
    bool toFire = false;
    bool Fired = false;
    public float OrbitSpeed;
    public Vector3 Target;
    public Vector3 startPos;
    public float Timer = 10.0f;

	// Use this for initialization
	void Start () {
        OrbitSpeed = Random.Range(0.9f, 5);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(parent.GetComponent<shooter>().hasShot)
        {
            Timer -= Time.deltaTime;
            if(Timer <= 0 )
            {
                parent.GetComponent<shooter>().hasShot = false;
                Timer = 10;
            }
        }

        if (!toFire)
        {
            //gabe scene
            if (SceneManager.GetActiveScene().name == "TestScene")
            {
                transform.RotateAround(transform.parent.position, Vector3.up, OrbitSpeed * Time.deltaTime);
            }
            else
            {
                transform.RotateAround(Sun.transform.position, new Vector3(0, 0, 1), 2.0f * Time.deltaTime);
            }
        }
        if(toFire && !Fired)
        {
            if (!parent.GetComponent<shooter>().hasShot)
            {
                Vector3 Temp = startPos - Input.mousePosition;
                Temp.Normalize();
                this.transform.position += Temp * Mathf.Sin(Time.time * 10) * 0.02f;
            }
        }
        if (Input.GetMouseButtonUp(0) && toFire)
        {
            if (SceneManager.GetActiveScene().name == "TestScene")

            {
                if (!parent.GetComponent<shooter>().hasShot)
                {
                    Target = startPos - Input.mousePosition;
                    Target.z = Target.y;
                    Target.y = 0;
                    Target = Target.normalized;
                    this.gameObject.GetComponent<Rigidbody>().AddForce(Target * 500.0f, ForceMode.Impulse);
                    Fired = true;

                    //dynamic follow ai
                    gameObject.tag = "shot";
                    parent.GetComponent<shooter>().hasShot = true;

                    //dynamic destuction (aka levelusion, aka destructable objects) AI
                    Destroy(this.gameObject, 10);
                }
                else
                {
                    parent.GetComponent<shooter>().hasShot = false;
                    gameObject.tag = "Asteroid";
                }
                
            }
            else
            {
                Target = startPos - Input.mousePosition;
                Target = Target.normalized;
                this.gameObject.GetComponent<Rigidbody>().AddForce(Target * 500.0f, ForceMode.Impulse);
                Fired = true;
                Destroy(this.gameObject, 10);
            }
        }
    }

    void OnMouseDown()
    {
        if (!parent.GetComponent<shooter>().hasShot)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            this.gotClicked = Physics.Raycast(ray, out hit);

            if (Input.GetMouseButtonDown(0) && this.gotClicked)
            {
                if (hit.collider.tag == "Asteroid")
                {
                    // Debug.Log("clicked");
                    startPos = Input.mousePosition;
                    this.toFire = true;
                }
            }
        }
        else
        {
            this.toFire = false;
        }
        
    }

    public bool GetFired()
    {
        return Fired;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            parent.GetComponent<shooter>().hasShot = false;
            gameObject.tag = " ";
            //  Debug.Log(collision.gameObject.tag);
            Destroy(this.gameObject,1);
        }
    }
}
