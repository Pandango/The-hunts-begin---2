using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

AudioSource Shot,Hit;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
	 	AudioSource[] audios = GetComponents<AudioSource>();
        Shot = audios[0];
        Hit = audios[1];
        Shot.Play();
	}



    void OnCollisionEnter2D(Collision2D hitInfo)
    {
		if ((hitInfo.gameObject.tag != "Arrow") && (hitInfo.gameObject.tag != "ArrowSuper"))
        {
			GetComponent<ArrowFlying>().enabled = false;
			Hit.Play();
            Destroy(this.gameObject.GetComponent<Rigidbody2D>());
            Destroy(this.gameObject.GetComponent<Collider2D>());
        }
    }

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if ((hitInfo.gameObject.tag == "Slash"))
		{
			if (hitInfo.transform.position.x > this.transform.position.x) {
				rb2d.velocity = new Vector2 (-50, rb2d.velocity.y);
			} else {
				rb2d.velocity = new Vector2 (50, rb2d.velocity.y);
			}
		}
	}
}
