using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour {

	Transform target;
	public Vector2 currentDirection = Vector2.up;
	public float speed = 5.0f;
	public float angularSpeed = 2.0f;

	private Rigidbody2D myRB;

	// Use this for initialization
	void Start () {
		target = PlayerController.instance.transform;
		myRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        myRB.velocity = currentDirection.normalized * speed;
		currentDirection = Vector2.Lerp(currentDirection, (target.position - transform.position), angularSpeed * Time.deltaTime);
		transform.rotation = Quaternion.LookRotation(currentDirection, Vector2.up);
		//currentDirection = Quaternion.LookRotation
		//transform.rotation = currentDirection;
		//currentDirection
	}
}
