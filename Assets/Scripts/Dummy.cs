using UnityEngine;
using System.Collections;

public class Dummy : MonoBehaviour {

	AudioSource Bleed;
	public GameObject BloodPrefab;

	// Use this for initialization
	void Start () {
		AudioSource[] audios = GetComponents<AudioSource>();
		Bleed = audios[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{

		if (hitInfo.gameObject.tag == "Slash") { //If Player got slashed by the blade.....
			Bleed.Play();
			var BloodSpill = (GameObject)Instantiate(BloodPrefab, hitInfo.transform.position, Quaternion.identity);
			BloodSpill.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5,5), Random.Range(-3,0));

			if (hitInfo.transform.position.x < this.transform.position.x) {
				BloodSpill.transform.localScale = new Vector3(-BloodSpill.transform.localScale.x , BloodSpill.transform.localScale.y , BloodSpill.transform.localScale.z);
			}
		}
	}
		

	void OnCollisionEnter2D(Collision2D hitInfo)
	{
		if ((hitInfo.gameObject.tag == "Arrow") || (hitInfo.gameObject.tag == "ArrowSuper")) //If Player was shot.....
		{
			Bleed.Play();
			var BloodSpill = (GameObject)Instantiate(BloodPrefab, hitInfo.transform.position, Quaternion.identity);
			BloodSpill.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5,5), Random.Range(-3,0));

			if (hitInfo.transform.position.x < this.transform.position.x) {
				BloodSpill.transform.localScale = new Vector3(-BloodSpill.transform.localScale.x , BloodSpill.transform.localScale.y , BloodSpill.transform.localScale.z);
			}
			if (hitInfo.gameObject.tag == "ArrowSuper") {
				BloodSpill.transform.localScale = new Vector3(BloodSpill.transform.localScale.x *1.5f , BloodSpill.transform.localScale.y , BloodSpill.transform.localScale.z);
			}
		}
	}
}
