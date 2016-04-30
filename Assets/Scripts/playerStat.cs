using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerStat : MonoBehaviour
{

    //public Slider Healthbar;
    //public Slider Staminabar;
    //public GameObject hpBlank;
    //public GameObject spBlank;
    
    //hp&sp test
    public int damage;
    //public float sp;
    public int MaxHp = 100;
    public int currentHp;   
    public int PlayerSoul;
    public int PlayerCurrentSoul = 1;
    public GameObject Player;
    public GameObject SoulPrefabs;
    playerHunter PlayerHunter;
    objectInteractionController objInteractCtrl;
   
    Transform soulDrop = null;
    //float MaxSp = 50.0f;

    // Use this for initialization
    void Start()
    {
        
        PlayerCurrentSoul = 1;
        currentHp = MaxHp;
        //Healthbar.maxValue = MaxHp;
        //Staminabar.maxValue = MaxSp;

        //Healthbar.value = MaxHp;
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
        if(currentHp > MaxHp)
        {
            currentHp = MaxHp;
        }

        if(currentHp <= 0)
        {
           
            playerDeath();
        }
    }
    void playerDeath()
    {
        SoulDrop();
        
        StartCoroutine(death());
        Destroy(this.gameObject, 0.1f);
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
