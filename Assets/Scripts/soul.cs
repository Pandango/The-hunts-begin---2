using UnityEngine;
using System.Collections;

public class soul : MonoBehaviour {
    public int SoulCount;
    public GameObject playerSoul;
   
    //private playerStat playerStats;
    // Use this for initialization
    void Start () {
        SoulCount = 1;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.tag == "Player")
        {
            
            Destroy(playerSoul);
        }
    }
}
