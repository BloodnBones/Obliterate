using UnityEngine;
using System.Collections;

public class shooter : MonoBehaviour {

    public bool hasShot;
    Vector3 originalPos;
    Vector3 bulletpos;
    bool cursor = false;
    // Use this for initialization
    void Start()
    {
      //  Cursor.visible = cursor;
        originalPos = transform.position;
        foreach (Transform obj in transform)
        {
            bulletpos = obj.position;
        }
    }
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            cursor = !cursor;
            Cursor.visible = cursor;
        }
        //if(Input.GetKeyDown(KeyCode.Space) && !hasShot)
        //{
        //    hasShot = true;
        //    foreach(Transform obj in transform)
        //    {
        //        obj.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //        obj.gameObject.tag = "shot";
        //        obj.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1000, ForceMode.Impulse);
        //    }
        //}
        //if(Input.GetKeyDown(KeyCode.R) && hasShot)
        //{
        //    hasShot = false;
        //    foreach (Transform obj in transform)
        //    {
        //        obj.gameObject.tag = "idle";
        //        obj.position = bulletpos;
        //        obj.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //        obj.rotation = Quaternion.Euler(Vector3.zero);
        //        obj.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //    }
        //    transform.position = originalPos;
        //}
	
	}
}
