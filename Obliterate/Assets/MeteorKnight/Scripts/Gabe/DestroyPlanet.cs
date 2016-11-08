using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlanet : MonoBehaviour
{

    GameObject[] Planets;
    public float HP = 1000;
    public float Onfire = 1;
    public float damageConstant;
    public bool burning = false;
    public float ExplosionRadius;
    float flameDuration;

	public GameObject explosion;	//Temporary particle Trigger
    // Use this for initialization
    void Start()
    {
        flameDuration = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (burning)
        {
            BurnDmg();
        }
        if (HP < 0)
        {
            destroythis();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "shot")
        {
            Debug.Log("hit");
            if (col.gameObject.tag == "shot" && col.gameObject.GetComponent<Interactions>().onfire)
            {
                burning = true;
                flameDuration = 5;
            }

            //calculate damage based on force applied
            if (col.relativeVelocity.magnitude > 10)
            {
                Debug.Log("Holla");
                HP -= damageConstant * col.relativeVelocity.magnitude;
            }
        }


    }

    void destroythis()
    {
		explosion.gameObject.active = true;	//Temporary particle Trigger

        Planets = GameObject.FindGameObjectsWithTag("Planet");          //Find all remaining planets

        foreach(GameObject g in Planets)
        {
            if(g != this)                                              //Make sure its not self
            {
                Vector3 Distance = this.transform.position - g.transform.position;          //Check distance
                if(Distance.magnitude < this.GetComponent<Orbit>().GetCutoff())
                {
                    float temp = this.GetComponent<Orbit>().GetMass() / Distance.magnitude;
                    g.GetComponent<DestroyPlanet>().PlanetExplosion(temp);
                }
            }
        }
        //instantiate some fancy particle system for special effects
        Destroy(gameObject);  //destroy planet
    }

    void BurnDmg()
    {
        flameDuration -= Time.deltaTime;
        if (flameDuration > 0)
        {
            HP -= Onfire;
        }
        else
        {
            burning = false;
        }
    }

    void PlanetExplosion(float damage)
    {
        HP -= damage;
    }
}