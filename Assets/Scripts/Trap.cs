using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	AudioSource Break;
	// Use this for initialization
	void Start () {
		AudioSource[] audios = GetComponents<AudioSource>();
		Break = audios[0];
	}
	
	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if (hitInfo.gameObject.tag == "Ground")
		{
			Destroy(this.gameObject.GetComponent<Rigidbody2D>());
		}

		if (hitInfo.gameObject.tag == "Player")
		{
			Destroy(gameObject,3);
			Break.Play ();
		}
	}
}
