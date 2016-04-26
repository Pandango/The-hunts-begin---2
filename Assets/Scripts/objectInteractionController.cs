using UnityEngine;
using System.Collections;

public class objectInteractionController : MonoBehaviour {

    private CharacterController characterContoller;
    SpriteRenderer playerRenderer;

    [SerializeField]
    public bool isEnter = false;

    [SerializeField]
    bool isEnterBush = false;
    bool isGetInBush = false;
    [SerializeField]
    string caveName;

    void Start()
    {
        characterContoller = GetComponent<CharacterController>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        EnterInBushs();
        EnterInCave();
    }

    void OnTriggerEnter(Collider hitInfo)
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

    void OnTriggerExit(Collider hitInfo)
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
            playerRenderer.sortingLayerName = "playerHide";
            playerRenderer.enabled = false;
            characterContoller.enabled = false;
            isGetInBush = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && isEnterBush && isGetInBush)
        {
            playerRenderer.sortingLayerName = "player";
            playerRenderer.enabled = true;
            characterContoller.enabled = true;
            isGetInBush = false;
        }

    }
}
