using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

	public float speed = 60f;
	public float maxSpeed = 3f;
	public bool grounded;
	public float jumpForce = 8f;
	public bool isJumping = false;

	private Rigidbody2D rb2d;
	private Animator anim;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		anim.SetFloat ("Speed", Math.Abs( rb2d.velocity.x));	
		anim.SetBool ("Grounded", grounded);
		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded) {
			isJumping = true;
		}
	}

	void FixedUpdate(){
		Vector2 FixedVelocity = rb2d.velocity;
		FixedVelocity.x *= 0.75f;
		if (grounded) {
			rb2d.velocity = FixedVelocity;
		}
		float h = Input.GetAxis ("Horizontal");
		if (h < -0.1f) {//Movimiento izda
			transform.localScale = new Vector3(-1f,1f,1f);
		} else if (h > 0.1f) {//Movimiento dcha
			transform.localScale = new Vector3(1f,1f,1f);		}

		rb2d.AddForce(Vector2.right * speed * h);
		if (rb2d.velocity.x > maxSpeed){
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}
		if (rb2d.velocity.x < -maxSpeed){
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}

		//Comprobamos salto
		if (isJumping == true){
			rb2d.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
			isJumping = false;

		}
	}
	void OnBecameInvisible(){
		rb2d.position = new Vector3 (-1f, 1f, 1f);
	}
}