using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public float Speed = 5.0f;

    public float RotationSpeed = 1.0f;

    //public GameObject DefaultLocation;
    public Vector3 DefaultLocation;

    public GameObject TargetObject;

    public GameObject GalaxyCenter;

    public float Fraction = 0.0f;

    public bool Zoom = false;

    public float CamHeightOffset = 10.0f;
    public float CamZOffset = -10.0f;

    private bool fade = false;

    private int LevelToLoad = -1;


    private Ray ray;
    private RaycastHit hit;

    private bool MouseDownOldFrame = false;
    // Use this for initialization
    void Start () {
        DefaultLocation = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (Zoom == true)
        {
            if(fade == false)
            {
                transform.GetComponent<ScreenFade>().BeginFade(1, 0.3f);
            }

            Fraction += Time.deltaTime * Speed;

            Vector3 NewTargetPos = TargetObject.transform.position;
            NewTargetPos.y += CamHeightOffset;
            NewTargetPos.z += CamZOffset;

            transform.position = Vector3.Lerp(DefaultLocation, NewTargetPos, Fraction);
            //transform.LookAt(TargetLocation.transform.position);

            Vector3 direction = TargetObject.transform.position - transform.position;
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, NewTargetPos) <= 2.0f)
            {
                string temp = "Level" + LevelToLoad.ToString();
                SceneManager.LoadScene(temp);
            }
        }
        else
        {
            if (fade == true)
            {
                transform.GetComponent<ScreenFade>().BeginFade(-1, 0.3f);
            }

            Vector3 direction = GalaxyCenter.transform.position - transform.position;
            direction.y -= 260.0f;
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            //Do Shit in Here
            if (Input.GetMouseButtonDown(0) == true && MouseDownOldFrame != true && Zoom != true)
            {
                if(hit.transform.gameObject.GetComponent<LevelInformation>().Level > 0)
                {
                    TargetObject = hit.transform.gameObject;
                    LevelToLoad = TargetObject.GetComponent<LevelInformation>().Level;
                    Zoom = true;
                }
            }
            MouseDownOldFrame = Input.GetMouseButtonDown(0);

            
            print(hit.collider.name);
        }
    }
}
