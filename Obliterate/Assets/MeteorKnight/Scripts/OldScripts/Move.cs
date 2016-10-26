using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
    
    
    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(Aim_Fire.maxSpeed * Time.deltaTime, 0, 0);
        pos += transform.rotation * velocity;

        transform.position = pos;
    }
}
