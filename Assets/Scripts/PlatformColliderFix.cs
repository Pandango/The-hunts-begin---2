using UnityEngine;
using System.Collections;

public class PlatformColliderFix : MonoBehaviour
{

    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.tag == "TopCollCheck")
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        else if (hitInfo.tag == "BotCollCheck")
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = false;

        }
    }
}
