using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

	private Light _light;

	//Intial position of the fire
	public Vector3 position;

	//time for flashing
	private float minWaitTime =0.5f;
	private float maxWaitTime=2f;

	// Use this for initialization
	void Start () {
		position = new Vector2(this.position.x,this.position.y);
		_light = transform.GetComponentInChildren<Light> ();
		StartCoroutine (flashing ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	IEnumerator flashing (){
		while (true) {
			yield return new WaitForSeconds (Random.Range(minWaitTime,maxWaitTime));
			_light.enabled = !_light.enabled;
		}
	}
}
