using UnityEngine;
using System.Collections;

public class objectInteractionController : MonoBehaviour {

    private playerHunter player;
    private Rigidbody2D rb2d;
    public Renderer playerLeft,playerRight,playerLeg;

    [SerializeField]
    public bool isEnter = false;

    [SerializeField]
    bool isEnterBush = false;
    bool isGetInBush = false;
    [SerializeField]
    string caveName;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();        
        player = gameObject.GetComponent<playerHunter>();
    }

    void Update(){
        EnterInBushs();
        EnterInCave();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Cave")
        {
            isEnter = true;
            caveName = hitInfo.name;
        }
        else if (hitInfo.tag == "Bush")
        {
            isEnterBush = true;
        }
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Cave")
        {
            isEnter = true;
            caveName = hitInfo.name;
        }
        else if (hitInfo.tag == "Bush")
        {
            isEnterBush = true;
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Cave")
        {
            isEnter = false;
            caveName = "";
        }
        else if (hitInfo.tag == "Bush")
        {
            isEnterBush = false;
        }
    }


    void EnterInCave()
    {
        GameObject cave2Pos = GameObject.Find("Cave2");
        GameObject cave1Pos = GameObject.Find("Cave1");

        if (Input.GetKeyDown(KeyCode.LeftControl) && isEnter && caveName == "Cave1")
        {
            this.transform.position = cave2Pos.transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && isEnter && caveName == "Cave2")
        {
            this.transform.position = cave1Pos.transform.position;
        }
    }

    void EnterInBushs()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isEnterBush && !isGetInBush)
        {
            rb2d.velocity = new Vector2(0, 0);
            playerLeft.sortingLayerName = "playerHide";
            playerRight.sortingLayerName = "playerHide";
            playerLeg.sortingLayerName = "playerHide";
            playerLeft.enabled = false;
            playerRight.enabled = false;
            playerLeg.enabled = false;
            player.enabled = false;
            isGetInBush = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && isEnterBush && isGetInBush)
        {
            playerLeft.sortingLayerName = "player";
            playerRight.sortingLayerName = "player";
            playerLeg.sortingLayerName = "player";
            //playerLeft.enabled = true;
            playerRight.enabled = true;
            playerLeg.enabled = true;
            player.enabled = true;
            isGetInBush = false;
        }

    }
}
