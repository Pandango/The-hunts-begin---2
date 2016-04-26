using UnityEngine;
using System.Collections;

public class ArrowStuck : MonoBehaviour {

    float depth = 0.30f; // how deep the arrow will enter the body

    void OnCollisionEnter(Collision hitInfo)
    {
        if ((hitInfo.gameObject.tag == "Arrow") || (hitInfo.gameObject.tag == "ArrowSuper"))
        {
            hitInfo.rigidbody.isKinematic = true; // stop physics control 
            hitInfo.transform.Translate(depth * Vector3.forward); // move the arrow deep inside 
            hitInfo.transform.parent = transform; // stuck the arrow to the enemy
        }
    }
}
