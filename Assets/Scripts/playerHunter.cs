using UnityEngine;
using System.Collections;

public class playerHunter : MonoBehaviour {

    private CharacterController characterContoller;
    public bool isGrounded;
    bool faceRight = true;
    bool isShoot = false;
    bool PowerShot = false;

    [SerializeField]
    bool isEnter = false;

    public AudioClip shootClip;
    public GameObject ArrowPrefab,ArrowSuperPrefab,SlashPrefab, TrapPrefab;
    public float shootForce,slashForce, slashPower;
    public KeyCode trap;


    Transform bow = null;

    public float gravity = 15.5f;
    private float fallSpeed;
    public float jumpSpeed = 5;
    public float moveSpeed = 5;
    private float speedx;
    // Use this for initialization
    void Start () {
        characterContoller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        MoveMachineMouse();
        groundCheck();
        Fall();
        Jump();
        Move();


        if ((Input.GetMouseButton(0)) && (ScoreController.skillCD0 == 0))
        {
            Shoot();
        }
        if ((Input.GetMouseButton(2)) && (ScoreController.skillCD1 == 0))
        {
            ShootSuper();
        }
        if ((Input.GetMouseButton(1)) && (ScoreController.skillCD2 == 0))
        {
            Slash();
        }
        if ((Input.GetKeyDown(trap)) && (ScoreController.skillCD3 == 0))
        {
            Trap();
        }
    }

    void groundCheck()
    {
        //using raycast need start position/direction/magnitude,lenght
        isGrounded = (Physics.Raycast(transform.position, -transform.up, characterContoller.height / 2.8f));
    }

    void Move()
    {
        speedx = Input.GetAxis("Horizontal");
        if(speedx != 0)
        {
            characterContoller.Move(new Vector3(speedx, 0) * moveSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            fallSpeed = -jumpSpeed;
        }
    }

    void Fall()
    {
        if (!isGrounded)
        {
            fallSpeed += gravity * Time.deltaTime;
        }
        else
        {
            if (fallSpeed > 0) fallSpeed = 0;
        }
        characterContoller.Move(new Vector3(0, -fallSpeed*Time.deltaTime));
    }

    void Flip()
    {
        faceRight = !faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    void MoveMachineMouse()
    {
        if (bow == null)
            bow = gameObject.transform.Find("ShootPoint");
        /*Vector2 dir = Input.mousePosition - transform.position;
        float angle = Mathf.Tan(dir.y / dir.x) * Mathf.Rad2Deg;
        
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));*/

        /*var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(mousePosition - transform.position, transform.TransformDirection(Vector3.forward));
        transform.rotation = new Quaternion(0, 0, rot.z -0.1f, rot.w - 0.1f);*/

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bow.rotation = Quaternion.LookRotation(Vector3.forward , mousePos - transform.position);

        if (mousePos.x > transform.position.x && !faceRight)
        {
            Flip();
        }
        else if (mousePos.x < transform.position.x && faceRight)
        {
            Flip();
        }

    }

        void Shoot()
    {

        if (bow == null)
            bow = gameObject.transform.Find("ShootPoint"); // todo use full path for faster

        // instantiat 1 bullet

        var Arrow = (GameObject)Instantiate(ArrowPrefab, bow.position, bow.rotation);
        Arrow.GetComponent<Rigidbody>().velocity = bow.TransformDirection(new Vector3(0, shootForce, 0));
        //Bullet2.GetComponent<Rigidbody>().AddForce(new Vector3 (10,20,shootForce));
        ScoreController.skillCD0 = 1f;
        ScoreController.skillCD2 = 0.5f;
    }

    void ShootSuper()
    {

        if (bow == null)
            bow = gameObject.transform.Find("ShootPoint"); // todo use full path for faster

        // instantiat 1 bullet

        var ArrowSuper = (GameObject)Instantiate(ArrowSuperPrefab, bow.position, bow.rotation);
        ArrowSuper.GetComponent<Rigidbody>().velocity = bow.TransformDirection(new Vector3(0, shootForce*5/2, 0));
        //Bullet2.GetComponent<Rigidbody>().AddForce(new Vector3 (10,20,shootForce));

        ScoreController.skillCD1 = 10f;
    }

    void Slash()
    {

        if (bow == null)
            bow = gameObject.transform.Find("ShootPoint"); // todo use full path for faster

        // instantiat 1 bullet

        var Slash = (GameObject)Instantiate(SlashPrefab, bow.position, bow.rotation);
        if (faceRight)
        {
            slashPower = slashForce + (speedx * moveSpeed);
        }
        else if (!faceRight)
        {
            slashPower = slashForce - (speedx * moveSpeed);
        }
        Slash.GetComponent<Rigidbody>().velocity = bow.TransformDirection(new Vector3(0, slashPower, 0));
        
        ScoreController.skillCD0 = 0.5f;
        ScoreController.skillCD2 = 0.5f;
    }

    void Trap()
    {

        Instantiate(TrapPrefab, transform.position - new Vector3(0, 0.8f, 0) , Quaternion.identity);

        ScoreController.skillCD3 = 20f;
    }
}
