using UnityEngine;
using System.Collections;

public class bushEater : MonoBehaviour {
    //private playerStat playerStats;
    private Animator anim;
    public bool playerInBush;
    // Use this for initialization
    void Start () {
        playerInBush = false;
        anim = this.gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
   void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.tag == "Player" && !playerInBush)
        {           
            playerInBush = true;
            
        }
        if ( hitInfo.tag == "Player" && playerInBush)
        {
            anim.SetTrigger("inBush");

        }
    }
    void OnTriggerStay2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Player" && !playerInBush)
        {
            playerInBush = true;
           
        }
       
    }
    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            playerInBush = false;
            
        }
    }
}
