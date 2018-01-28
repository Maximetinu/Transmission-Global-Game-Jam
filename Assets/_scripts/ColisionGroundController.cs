using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionGroundController : MonoBehaviour {

	private PlayerController player;
	//private Rigidbody2D rg2d;
	// Use this for initialization
	void Start () {
		player = GetComponentInParent<PlayerController> ();
		//rg2d = GetComponentInParent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == LayersAndTags.Platform) {
			//rg2d.velocity = new Vector3 (0f, 0f, 0f);
			player.grounded = true;
			player.transform.parent = col.transform;
		}	
	}
	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == LayersAndTags.Suelo) {
			player.grounded = true;
		}
		if (col.gameObject.tag == LayersAndTags.Platform) {
			player.grounded = true;
			player.transform.parent = col.transform;
		}	
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == LayersAndTags.Suelo) {
			player.grounded = false;
		}
		if (col.gameObject.tag == LayersAndTags.Platform) {
			player.grounded = false;
			player.transform.parent =null;
		}	}
}
