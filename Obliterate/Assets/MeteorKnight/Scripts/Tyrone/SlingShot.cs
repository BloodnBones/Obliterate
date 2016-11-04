using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SlingShot : MonoBehaviour
{

    public GameObject Sun;
    public GameObject parent;
    public GameObject child;
    public Vector3 Target;
    public Vector3 startPos;
    public float OrbitSpeed;

    private Color lerpedAim;
    private SpriteRenderer spr;

    private Ray ray;
    private RaycastHit hit;

    private bool gotClicked;
    private bool toFire = false;
    private bool Fired = false;
    private bool Childset = false;
    private bool Hover = false;

    private float Timer = 100000.0f;

    public Texture2D cursorTexture;
    public Texture2D hoverTexture;
    [SerializeField]
    Texture2D tex;
    int w = 32;
    int h = 32;
    public CursorMode cursorMode = CursorMode.Auto;
    private Vector2 mouse = Vector2.zero;
    private Vector2 MousePos = Vector2.zero;

    private float FollowTimer = 10.0f;
    // Use this for initialization
    void Start()
    {

        OrbitSpeed = Random.Range(0.9f, 1);
        spr = child.GetComponent<SpriteRenderer>();
        lerpedAim = Color.white;
        tex = cursorTexture;
    }

    void Update()
    {
        //if(FollowTimer < 0.0f)
        //{
        //    this.gameObject.tag = "Lost";
        //    FollowTimer = 10.0f;
        //}
        //FollowTimer -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        Cursor.SetCursor(tex, mouse, cursorMode);

        Debug.DrawLine(transform.position, transform.forward, Color.yellow);
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            parent.GetComponent<shooter>().hasShot = false;
            parent.GetComponent<shooter>().cursorLOCK = false;
            Timer = 10000000.0f;
        }


        Fire();
        Aimtracker();
    }

    void OnMouseDown()
    {
            if (!parent.GetComponent<shooter>().hasShot)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                this.gotClicked = Physics.Raycast(ray, out hit);

                if (Input.GetMouseButtonDown(0) && this.gotClicked)
                {
                    if (hit.collider.tag == "Asteroid")
                    {
                        startPos = Input.mousePosition;
                        this.toFire = true;
                        parent.GetComponent<shooter>().cursorLOCK = true;
                        Cursor.visible = false;
                    }
                }
            }
            else
            {
                this.toFire = false;
                parent.GetComponent<shooter>().cursorLOCK = false;
            }
    }

    public bool GetFired()
    {
        return Fired;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            parent.GetComponent<shooter>().hasShot = false;
            Timer = 0;
            gameObject.tag = "idle";
            Destroy(this.gameObject, 0.2f);
        }
    }

    void Fire()
    {
        if (!toFire)
        {
            if (SceneManager.GetActiveScene().name == "TyroneTest")
            {
                transform.RotateAround(Sun.transform.position, new Vector3(0, 0, 1), 2.0f * Time.deltaTime);
            }
            else
            {
                //gabe scene
                transform.RotateAround(transform.parent.position, Vector3.up, OrbitSpeed * Time.deltaTime);
            }
        }
        if (toFire && !Fired)
        {

            if (!parent.GetComponent<shooter>().hasShot)
            {
                Vector3 rotationvec;
                Vector3 Temp = startPos - Input.mousePosition;

                Temp.Normalize();
                rotationvec = Temp;
                rotationvec.z = rotationvec.y;
                rotationvec.y = 0;

                transform.rotation = Quaternion.LookRotation(rotationvec);                      //Doesnt follow curser when camera rotates, but still points in aimed direction
                this.transform.position += Temp * Mathf.Sin(Time.time * 10) * 0.02f;
            }
        }
        if (Input.GetMouseButtonUp(0) && toFire)
        {

            if (SceneManager.GetActiveScene().name == "TyroneTest")
            {
                Target = startPos - Input.mousePosition;
                Target = Target.normalized;
                this.gameObject.GetComponent<Rigidbody>().AddForce(Target * 500.0f, ForceMode.Impulse);
                Fired = true;
                Destroy(this.gameObject, 10);
            }
            else
            {
                if (!parent.GetComponent<shooter>().hasShot)
                {
                    parent.GetComponent<shooter>().hasShot = true;
                    Timer = 10;
                    Target = startPos - Input.mousePosition;
                    Target.z = Target.y;
                    Target.y = 0;
                    Target = Target.normalized;
                    this.gameObject.GetComponent<Rigidbody>().AddForce(Target * 500.0f, ForceMode.Impulse);
                    Fired = true;

                    //dynamic follow ai
                    gameObject.tag = "shot";

                    //FollowTimer = 10.0f;

                    //dynamic destuction (aka levelusion, aka destructable objects) AI
                    Destroy(this.gameObject, 10);                       //Change back to 10
                }
            }
        }

    }

    void Aimtracker()
    {
        if (!this.toFire)
        {
            spr.color = Color.clear;
        }
        else
        {
            LerpAlpha();
        }

        if(parent.GetComponent<shooter>().hasShot)
        {
            spr.color = Color.clear;
        }
    }

    void LerpAlpha()
    {
        lerpedAim = Color.Lerp(lerpedAim, Color.red, Time.deltaTime);
        spr.color = lerpedAim;
    }

    void OnMouseEnter()
    {
        if (!parent.GetComponent<shooter>().hasShot && !parent.GetComponent<shooter>().cursorLOCK)
        {
           // Debug.Log("we in");
            Cursor.visible = false;
            Hover = true;
        }
    }
    void OnMouseExit()
    {
        Cursor.visible = true;
        tex = cursorTexture;
        Hover = false;
    }

    void OnGUI()
    {
        if (Hover)
        {
           // Debug.Log("isss lit");
            GUI.DrawTexture(new Rect(MousePos.x - (w / 2), MousePos.y - (h / 2), w, h), hoverTexture);
        }
    }
}
