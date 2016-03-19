using UnityEngine;

public class MyPlayer : Character
{

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private int coins = 0;

    [SerializeField]
    private bool flagTaken;

    private bool isGrounded;

    private bool isJump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    private BoxCollider2D boxCollider2D;

    public Rigidbody2D MyRigidbody { get; set; }

    private Vector3 initialPosition;

	public bool isDying;

	SpriteRenderer spriteRenderer;

	GameObject child;

	SpriteRenderer deathSpriteRenderer;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        coins = GetCoinScore();
        initialPosition = transform.position;
		spriteRenderer = GetComponent<SpriteRenderer>();
		child = transform.GetChild (0).gameObject;
		deathSpriteRenderer = child.GetComponent<SpriteRenderer> ();
		deathSpriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();
        MyAnimator.SetBool("isGrounded", isGrounded);

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleLayers();

        ResetValues();

        checkVerticalPosition();

		boxCollider2D.enabled = !isDying;

    }

    private void checkVerticalPosition()
    {
        if (transform.position.y < -20f)
        {
            die();
        }
    }

    private void die()
    {
		deathSpriteRenderer.enabled = true;
		spriteRenderer.enabled = false;
		if (!isDying) {
			MyAnimator.SetBool ("isDying", isDying);
			Invoke ("reallyDie", 0.5);
			isDying = true;
		}
    }

	private void reallyDie() {
		transform.position = initialPosition;
		isDying = false;
		spriteRenderer.enabled = true;
		deathSpriteRenderer.enabled = false;
		HudLivesManager.lives -= 1;
		PlayerPrefs.SetInt("Lives", HudLivesManager.lives);
		//lives -- 
		// if lives == 0 gameover
		
	}

    private void HandleMovement(float horizontal)
    {
        if (horizontal != 0 && (isGrounded || Mathf.Abs(MyRigidbody.velocity.y) > 1))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }

        if (isGrounded && isJump)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
            MyAnimator.SetTrigger("jump");
        }

        if (MyRigidbody.velocity.y < 0 && !isGrounded)
        {
            MyAnimator.SetBool("land", true);
        }
        else {
            MyAnimator.SetBool("land", false);
        }

        MyAnimator.SetFloat("velocityY", MyRigidbody.velocity.y);
        MyAnimator.SetFloat("velocityX", MyRigidbody.velocity.x);
        MyAnimator.SetBool("isJump", isJump);
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && facingLeft || horizontal < 0 && !facingLeft)
        {
            ChangeDirection();
        }
    }

    private void ResetValues()
    {
        isJump = false;
    }

    private bool IsGrounded()
    {
        bool grounded = false;
        //Note: If walking over a diagonal floor, y velocity is < = and you cannot jump.
        foreach (Transform point in groundPoints)
        {
            grounded = Physics2D.OverlapCircle(point.position, groundRadius, whatIsGround);
            if (grounded)
            {
                break;
            }
        }
        return grounded;
    }

    private void HandleLayers()
    {
        if (isGrounded)
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject o = other.gameObject;
		if (o.CompareTag ("Pickups")) {
			//			Vector2 otherCenter = new Vector2(o.transform.position.x, o.transform.position.y);
			//			if(boxCollider2D.OverlapPoint(otherCenter)){
			other.gameObject.SetActive (false);
			coins++;
			SaveCoinScore (coins, false);
			//			}
			//			print ("boxCollider2D : " + boxCollider2D.transform.position.ToString());
			//			print ("otherCenter : " + otherCenter.ToString());
		} else if (o.CompareTag ("Flag")) {
			//Win condition
			SaveCoinScore (coins, true);
			loadNextScene ();
		} else if (o.CompareTag ("Enemy")) {
			FarmerScript farmerScript = o.GetComponent<FarmerScript>();
			if (farmerScript) {
				if (farmerScript.isAttacking) {
					die ();
					farmerScript.isAttacking = false;
				} else {
					farmerScript.isAttacking = true;
				}
			} else {
				die ();
			}
		}
    }

    public void loadNextScene()
    {
        //float fadeTime = GameObject.Find ("scene2Choose").GetComponent<fading>().BeginFade(1);
        //yield return new WaitForSeconds(fadeTime);
        int currentLv = PlayerPrefs.GetInt("Current", 1);
        currentLv++;

        if (currentLv > 4)
        { // Update if adding more levels.
            currentLv = 1;
        }
        PlayerPrefs.SetInt("Current", currentLv);
        Application.LoadLevel("Level" + currentLv);
    }

    private void SaveCoinScore(int coins, bool toPrefs)
    {
		HudScoreManager.score = coins;
		if (toPrefs) {
			PlayerPrefs.SetInt ("Score", coins);
		}
    }

    int GetCoinScore()
    {
        return PlayerPrefs.GetInt("Score", 0);
    }

}
