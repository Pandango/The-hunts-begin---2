using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    public Slider Healthbar;
    //public Slider Staminabar;
    public GameObject hpBlank;
    //public GameObject spBlank;

    //hp&sp test
    public float damage;
    //public float sp;
    float MaxHp = 100.0f;
    //float MaxSp = 50.0f;

    // Use this for initialization
    void Start()
    {
        Healthbar.maxValue = MaxHp;
        //Staminabar.maxValue = MaxSp;

        Healthbar.value = MaxHp;
        //Staminabar.value = MaxSp;
    }

    // Update is called once per frame
    void Update()
    {
        playerHit();
        //playerSpUsed();
    }

    //คำนวณ HP ของ player
    void playerHit()
    {
        Healthbar.value -= damage;
        damage = 0;                             //เซ็ต damage เป็น 0 (ใช้ test)

        if (Healthbar.value == 0)
        {
            hpBlank.SetActive(false);
        }
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
