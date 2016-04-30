using UnityEngine;
using System.Collections;

public class bushEater : MonoBehaviour {
    private playerStat playerStats;
    private Animator anim;
    private objectInteractionController inBush;
    // Use this for initialization
    void Start () {
        playerStats = GameObject.Find("Player").GetComponent<playerStat>();
        inBush = GameObject.Find("Player").GetComponent<objectInteractionController>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftControl) && inBush.isEnterBush && !inBush.isGetInBush)
        {
            anim.SetTrigger("inBush");
        }
    }
   
}
