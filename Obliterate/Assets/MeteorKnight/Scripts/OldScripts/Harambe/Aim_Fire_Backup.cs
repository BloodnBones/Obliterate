using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Aim_Fire_Backup : MonoBehaviour
{


    float RotSpeed = 3.0f;
    public GameObject tempBullet;
    public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
    public float FireRate = 6.0f;
    float LastShot = 10.0f;
    public float Timer = 5;
    // public List<GameObject> Bullet;

    // Use this for initialization
    void Start()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
        Score.Score_Scalar = 5;
        Timer = 5;
    }

    // Update is called once per frame
    void Update()
    {

        // float moveHorizontal = Input.GetAxis("Horizontal");
        // float moveVertical = Input.GetAxis("Vertical");

        // Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //// rigidbody.velocity = movement * speed;

        if (Input.GetKey("up"))
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f) * RotSpeed);
            //Debug.Log("Up");
        }
        if (Input.GetKey("down"))
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, -1.0f) * RotSpeed);
            //Debug.Log("Down");
        }

        Vector3 offset = transform.rotation * bulletOffset;

        LastShot += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && LastShot > FireRate)
        {
            GetComponent<AudioSource>().Play();
            if (GetComponent<AudioSource>().isPlaying)
            {
                GameObject clone = Instantiate(tempBullet, transform.position + offset, transform.rotation) as GameObject;
                Vector3 velocity = tempBullet.transform.rotation * Vector3.right;
                clone.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1000, 0), ForceMode.Impulse);
                LastShot = 0;
                Destroy(clone.gameObject, FireRate);
                if (Score.Score_Scalar != 1)
                {
                    Score.Score_Scalar -= 1;
                }
            }
        }


        //rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }
};
