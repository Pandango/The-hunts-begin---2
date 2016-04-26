using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	
	public static float skillCD0= 0, skillCD1 = 0, skillCD2 = 0, skillCD3 = 0;
    public static float skillCD0_show = 0, skillCD1_show = 0, skillCD2_show = 0, skillCD3_show = 0;
    public Text SkillText0, SkillText1, SkillText2 , SkillText3;
    public Image SkillPic0, SkillPic1, SkillPic2, SkillPic3;
    bool isCD = false;
	
    void Start()
    {
        
    }

	void Update () {
        if(skillCD0 == 0)
        {
            SkillPic0.color = Color.white;
            SkillText0.color = Color.black;
            SkillText0.text = "LClick";
        }
        else
        {
            SkillPic0.color = Color.red;
            SkillText0.color = Color.red;
            SkillText0.text = "CD :" + skillCD0_show.ToString();
        }

        if (skillCD1 == 0)
        {
            SkillPic1.color = Color.white;
            SkillText1.color = Color.black;
            SkillText1.text = "MClick";
        }
        else
        {
            SkillPic1.color = Color.red;
            SkillText1.color = Color.red;
            SkillText1.text = "CD :" + skillCD1_show.ToString();
        }

        if (skillCD2 == 0)
        {
            SkillPic2.color = Color.white;
            SkillText2.color = Color.black;
            SkillText2.text = "RClick";
        }
        else
        {
            SkillPic2.color = Color.red;
            SkillText2.color = Color.red;
            SkillText2.text = "CD :" + skillCD2_show.ToString();
        }

        if (skillCD3 == 0)
        {
            SkillPic3.color = Color.white;
            SkillText3.color = Color.black;
            SkillText3.text = "LShift";
        }
        else
        {
            SkillPic3.color = Color.red;
            SkillText3.color = Color.red;
            SkillText3.text = "CD :" + skillCD3_show.ToString();
        }

        if (!isCD) { StartCoroutine(CoolDown()); }
        
    }

    IEnumerator CoolDown()
    {
        isCD = true;

        yield return new WaitForSeconds(0.5f);
        if (skillCD0 > 0)
        {
            skillCD0_show = Mathf.Ceil(skillCD0);
            skillCD0 -= 0.5f;
            
        }
        if (skillCD1 > 0)
        {
            skillCD1_show = Mathf.Ceil(skillCD1);
            skillCD1 -= 0.5f;
            
        }
        if (skillCD2 > 0)
        {
            skillCD2_show = Mathf.Ceil(skillCD2);
            skillCD2 -= 0.5f;

        }
        if (skillCD3 > 0)
        {
            skillCD3_show = Mathf.Ceil(skillCD3);
            skillCD3 -= 0.5f;

        }

        isCD = false;
    }
    

    }
