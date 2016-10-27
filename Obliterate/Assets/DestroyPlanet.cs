using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlanet : MonoBehaviour
{


    public float HP = 1000;
    public float Onfire = 1;
    public float damageConstant;
    public bool burning = false;
    float flameDuration;
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
            if (col.gameObject.tag == "shot" &&
                col.gameObject.GetComponent<Interactions>().onfire)
            {
                burning = true;
                flameDuration = 5;
            }

            //calculate damage based on force applied
            if (col.relativeVelocity.magnitude > 10)
            {
                HP -= damageConstant * col.relativeVelocity.magnitude;
            }
        }


    }

    void destroythis()
    {

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
}