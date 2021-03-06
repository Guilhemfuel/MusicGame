﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop : MonoBehaviour {

	public float vitesse = 0.1f;
	public float posVmax = -50;

	private Vector3 startPosition;
	private Vector3 EndPosition;

	private float Timer;
	private float TimeToFall = 2f;

	// Use this for initialization
	void Start () {
		Camera cam = Camera.main;
		float height = 2f * cam.orthographicSize;

		startPosition = transform.position;
		EndPosition = startPosition + Vector3.down * height;
	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;

		float t = Timer / TimeToFall;

		Vector3 position = Vector3.Lerp(startPosition, EndPosition, t);
		gameObject.transform.position = position;

		if (t >= 1) {
			Destroy(gameObject);
		}
	}
}
