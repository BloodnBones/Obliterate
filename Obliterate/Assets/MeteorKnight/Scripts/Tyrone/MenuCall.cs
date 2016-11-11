using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCall : MonoBehaviour {

    public Transform pause;
    public Transform gameOver;
    public GameObject[] Planets;
    public GameObject[] Asteroids;
    GameObject Active;
    public Text Shots;
    public Text Worlds;
    int StartShots;
    
    //bool paused;

	// Use this for initialization
	void Start () {
        pause.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        Asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        StartShots = Asteroids.Length;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && gameOver.gameObject.activeSelf != true)
        {
            Pause();
        }

        //Game Over stuff
        Planets = GameObject.FindGameObjectsWithTag("Planet");
        Asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        Active = GameObject.FindGameObjectWithTag("shot");
        if (Planets.Length == 0 || Asteroids.Length == 0 && Active == null)
        {
            Shots.text = (StartShots - Asteroids.Length).ToString();
            Worlds.text = Planets.Length.ToString();
            gameOver.gameObject.SetActive(true);
        }
    }

    public void Pause()
    {

        if (pause.gameObject.activeInHierarchy == false)
        {
            pause.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pause.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

    }

    
}
