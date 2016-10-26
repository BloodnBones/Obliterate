using UnityEngine;
using System.Collections;

public class SeekBullet : MonoBehaviour
{
    public float Speed = 0.0f;

    public Vector3 temp;

    public float rotSpeed = 360.0f;

    float dist;

    public Transform target;

    Quaternion CurrentRotation;

    Vector3 LastDirection;

    void Start()
    {
        CurrentRotation = transform.rotation;
        LastDirection = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float step = Speed * Time.deltaTime;   
        if (GameObject.Find("Bullet(Clone)"))
        {               
            target = GameObject.Find("Bullet(Clone)").transform;
            dist = Vector3.Distance(GameObject.Find("Bullet(Clone)").transform.position, this.transform.position);
            
            if (dist < Aim_Fire.BulletRadius)
            {
                Speed = Mathf.Clamp(Aim_Fire.maxSpeed, 0, 2);

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
