using UnityEngine;
using System.Collections;

public class Totem : MonoBehaviour {
    public GameObject potionPrefab;
    Transform potionSpawnPoint;
	// Use this for initialization
	void Start () {
	 StartCoroutine(PotionSpawn());
	}
	
	// Update is called once per frame
	void Update () {
       
	}
    IEnumerator PotionSpawn()
    {
        yield return new WaitForSeconds(20f);
        InvokeRepeating("potionSpawner", 0.1f,20f);
    }
    void potionSpawner()
    {
        if (potionSpawnPoint == null)
            potionSpawnPoint = gameObject.transform.Find("PotionSpawnPoint"); // todo use full path for faster

        // instantiat 1 bullet

        var soulSpawn = (GameObject)Instantiate(potionPrefab, potionSpawnPoint.position, potionSpawnPoint.rotation);
    }
}
