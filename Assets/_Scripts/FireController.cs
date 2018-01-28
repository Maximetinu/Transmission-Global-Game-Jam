using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

	//Intial position of the fire
	private Vector2 originPosition;
	private float speed = 2.0f;
	private float distanceUmbral = 2.0f;
	bool moveToOrigin = false;
	private Rigidbody2D myRigidbody;

	void Start () {
		if (OriginPositionSetted == false)
			originPosition = new Vector2(transform.position.x, transform.position.y);

		moveToOrigin = (originPosition != null && Vector2.Distance(originPosition,transform.position) >= distanceUmbral);
		myRigidbody = GetComponent<Rigidbody2D>();
		StartCoroutine(DisableColliderForSeconds(1.0f));
	}

	IEnumerator DisableColliderForSeconds(float nonCollisionTime)
	{
		GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds(nonCollisionTime);
		GetComponent<Collider2D>().enabled = true;
	}

	void FixedUpdate()
	{
		if (moveToOrigin){
			//GetComponent<Rigidbody2D>().AddForce(0.5f * acceleration * Time.time * myRigidbody.mass * (originPosition - new Vector2(transform.position.x, transform.position.y).normalized));
			GetComponent<Rigidbody2D>().velocity = (originPosition - new Vector2(transform.position.x, transform.position.y).normalized * speed);
			if (Vector2.Distance(originPosition, transform.position) <= distanceUmbral * 2.0f){
				transform.position = originPosition; 
				myRigidbody.velocity = Vector2.zero;
				moveToOrigin = false;
				myRigidbody.simulated = false;
			}
		}
	}

	public Vector2 GetOriginPosition(){
		return originPosition;
	}

	private bool OriginPositionSetted = false;
	public void SetOriginPosition(Vector2 pos){
		originPosition = pos;
		OriginPositionSetted = true;
	}
}
