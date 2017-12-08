using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop : MonoBehaviour {

	public float vitesse = 0.1f;
	public float posVmax = -50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = gameObject.transform.position;
		position.y -= vitesse;
		gameObject.transform.position = position;

		if (gameObject.transform.position.y <= posVmax) {
			Destroy(gameObject);
		}
	}
}
