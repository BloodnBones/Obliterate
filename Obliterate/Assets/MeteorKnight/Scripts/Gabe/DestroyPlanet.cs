using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlanet : MonoBehaviour
{

    GameObject[] Planets;
    public GameObject Explosion;
    public List<GameObject> HealthGlows;
    public float HP = 1000;
    float StartHP;
    public float Onfire = 1;
    public float damageConstant;
    public bool burning = false;
    public float ExplosionRadius;
    float flameDuration;
    // Use this for initialization
    void Start()
    {
        flameDuration = 5;
        StartHP = HP;
       foreach(Transform obj in transform)
        {
            HealthGlows.Add(obj.gameObject);
            obj.gameObject.SetActive(false);
        }
       HealthGlows[0].SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (burning)
        {
            BurnDmg();
        }
        if((HP/StartHP) > 0.8f)
        {
            HealthGlows[0].SetActive(true);
        }
        if ((HP/StartHP) > 0.4f &&( HP / StartHP) < 0.8f)
        {
            HealthGlows[0].SetActive(false);
            HealthGlows[1].SetActive(true);
        }
        if((HP / StartHP) < 0.4f)
        {
            HealthGlows[1].SetActive(false);
            HealthGlows[2].SetActive(true);
        }
        if (HP < 0)
        {
            destroythis();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "shot")
        {
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
        GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;        
        Destroy(this.gameObject);  //destroy planet
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