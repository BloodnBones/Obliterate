using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityMod : MonoBehaviour
{

    public float Speed = 0.0f;

    public Vector3 temp;

    public float rotSpeed = 360.0f;
    public float bulletRadius = 2.0f;
    GameObject go;
    float dist;
    public Transform target;

    Quaternion CurrentRotation;
    Vector3 LastDirection;
    public List<GameObject> Rocks;

    void Start()
    {
        CurrentRotation = transform.rotation;
        LastDirection = new Vector3(0, 0, 0);
        Rocks[0] = GameObject.Find("rock_planet");
       // Rocks[1] = GameObject.Find("rock_planet (1)");
    }

    // Update is called once per frame
    void Update()
    {
        float step = Speed * Time.deltaTime;
        if (Rocks.Count > 0)
        {
            foreach (GameObject obj in Rocks)
            {
                target = obj.transform;
                dist = Vector3.Distance(obj.transform.position, this.transform.position) / 2f;

                if (dist < bulletRadius)
                {
                    Speed = Mathf.Clamp(Aim_Fire.maxSpeed, 0, 1.1f);

                    if (target != null)
                    {
                        LastDirection = target.position;
                        transform.position = Vector3.MoveTowards(transform.position, LastDirection, step);
                    }
                }
                else
                {
                    temp = new Vector3((transform.position.x - LastDirection.x),
                         (transform.position.y - LastDirection.y),
                         (transform.position.z - LastDirection.z));

                    if (Vector3.Distance(LastDirection, this.transform.position) < 1)
                    {
                        LastDirection += LastDirection;
                    }

                    transform.position = Vector3.MoveTowards(transform.position, transform.position - temp, step);
                }
            }
        }
        else
        {
            temp = new Vector3((transform.position.x - LastDirection.x),
                   (transform.position.y - LastDirection.y),
                   (transform.position.z - LastDirection.z));

            if (Vector3.Distance(LastDirection, this.transform.position) < 1)
            {
                LastDirection += LastDirection;
            }

            transform.position = Vector3.MoveTowards(transform.position, transform.position - temp, step);
            //transform.position = Vector3.MoveTowards(transform.position, LastDirection, step);
        }
    }
}

