using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawLineController : MonoBehaviour {
	public Transform from;
	public Transform to;
	void OnDrawGizmosSelected (){
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine (from.position, to.position);
		Gizmos.DrawSphere (from.position, 0.15f);
		Gizmos.DrawSphere (to.position, 0.15f);
	}
}
