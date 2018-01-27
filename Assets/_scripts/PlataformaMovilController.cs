using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovilController : MonoBehaviour {

	public float speed = 0.45f;
	public Transform target;
	private Vector3 start;
	private Vector3 end;
	// Use this for initialization
	void Start () {
		start = transform.position;
		end = target.position;
		if (target != null) {
			target.parent = null;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float modifiedSpeed = speed * Time.deltaTime;
		if (target != null) {
			transform.position = Vector3.MoveTowards (transform.position, target.position, modifiedSpeed);
		}
		if (transform.position == target.position) {
			target.position = (target.position == start)? end : start;
		}
		
	}
}
