using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerStat : NetworkBehaviour
{

    public Slider Healthbar;
    //public Slider Staminabar;
    public GameObject hpBlank;
    //public GameObject spBlank;
    
    //hp&sp test
    public int damage;
    //public float sp;
//<<<<<<< HEAD
	public float MaxHp = 100;
	public float currentHp;   
    public float PlayerSoul;
    public float PlayerCurrentSoul = 1;

//>>>>>>> origin/master
    public GameObject Player;
	public GameObject SoulPrefabs , BloodPrefab;
    playerHunter PlayerHunter;
    objectInteractionController objInteractCtrl;
	public ParticleSystem bloodshedEffect;
   
	bool DIE = false;

    Transform soulDrop = null;
    //float MaxSp = 50.0f;

    // Use this for initialization
    void Start()
    {

        PlayerCurrentSoul = 1;
        currentHp = MaxHp;
        Healthbar.maxValue = currentHp;
        //Healthbar.value = currentHp;
        //Staminabar.maxValue = MaxSp;
        
        //Staminabar.value = MaxSp;
        PlayerHunter = GetComponent<playerHunter>();
        objInteractCtrl = GetComponent<objectInteractionController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSoul = PlayerCurrentSoul;
        //playerHit();
        //playerSpUsed();
        currentHpCtrl();
        
    }

    //คำนวณ HP ของ player
   /* void playerHit()
    {
        Healthbar.value -= damage;
        damage = 0;                             //เซ็ต damage เป็น 0 (ใช้ test)

        if (Healthbar.value == 0)
        {
            hpBlank.SetActive(false);
        }
    }*/

    

    void currentHpCtrl()
    {
        Healthbar.value = currentHp;
        if (currentHp > MaxHp)
        {
            currentHp = MaxHp;
        }

		if((currentHp <= 0) && !DIE)
        {
//<<<<<<< HEAD
			DIE = true;
			Destroy(this.gameObject.GetComponent<Collider2D>());
			SoulDrop();
            //playerDeath();
//=======
            hpBlank.SetActive(false);
            playerDeath();
//>>>>>>> origin/master
        }
    }
    void playerDeath()
    {
        SoulDrop();
        
        //StartCoroutine(death());
        //Destroy(this.gameObject, 0.1f);
    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    void SoulDrop()
    {
       
        if (soulDrop == null)
            soulDrop = gameObject.transform.Find("SoulSpawn"); // todo use full path for faster

        // instantiat 1 bullet

		var soulSpawn = (GameObject)Instantiate(SoulPrefabs, soulDrop.position,soulDrop.rotation);
		soulSpawn.transform.localScale = new Vector3(PlayerCurrentSoul/4f , PlayerCurrentSoul/4f  , PlayerCurrentSoul/4f );

		ParticleSystem newFireParticleSystem = Instantiate(bloodshedEffect, transform.position, Quaternion.identity) as ParticleSystem;
		Destroy(newFireParticleSystem.gameObject, 2);

		var BloodSpill1 = (GameObject)Instantiate(BloodPrefab, transform.position, Quaternion.identity);
		BloodSpill1.transform.localScale = new Vector3(Random.Range(-3,0) , BloodSpill1.transform.localScale.y , BloodSpill1.transform.localScale.z);
		BloodSpill1.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-8,-2), Random.Range(-2,6));

		var BloodSpill2 = (GameObject)Instantiate(BloodPrefab, transform.position, Quaternion.identity);
		BloodSpill2.transform.localScale = new Vector3(Random.Range(1,4) , BloodSpill2.transform.localScale.y , BloodSpill2.transform.localScale.z);
		BloodSpill2.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3,9), Random.Range(-2,6));

		var BloodSpill3 = (GameObject)Instantiate(BloodPrefab, transform.position, Quaternion.identity);
		BloodSpill3.transform.localScale = new Vector3(Random.Range(-2,0) , BloodSpill3.transform.localScale.y , BloodSpill3.transform.localScale.z);
		BloodSpill3.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-8,9), Random.Range(2,6));

		var BloodSpill4 = (GameObject)Instantiate(BloodPrefab, transform.position, Quaternion.identity);
		BloodSpill4.transform.localScale = new Vector3(Random.Range(1,3) , BloodSpill4.transform.localScale.y , BloodSpill4.transform.localScale.z);
		BloodSpill4.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-8,9), Random.Range(2,6));

		var BloodSpill5 = (GameObject)Instantiate(BloodPrefab, transform.position, Quaternion.identity);
		BloodSpill5.transform.localScale = new Vector3(Random.Range(-4,0) , BloodSpill5.transform.localScale.y , BloodSpill5.transform.localScale.z);
		BloodSpill5.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-8,0), Random.Range(-5,0));

		var BloodSpill6 = (GameObject)Instantiate(BloodPrefab, transform.position, Quaternion.identity);
		BloodSpill6.transform.localScale = new Vector3(Random.Range(1,5) , BloodSpill6.transform.localScale.y , BloodSpill6.transform.localScale.z);
		BloodSpill6.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1,9), Random.Range(-5,0));

		Destroy(gameObject);
    }
    //คำนวณการใช้ stamina ของ player
    //void playerSpUsed()
    //{
    //    Staminabar.value -= sp;
    //    sp = 0;                                //เซ็ต sp ที่ใช้ไป เป็น 0 (ใช้ test)

    //    if (Staminabar.value == 0)
    //    {
    //        spBlank.SetActive(false);
    //    }
    //}
}
