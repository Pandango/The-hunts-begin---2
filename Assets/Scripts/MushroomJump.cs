using UnityEngine;
using System.Collections;

public class MushroomJump : MonoBehaviour {

    private Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if ((hitInfo.gameObject.tag == "Player"))
        {
            Debug.Log("JUMP!!");
            anim.SetTrigger("isJumpped");
            hitInfo.rigidbody.velocity = new Vector2(0, 30);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
