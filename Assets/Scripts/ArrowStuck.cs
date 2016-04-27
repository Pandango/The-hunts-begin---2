using UnityEngine;
using System.Collections;

public class ArrowStuck : MonoBehaviour {


    float depth = 0.1f; // how deep the arrow will enter the body

	void Start () {
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if ((hitInfo.gameObject.tag == "Arrow") || (hitInfo.gameObject.tag == "ArrowSuper"))
        {
            hitInfo.rigidbody.isKinematic = true; // stop physics control 
            hitInfo.transform.Translate(depth * Vector2.up); // move the arrow deep inside 
			hitInfo.transform.parent = transform; // stuck the arrow to the enemy
        }
    }
}
