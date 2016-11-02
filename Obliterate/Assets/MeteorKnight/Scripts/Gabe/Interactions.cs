using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour {

    public List<GameObject> particles;
    public bool onfire = false;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		
	}


    //things that will hit
    void OnCollisionEnter(Collision collision)
    {
        //else if other stuff
    }

    //things to pass through
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Gas")
        {
            GasGiant();
        }
    }

    void GasGiant()
    {
        onfire = true;
        /*
         *Gameobject particle; 
         * Particle = Instatiate(particles[x], transform.position, transform.rotation) as Gameobject;
         * particle.parent = transform;
         */
    }

}
