using UnityEngine;
using System.Collections;

public class MushroomJump : MonoBehaviour {


    // Use this for initialization
    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if ((hitInfo.gameObject.tag == "Player"))
        {
            Debug.Log("JUMP!!");
            hitInfo.rigidbody.velocity = new Vector2(0, 30);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
