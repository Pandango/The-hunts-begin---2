using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class playerHunter : NetworkBehaviour {

    private Rigidbody2D rb2d;
    public bool grounded;
    public bool faceRight,movable = true;
    bool isShoot = false;
    bool PowerShot = false;

    private playerStat playerStats;
    

	public Renderer spriteR, spriteL ;

    [SerializeField]
    bool isEnter = false;

    AudioSource SlashS,Bleed,SoulRetrieve;
    public GameObject ArrowPrefab,ArrowSuperPrefab,SlashPrefab, TrapPrefab , BloodPrefab;
	public float shootForce, shootPower ,slashForce, slashPower ,TrapForce, TrapPower;
    public KeyCode trap;
	private Animator animTopR,animTopL,animLeg;

    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    Transform bow,rotate,Leg,Collider = null;

    public float maxSpeed = 300f;
    public float jumpSpeed = 300f;
    public float fallSpeed = 10f;
    public float Speed = 2f;
    private float speedx;

	SpawnManager spawnManager;

    // Use this for initialization
    void Start () {
        playerStats = GetComponent<playerStat>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
		animLeg = gameObject.transform.Find("LEGS").GetComponent<Animator>();
		animTopR = gameObject.transform.Find("MainRotatePoint/RotatePoint/HunterTop-1").GetComponent<Animator>();
		animTopL = gameObject.transform.Find("MainRotatePoint/RotatePoint/HunterTop-2").GetComponent<Animator>();

		spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager> ();

        AudioSource[] audios = GetComponents<AudioSource>();
        SlashS = audios[0];
		Bleed = audios[1];
		SoulRetrieve = audios[2];

		spriteR.enabled = true;
		spriteL.enabled = false;

        

	}
	
	// Update is called once per frame
	void Update () {

		Jump();
        Fall();

        MoveMachineMouse();

        if ((Input.GetMouseButton(0)) && (ScoreController.skillCD0 == 0))
        {
            CmdShoot();
        }
        if ((Input.GetMouseButton(2)) && (ScoreController.skillCD1 == 0))
        {
			CmdShootSuper();
        }
        if ((Input.GetMouseButton(1)) && (ScoreController.skillCD2 == 0))
        {
            Slash();
        }
        if ((Input.GetKeyDown(trap)) && (ScoreController.skillCD3 == 0))
        {
			CmdTrap();
        }
    }

    void FixedUpdate()
    {

		if (movable) {

			speedx = Input.GetAxis ("Horizontal");

			rb2d.velocity = new Vector2 (speedx * maxSpeed, rb2d.velocity.y);

		}
		//Move();
		animLeg.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //anim.SetBool("Grounded", grounded);



        if (speedx > 0 && !faceRight)
        {
            Flip();

        }
        else if (speedx < 0 && faceRight)
        {
            Flip();
        }
    }

    /*void Move()
    {
		if (movable) {
			speedx = Input.GetAxis ("Horizontal");

			rb2d.velocity = new Vector2 (speedx * maxSpeed, rb2d.velocity.y);
			anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));
		}
    }*/

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && grounded && movable)
        {
            rb2d.AddForce(Vector2.up * jumpSpeed);
        }
    }

    void Fall()
    {
        if (!grounded)
        {
            fallSpeed += Time.deltaTime;
        }

        if (!grounded && fallSpeed >= 0.6f)
        {

            rb2d.AddForce(Vector2.up * jumpSpeed * -fallSpeed / 20);

        }
        else if (grounded)
        {
            fallSpeed = 0;
        }

    }

    void Flip()
    {
		if (faceRight) {
			spriteL.enabled = true;
			spriteR.enabled = false;

		}else if (!faceRight) {
			spriteR.enabled = true;
			spriteL.enabled = false;
		}

        faceRight = !faceRight;

		if (Leg == null)
			Leg = gameObject.transform.Find("LEGS");

		Vector3 theScale = Leg.localScale;
        theScale.x *= -1;
		Leg.localScale = theScale;


        if (Collider == null)
            Collider = gameObject.transform.Find("MainRotatePoint/playerCollider");

        Vector3 ColliderScale = Collider.localScale;
        ColliderScale.x *= -1;
		ColliderScale.y *= 1;
        Collider.localScale = ColliderScale;

        /*
		Vector3 TopScale = spritePic.localScale;
		Quaternion TopRotate = spritePic.localRotation;
		//TopScale.x *= -1;
		//TopScale.y *= -1;
		//TopRotate.z = -1;
		if (TopRotate.z > 0 && TopRotate.z < 180)
		{
			float dif = 180 - TopRotate.z;
			TopRotate.z = dif;
		}

		//TopRotate.z -= TopRotate.z;

		spritePic.localScale = TopScale;
		spritePic.localRotation = TopRotate; */

    }

    void MoveMachineMouse()
    {
		
        if (rotate == null)
			rotate = gameObject.transform.Find("MainRotatePoint");
        /*Vector2 dir = Input.mousePosition - transform.position;
        float angle = Mathf.Tan(dir.y / dir.x) * Mathf.Rad2Deg;
        
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));*/

        /*var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(mousePosition - transform.position, transform.TransformDirection(Vector3.forward));
        transform.rotation = new Quaternion(0, 0, rot.z -0.1f, rot.w - 0.1f);*/

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		rotate.rotation = Quaternion.LookRotation(Vector3.forward , (mousePos - transform.position) );


        if (mousePos.x > transform.position.x && !faceRight)
        {
            Flip();
        }
        else if (mousePos.x < transform.position.x && faceRight)
        {
			Flip();
        }

    }

	[Command]
    void CmdShoot()
    {
		if (faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint1");
				animTopR.SetTrigger ("SHOOT");
		} else if (!faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint2");
				animTopL.SetTrigger ("SHOOT");
		}
		

			// instantiat 1 bullet

		var Arrow = (GameObject)Instantiate (ArrowPrefab, bow.position, bow.rotation);

		if (faceRight)
		{
			shootPower = shootForce + (rb2d.velocity.x)/2;
		}
		else if (!faceRight)
		{
			shootPower = shootForce - (rb2d.velocity.x)/2;
		}
			Arrow.GetComponent<Rigidbody2D> ().velocity = bow.TransformDirection (new Vector3 (0, shootPower, 0));
		NetworkServer.Spawn (Arrow);
			//Bullet2.GetComponent<Rigidbody>().AddForce(new Vector3 (10,20,shootForce));
			ScoreController.skillCD0 = 1f;
			ScoreController.skillCD2 = 0.5f;
    }

	[Command]
	void CmdShootSuper()
    {

			if (faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint1");
				animTopR.SetTrigger ("SHOOT");
			}else if (!faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint2");
				animTopL.SetTrigger ("SHOOT");
			}


        // instantiat 1 bullet

        var ArrowSuper = (GameObject)Instantiate(ArrowSuperPrefab, bow.position, bow.rotation);

		if (faceRight)
		{
			shootPower = shootForce + (rb2d.velocity.x)/2;
		}
		else if (!faceRight)
		{
			shootPower = shootForce - (rb2d.velocity.x)/2;
		}
		ArrowSuper.GetComponent<Rigidbody2D>().velocity = bow.TransformDirection(new Vector3(0, shootPower*5/2, 0));
		NetworkServer.Spawn (ArrowSuper);
        //Bullet2.GetComponent<Rigidbody>().AddForce(new Vector3 (10,20,shootForce));

        ScoreController.skillCD1 = 10f;
    }

    void Slash()
    {

			if (faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint1");
				animTopR.SetTrigger ("SLASH");
			}else if (!faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint2");
				animTopL.SetTrigger ("SLASH");
			}


        // instantiat 1 bullet
		SlashS.Play();

        /*var Slash = (GameObject)Instantiate(SlashPrefab, bow.position, bow.rotation);
        if (faceRight)
        {
			slashPower = rb2d.velocity.x;
        }
        else if (!faceRight)
        {
			slashPower = -rb2d.velocity.x;
        }
		Slash.GetComponent<Rigidbody2D>().velocity = bow.TransformDirection(new Vector2(0, slashPower));*/
        
        ScoreController.skillCD0 = 0.5f;
        ScoreController.skillCD2 = 0.5f;
    }

	[Command]
	void CmdTrap()
    {
		if (faceRight) {
			bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint1");
		}else if (!faceRight) {
			bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint2");
		}


		// instantiat 1 bullet

		var BearTrap = (GameObject)Instantiate(TrapPrefab, bow.position, Quaternion.identity);

		if (faceRight)
		{
			TrapPower = TrapForce + (rb2d.velocity.x);
		}
		else if (!faceRight)
		{
			TrapPower = TrapForce - (rb2d.velocity.x);
		}
		BearTrap.GetComponent<Rigidbody2D>().velocity = bow.TransformDirection(new Vector2(0, TrapPower));
		NetworkServer.Spawn (BearTrap);

        ScoreController.skillCD3 = 20f;
    }

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if (hitInfo.gameObject.tag == "bearTrap") //If player stepped on trap.....
		{
			SlashS.Play();

			Bleed.Play();
			var BloodSpill = (GameObject)Instantiate(BloodPrefab, hitInfo.transform.position, Quaternion.identity);
			BloodSpill.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			BloodSpill.transform.localScale = new Vector3(-BloodSpill.transform.localScale.x *(Mathf.Pow( -1 ,Random.Range(1, 3))) , BloodSpill.transform.localScale.y , BloodSpill.transform.localScale.z);
			playerStats.currentHp -= 25;

			movable = false;
			GetComponent<objectInteractionController>().enabled = false;
			rb2d.velocity = new Vector2 (0, 0);
			StartCoroutine (immovable());
		}

		if (hitInfo.gameObject.tag == "Slash") { //If Player got slashed by the blade.....
            playerStats.currentHp -= 25;
            Bleed.Play();
			var BloodSpill = (GameObject)Instantiate(BloodPrefab, hitInfo.transform.position, Quaternion.identity);
			BloodSpill.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3,3), Random.Range(-3,0));

			if (hitInfo.transform.position.x < this.transform.position.x) {
				BloodSpill.transform.localScale = new Vector3(-BloodSpill.transform.localScale.x , BloodSpill.transform.localScale.y , BloodSpill.transform.localScale.z);
			}
		}
        if (hitInfo.gameObject.tag == "Soul")
        {
			SoulRetrieve.Play();
            float soul = hitInfo.GetComponent<soul>().SoulCount;
            playerStats.PlayerCurrentSoul += soul;
            
        }
        if (hitInfo.gameObject.tag == "Potion")
        {

            playerStats.currentHp += 25;

        }
        if (hitInfo.gameObject.tag == "WaterBottom")
        {

            playerStats.currentHp -= 100;

        }
    }

	IEnumerator immovable(){
		yield return new WaitForSeconds (3);
		movable = true;
		SlashS.pitch = 1.5f;
		SlashS.Play();
		GetComponent<objectInteractionController>().enabled = true;
	}

	void OnCollisionEnter2D(Collision2D hitInfo)
	{
		if ((hitInfo.gameObject.tag == "Arrow") || (hitInfo.gameObject.tag == "ArrowSuper")) //If Player was shot.....
		{
           
            Bleed.Play();
			var BloodSpill = (GameObject)Instantiate(BloodPrefab, hitInfo.transform.position, Quaternion.identity);
			BloodSpill.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3,3), Random.Range(-3,0));

			if (hitInfo.transform.position.x < this.transform.position.x) {
				BloodSpill.transform.localScale = new Vector3(-BloodSpill.transform.localScale.x , BloodSpill.transform.localScale.y , BloodSpill.transform.localScale.z);
			}
			if (hitInfo.gameObject.tag == "ArrowSuper") {
                playerStats.currentHp -= 100;
                BloodSpill.transform.localScale = new Vector3(BloodSpill.transform.localScale.x *1.5f , BloodSpill.transform.localScale.y , BloodSpill.transform.localScale.z);
			}
            if(hitInfo.gameObject.tag == "Arrow")
            {
                playerStats.currentHp -= 25;
            }
            
        }
	}
}
