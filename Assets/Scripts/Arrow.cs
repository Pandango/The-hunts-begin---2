using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

AudioSource Shot,Fire;

	// Use this for initialization
	void Start () {
	 	AudioSource[] audios = GetComponents<AudioSource>();
        Shot = audios[0];
        Fire = audios[1];
        Shot.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision hitInfo)
    {
        if ((hitInfo.gameObject.tag != "Player") && (hitInfo.gameObject.tag != "Arrow") && (hitInfo.gameObject.tag != "ArrowSuper") && (hitInfo.gameObject.tag != "Slash"))
        {
        	Fire.Play();
            Destroy(this.gameObject.GetComponent<Rigidbody>());
            Destroy(this.gameObject.GetComponent<Collider>());
        }
    }
}
