using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

	public PartitionController partitionController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			partitionController.checkKey (1);
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			partitionController.checkKey (2);
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			partitionController.checkKey (3);
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			partitionController.checkKey (4);
		}
	}
}
