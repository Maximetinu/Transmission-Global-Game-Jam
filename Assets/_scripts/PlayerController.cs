using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour { 
    public static PlayerController instance = null;

    public float speed = 60f;
	public float maxSpeed = 3f;
	public bool grounded;
	public float jumpForce = 8f;
	public bool isJumping = false;

	private List<Vector2> flamesPositions = new List<Vector2>();

	private Rigidbody2D rigidbody;
	private Animator anim;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		anim.SetFloat ("Speed", Math.Abs( rigidbody.velocity.x));	
		anim.SetBool ("Grounded", grounded);
		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded) {
			isJumping = true;
		}
	}

	void FixedUpdate(){
		Vector2 FixedVelocity = rigidbody.velocity;
		FixedVelocity.x *= 0.75f;
		if (grounded) {
			rigidbody.velocity = FixedVelocity;
		}
		float h = Input.GetAxis ("Horizontal");
		if (h < -0.1f) {//Movimiento izda
			transform.localScale = new Vector3(-1f,1f,1f);
		} else if (h > 0.1f) {//Movimiento dcha
			transform.localScale = new Vector3(1f,1f,1f);
        }

		rigidbody.AddForce(Vector2.right * speed * h);
		if (rigidbody.velocity.x > maxSpeed){
			rigidbody.velocity = new Vector2 (maxSpeed, rigidbody.velocity.y);
		}
		if (rigidbody.velocity.x < -maxSpeed){
			rigidbody.velocity = new Vector2 (-maxSpeed, rigidbody.velocity.y);
		}

		//Comprobamos salto
		if (isJumping == true){
			rigidbody.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
			isJumping = false;
		}
	}

	void OnBecameInvisible(){
		rigidbody.position = new Vector3 (-1f, 1f, 1f);
	}

    void Die()
    {
		StartCoroutine(GameManager.instance.FadeOut());
		GameManager.instance.StartGame();
    }

	public void AddFlamePosition(Vector2 newFlamePos){
		flamesPositions.Add(newFlamePos);
	}


}
