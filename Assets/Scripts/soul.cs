using UnityEngine;
using System.Collections;

public class soul : MonoBehaviour {
    public float SoulCount = 1;
    public GameObject playerSoul;
   
    //private playerStat playerStats;
    // Use this for initialization
    void Start () {
		SoulCount = (transform.localScale.x)*4f;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
		if(hitInfo.gameObject.tag == "Player" || hitInfo.gameObject.tag == "Slash")
        {
            
            Destroy(playerSoul);
        }
    }
}
