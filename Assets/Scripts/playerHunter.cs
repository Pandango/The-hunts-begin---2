﻿using UnityEngine;
using System.Collections;

public class playerHunter : MonoBehaviour {

    private CharacterController characterContoller;
    public bool isGrounded;
    bool faceRight = true;
    bool isShoot = false;
    bool PowerShot = false;

	public Renderer spriteR, spriteL ;

	float ab=1f;

    [SerializeField]
    bool isEnter = false;

    AudioSource SlashS;
    public GameObject ArrowPrefab,ArrowSuperPrefab,SlashPrefab, TrapPrefab;
    public float shootForce,slashForce, slashPower;
    public KeyCode trap;


	Transform bow,rotate,Leg = null;

    public float gravity = 15.5f;
    private float fallSpeed;
    public float jumpSpeed = 5;
    public float moveSpeed = 5;
    private float speedx;
    
    // Use this for initialization
    void Start () {
        characterContoller = GetComponent<CharacterController>();
        AudioSource[] audios = GetComponents<AudioSource>();
        SlashS = audios[0];

		spriteR.enabled = true;
		spriteL.enabled = false;


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
        	SlashS.Play();
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
        isGrounded = (Physics.Raycast(transform.position, -transform.up, characterContoller.height / 1.8f));
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
		rotate.rotation = Quaternion.LookRotation(Vector3.forward , (mousePos - transform.position)*ab );


        if (mousePos.x > transform.position.x && !faceRight)
        {
			ab = 1f;
            Flip();
        }
        else if (mousePos.x < transform.position.x && faceRight)
        {
			ab = 0.5f;
			Flip();
        }

    }

        void Shoot()
    	{
			if (faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint1");
			} else if (!faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint2");
			}
		

			// instantiat 1 bullet

			var Arrow = (GameObject)Instantiate (ArrowPrefab, bow.position, bow.rotation);
			Arrow.GetComponent<Rigidbody> ().velocity = bow.TransformDirection (new Vector3 (0, shootForce, 0));
			//Bullet2.GetComponent<Rigidbody>().AddForce(new Vector3 (10,20,shootForce));
			ScoreController.skillCD0 = 1f;
			ScoreController.skillCD2 = 0.5f;
    }

    void ShootSuper()
    {

			if (faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint1");
			}else if (!faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint2");
			}


        // instantiat 1 bullet

        var ArrowSuper = (GameObject)Instantiate(ArrowSuperPrefab, bow.position, bow.rotation);
        ArrowSuper.GetComponent<Rigidbody>().velocity = bow.TransformDirection(new Vector3(0,		 shootForce*5/2, 0));
        //Bullet2.GetComponent<Rigidbody>().AddForce(new Vector3 (10,20,shootForce));

        ScoreController.skillCD1 = 10f;
    }

    void Slash()
    {

			if (faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint1");
			}else if (!faceRight) {
				bow = gameObject.transform.Find ("MainRotatePoint/RotatePoint/ShootPoint2");
			}


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
