using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour {

    public GameObject Sun;

    Ray ray;
    RaycastHit hit;
    bool gotClicked;
    bool toFire = false;
    bool Fired = false;
    public Vector3 Target;
    public Vector3 startPos;

	// Use this for initialization
	void Start () {  
    }
	
	// Update is called once per frame
	void Update () {
        if (!toFire)
        {
            transform.RotateAround(Sun.transform.position, new Vector3(0, 0, 1), 2.0f * Time.deltaTime);
        }
        if(toFire && !Fired)
        {

            Vector3 Temp = startPos - Input.mousePosition;
            Temp.Normalize();
            this.transform.position += Temp * Mathf.Sin(Time.time*10) * 0.02f;
        }
        if (Input.GetMouseButtonUp(0) && toFire)
        {
            Target = startPos - Input.mousePosition;
            Target = Target.normalized;
            this.gameObject.GetComponent<Rigidbody>().AddForce(Target * 500.0f, ForceMode.Impulse);
            Fired = true;
            Destroy(this.gameObject, 10);
          
        }
    }

    void OnMouseDown()
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

    public bool GetFired()
    {
        return Fired;
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
