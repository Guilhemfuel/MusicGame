using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

	public PartitionController partitionController;
	public ButtonController blueButtonController;
	public ButtonController yellowButtonController;
	public ButtonController redButtonController;
	public ButtonController greenButtonController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			partitionController.checkKey (1);
			blueButtonController.pressKey ();
		}

		if(Input.GetKeyUp(KeyCode.A)) {
			blueButtonController.releasedKey ();
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			partitionController.checkKey (2);
			yellowButtonController.pressKey ();
		}

		if(Input.GetKeyUp(KeyCode.Z)) {
			yellowButtonController.releasedKey ();
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			partitionController.checkKey (3);
			redButtonController.pressKey ();
		}

		if(Input.GetKeyUp(KeyCode.E)) {
			redButtonController.releasedKey ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			partitionController.checkKey (4);
			greenButtonController.pressKey ();
		}

		if(Input.GetKeyUp(KeyCode.R)) {
			greenButtonController.releasedKey ();
		}
	}
}
