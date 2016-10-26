using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
public Animator anim;
	// Use this for initialization
	void Start () {
	anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown("1"))
	{

		anim.Play("Player_Idle", -1, 0f);
	}
	if(Input.GetKeyDown("2"))
	{

		anim.Play("Player_Fire", -1, 0f);
	}
	}
}
