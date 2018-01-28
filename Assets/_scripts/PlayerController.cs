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

	// BETTER JUMP!!
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2.0f;
	//public float range = 4f;

	//Parameter used for make configurable the radius increase of the light
	public float LightRangeVariator = 0.5f;
	private float LigthInitial = 40;
	public float LightMaxRange = 100;

	// Fall
	private float timeInAir = 0f;
	public float fallingFactor = 1.5f;
 	public float FallingDamageTimer = 3f;
	private bool damageWhenGround = false;

	private List<Vector2> flamesPositions = new List<Vector2>();
	public int VidaSinLlamas = 3;
	private int HealthPoints;

	private Rigidbody2D myRigidbody;
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
		HealthPoints = VidaSinLlamas;
		myRigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		StartLight ();

	}

	// Update is called once per frame
	void Update () {
		anim.SetFloat ("Speed", Math.Abs( myRigidbody.velocity.x));	
		anim.SetBool ("Grounded", grounded);
		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded) {
			isJumping = true;
		}
		if (myRigidbody.velocity.y < 0.0f)
			myRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1.0f) * Time.deltaTime;
		else if (myRigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
			myRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1.0f) * Time.deltaTime;
	}

	void Damage(float fallingTime){
		int vidaPerdida = Mathf.RoundToInt(Mathf.Pow(fallingTime, fallingFactor));
		HealthPoints -= vidaPerdida;
		Debug.Log("Me queda de vida: " + HealthPoints);
		if (vidaPerdida >= HealthPoints)
			PerderLlamas(flamesPositions.Count - 1);
		else
			PerderLlamas(vidaPerdida);

		if (HealthPoints <= 0)
			Die();
	}

	void PerderLlamas(int llamasPerdidas){
		aura.range -= LightRangeVariator * llamasPerdidas;
		for (int i = 0; i <= llamasPerdidas; i++){
			Debug.Log("Spawneando llama perdida");
			int selected = UnityEngine.Random.Range(0, flamesPositions.Count - 1);
			GameObject nuevaLlama = Instantiate(GameManager.instance.LlamaPrefabReference,transform.position, transform.rotation);
			nuevaLlama.GetComponent<Collider2D>().enabled = false;
			nuevaLlama.transform.position = new Vector3(nuevaLlama.transform.position.x, nuevaLlama.transform.position.y, -0.1f);
			nuevaLlama.GetComponent<FireController>().SetOriginPosition(flamesPositions[selected]);
			flamesPositions.RemoveAt(selected);
		}
	}

	void FixedUpdate(){
		Vector2 FixedVelocity = myRigidbody.velocity;
		FixedVelocity.x *= 0.75f;

		if (grounded) {
			
			myRigidbody.velocity = FixedVelocity;

			if (damageWhenGround){
				damageWhenGround = false;
				Damage(timeInAir);
			}
			timeInAir = 0.0f;

		} else {
			timeInAir += Time.deltaTime;
		}


		if (timeInAir >= FallingDamageTimer){
			damageWhenGround = true;
		}

		float h = Input.GetAxis ("Horizontal");
		if (h < -0.1f) {//Movimiento izda
			transform.localScale = new Vector3(-1f,1f,1f);
		} else if (h > 0.1f) {//Movimiento dcha
			transform.localScale = new Vector3(1f,1f,1f);
        }

		myRigidbody.AddForce(Vector2.right * speed * h);
		if (myRigidbody.velocity.x > maxSpeed){
			myRigidbody.velocity = new Vector2 (maxSpeed, myRigidbody.velocity.y);
		}
		if (myRigidbody.velocity.x < -maxSpeed){
			myRigidbody.velocity = new Vector2 (-maxSpeed, myRigidbody.velocity.y);
		}

		//Comprobamos salto
		if (isJumping == true){
			myRigidbody.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
			isJumping = false;
		}
		//aura = GetComponentInChildren<Light> ();
		//aura = GetComponents<Light>()[0];
		//Debug.Log ( GetComponentsInParent<Light>().Length);

	}

	void OnBecameInvisible(){
		myRigidbody.position = new Vector3 (-1f, 1f, 1f);
	}


	/**
	 * Increase range of the ligth player
	 **/

	private void IncreaseLight(){
		//aura = transform.GetChild(0).GetComponent<Light>();
		//Debug.Log (aura.spotAngle+";"+LightMaxRange);
		if (aura.range <= LightMaxRange) {
			aura.range += LightRangeVariator;
			//LightCurrent = aura.range;
		}
	}

	private void DecreaseLigth(){
		if (aura.range >= LigthInitial) {
			//aura = transform.GetComponentInChildren<Light> ();
			aura.range -= LightRangeVariator;
			//LightCurrent = aura.range;
		}
	}

	/**
	 * Decrease range of the ligth player
	 **/
	private void StartLight(){
		aura = transform.GetChild(0).GetComponent<Light>();
		
		// Inicializar valores a los seteados por editor para configurar el aura más visualmente
		LigthInitial = aura.range;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Llama"){
            IncreaseLight();
			AddFlamePosition(col.GetComponent<FireController>().GetOriginPosition());
            Destroy(col.gameObject);
		}
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

