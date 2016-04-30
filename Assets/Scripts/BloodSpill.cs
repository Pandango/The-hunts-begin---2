using UnityEngine;
using System.Collections;

public class BloodSpill : MonoBehaviour {

	public GameObject bloodFloor;

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if (hitInfo.gameObject.tag == "Ground")
		{
			var BloodSpill = (GameObject)Instantiate(bloodFloor, this.transform.position, Quaternion.identity);


			if ((transform.localScale.x < 0)) {
				BloodSpill.transform.localScale = new Vector3 (-BloodSpill.transform.localScale.x, BloodSpill.transform.localScale.y *(Mathf.Pow( -1 ,Random.Range(1, 3))) , BloodSpill.transform.localScale.z);
			} else {
				BloodSpill.transform.localScale = new Vector3 (BloodSpill.transform.localScale.x , BloodSpill.transform.localScale.y *(Mathf.Pow( -1 ,Random.Range(1, 3))) , BloodSpill.transform.localScale.z);
			}

			if ((transform.localScale.x < -1.2f) || (transform.localScale.x > 1.2f)) {
				BloodSpill.transform.localScale = new Vector3(BloodSpill.transform.localScale.x *1.5f , BloodSpill.transform.localScale.y , BloodSpill.transform.localScale.z);
			}

			Destroy(gameObject);
		}
	}
}
