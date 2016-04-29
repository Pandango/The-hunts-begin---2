using UnityEngine;
using System.Collections;

public class ArrowFlying : MonoBehaviour {

	Vector3 lastPosition; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		var direction = (transform.position - lastPosition).normalized;
		lastPosition = transform.position;
		transform.up = direction;
	}
}
