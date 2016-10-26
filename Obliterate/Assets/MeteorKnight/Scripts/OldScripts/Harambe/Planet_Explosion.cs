using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Planet_Explosion : MonoBehaviour
{

    public GameObject Shatter;
    bool Explode = false;
    float Timer = 5;
    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Planet_Explosions")
            {
                Debug.Log("Done");
                child.gameObject.SetActive(false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Score.Dead)
        {
            Timer -= Time.deltaTime;
            Destroy(GameObject.Find("LifePlanet_Whole"));
            Destroy(GameObject.Find("EarthAtmosphere"));
            foreach (Transform child in transform)
            {
                if (child.tag == "Planet_Explosions")
                {
                    Debug.Log("Done");
                    child.gameObject.SetActive(true);
                }

            }
            if (!Explode)
            {
                Instantiate(Shatter);
                GetComponent<AudioSource>().Play();
                Explode = true;
            }
            if (Timer <= 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
