using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

AudioSource Shot,Hit;

	// Use this for initialization
	void Start () {
	 	AudioSource[] audios = GetComponents<AudioSource>();
        Shot = audios[0];
        Hit = audios[1];
        Shot.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
		if ((hitInfo.gameObject.tag != "Arrow") && (hitInfo.gameObject.tag != "ArrowSuper") && (hitInfo.gameObject.tag != "Slash"))
        {
			Hit.Play();
            Destroy(this.gameObject.GetComponent<Rigidbody2D>());
            Destroy(this.gameObject.GetComponent<Collider2D>());
        }
    }
}
