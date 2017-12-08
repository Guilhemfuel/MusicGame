using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			print("space A was pressed");
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			print("space Z was pressed");
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			print("space E was pressed");
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			print("space R was pressed");
		}
	}
}
