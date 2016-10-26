using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{

    public Transform Bullet;
    public Transform firePosition;
    public Vector3 offset;
    public float rotateSpeed;
    public float fireForce;
    public float fireRate;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Debug.Log("")
            transform.Rotate(new Vector3(0, 0, -1) * rotateSpeed);
        }
    }

    void Shoot()
    {
        Transform clone;
        clone = Instantiate(Bullet, firePosition.transform.position, this.transform.rotation) as Transform;
        clone.GetComponent<Rigidbody>().AddForce(clone.transform.up * fireForce, ForceMode.Impulse);

        Destroy(clone.gameObject, fireRate);
    }
}
