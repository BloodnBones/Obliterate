using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Aim_Fire : MonoBehaviour { 
    float RotSpeed = 3.0f;
    public GameObject tempBullet;
    public Vector3 bulletOffset = new Vector3(0.0f, 0.0f, 0.0f);
    public float FireRate = 6.0f;
    public float LastShot = 0.0f;

    static public float BulletRadius = 2;
    static public float maxSpeed = 5.0f;
    // public List<GameObject> Bullet;

    // Use this for initialization
    void Start () {
        transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
        Score.Score_Scalar = 5;

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle > 45 || angle < -45)
        {
            angle = 0;
        }
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 offset = transform.rotation * bulletOffset + Vector3.right;

        LastShot += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && LastShot > FireRate )
        {
           GameObject clone = Instantiate(tempBullet, transform.position, transform.rotation) as GameObject;
            Vector3 velocity = tempBullet.transform.rotation * Vector3.right;
            LastShot = 0;
            Destroy(clone.gameObject, FireRate);
            if(Score.Score_Scalar != 1)
            {
                Score.Score_Scalar -= 1;
            }
            //GetComponent<AudioSource>().Play();
        }
        //rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }

};
