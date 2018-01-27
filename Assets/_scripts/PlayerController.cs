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
	public float range = 4f;

	//Parameter used for make configurable the radius increase of the light
	private float LightRangeVariator = 2f;
	private float LightCurrent;
	private float LigthInitial = 18;
	private float LightMaxRange = 150f;
	private float LightMinRange = 4f;

<<<<<<< HEAD
	// Fall
	private float timeInAir = 0f;
 	public float FallingDeathTimer = 3f;
=======
>>>>>>> 09a6263a8752df61877be5ca4825aeb3567e8023

	private List<Vector2> flamesPositions = new List<Vector2>();

	private Rigidbody2D rigidbody;
	private Animator anim;
	private Light aura;

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
		StartLight ();

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
			Debug.Log("timeInAir --> " +  timeInAir);
		} else {
			timeInAir += Time.deltaTime;
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
		//aura = GetComponentInChildren<Light> ();
		//aura = GetComponents<Light>()[0];
		//Debug.Log ( GetComponentsInParent<Light>().Length);

		IncreaseLight ();

		if (timeInAir >= FallingDeathTimer) 
		{
			Die();
		}

	}

	void OnBecameInvisible(){
		rigidbody.position = new Vector3 (-1f, 1f, 1f);
	}


	/**
	 * Increase range of the ligth player
	 **/

	private void IncreaseLight(){
		aura = transform.GetChild(0).GetComponent<Light>();
		//Debug.Log (aura.spotAngle+";"+LightMaxRange);
		if (aura.spotAngle <= LightMaxRange) {
			aura.spotAngle += LightRangeVariator;
			LightCurrent = aura.range;
		}
	}

	private void DecreaseLigth(){
		if (aura.spotAngle >= LightMinRange) {
			aura = transform.GetComponentInChildren<Light> ();
			aura.spotAngle -= LightRangeVariator;
			LightCurrent = aura.range;
		}
	}

	/**
	 * Decrease range of the ligth player
	 **/
	private void StartLight(){
		aura = transform.GetChild(0).GetComponent<Light>();
		aura.spotAngle = LigthInitial;
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("incrementando llama");
		IncreaseLight ();
		AddFlamePosition(col.transform.position);
		Destroy (col.gameObject);
	}

    void Die()
    {
		Debug.Log("Death");
		StartCoroutine(GameManager.instance.FadeOut());
		GameManager.instance.StartGame();
    }

	public void AddFlamePosition(Vector2 newFlamePos){
		flamesPositions.Add(newFlamePos);
	}



}

