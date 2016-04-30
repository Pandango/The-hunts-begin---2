using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
