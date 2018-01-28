﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public float factor = 0.01f;
	private float playerInitialPositionY;
	private float currentPlayerPositionY;

	// Use this for initialization
	void Start () {
		playerInitialPositionY = currentPlayerPositionY = PlayerController.instance.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(transform.position.x, playerInitialPositionY + PlayerController.instance.transform.position.y * factor);
	}
}