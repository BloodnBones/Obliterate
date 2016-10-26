using UnityEngine;
using System.Collections;

public class Dynamic_Follow : MonoBehaviour
{
    //shooting
    GameObject bullet;
    public GameObject parent;
    public float xoffset;
    public float yoffset;
    public float zoffset;
    public float originaly;
    public float speed;


    //idle
    private float speedH = 0.05f;
    private float speedV = 0.05f;

    public float yaw = 0.0f;
    public float pitch = 0.0f;

    public float yawmin= 0.0f;
    public float yawmax = 1.5f;
    public float Pitchmin = 0.0f;
    public float Pitchmax = 1.5f;

    float heightMax = 148f;
    float heightMin = 50f;
    float scrollSpeed = 500f;
    public float Height = 0;

    Quaternion initialRotation;
    // Use this for initialization
    void Start()
    {
        initialRotation = transform.rotation;
       // Height = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check to see if there is an asteroid belt
        if (parent.GetComponent<shooter>() != null)
        {
            shootingFollow();
            idleRotation();

        }
    }

    void shootingFollow()
    {
        if (parent.GetComponent<shooter>().hasShot)
        {
            xoffset = Input.mousePosition.x * 0.001f;

            if (zoffset > 5.0f)
            {
                zoffset -= Time.deltaTime;
            }
            bullet = GameObject.FindGameObjectWithTag("shot");
            Vector3 NewPos = bullet.transform.position;
            NewPos.z += zoffset;
            NewPos.x += xoffset;
            NewPos.y += yoffset;
            transform.position = Vector3.Slerp(transform.position, NewPos, speed);
            transform.LookAt(bullet.transform);
        }
        else
        {
           
            Vector3 NewPos = parent.transform.position;
            NewPos.z += zoffset;
            NewPos.x += xoffset;
            NewPos.y += originaly;
            transform.position = Vector3.Slerp(transform.position, NewPos, speed);
        }

    }

    void idleRotation()
    {
        if (!parent.GetComponent<shooter>().hasShot)
        {
            if (Input.GetMouseButton(1))
            {

                Quaternion curr = transform.rotation;
                yaw += speedH * Input.GetAxis("Mouse X");
                pitch -= speedV * Input.GetAxis("Mouse Y");

          
                if(pitch > Pitchmax)
                {
                    pitch = Pitchmax;
                }
                else if(pitch < Pitchmin)
                {
                    pitch = Pitchmin;
                }

                if (yaw > yawmax)
                {
                    yaw = yawmax;
                }
                else if (yaw < yawmin)
                {
                    yaw = yawmin;
                }

                curr.eulerAngles += new Vector3(pitch, yaw, 0.0f);
                transform.rotation = curr;
            }
            else
            {
                Quaternion curr = transform.rotation;
                transform.rotation = Quaternion.Slerp(curr, initialRotation, Time.deltaTime);
            }


            Height += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            Height = Mathf.Clamp(Height, heightMin, heightMax);
            Vector3 NewPos = transform.position;
            NewPos.y = Height;
            transform.position = Vector3.Slerp(transform.position, NewPos, Time.deltaTime *20.0f);
        }

    }
}
