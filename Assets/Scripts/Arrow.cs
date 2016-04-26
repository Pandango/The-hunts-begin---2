using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision hitInfo)
    {
        if ((hitInfo.gameObject.tag != "Player") && (hitInfo.gameObject.tag != "Arrow") && (hitInfo.gameObject.tag != "ArrowSuper") && (hitInfo.gameObject.tag != "Slash"))
        {
            Destroy(this.gameObject.GetComponent<Rigidbody>());
            Destroy(this.gameObject.GetComponent<Collider>());
        }
    }
}
