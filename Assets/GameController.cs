using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject a;
	public GameObject b;
	public GameObject c;
	public GameObject d;
	public GameObject sphere;

	private float timeCounter = 0;
	public float delay = 0.200f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void spawnNote(float notekey)
	{
		if (notekey == 1) {
			GameObject currentCube = Instantiate (sphere);
			currentCube.transform.position = a.transform.position;
		}
		else if (notekey == 2) {
			GameObject currentCube = Instantiate (sphere);
			currentCube.transform.position = b.transform.position;
		}
		else if (notekey == 3) {
			GameObject currentCube = Instantiate (sphere);
			currentCube.transform.position = c.transform.position;
		}
		else if (notekey == 4) {
			GameObject currentCube = Instantiate (sphere);
			currentCube.transform.position = d.transform.position;
		}

	}
}
