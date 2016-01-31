using UnityEngine;
using System.Collections;

public class MyPlayer : Character
{
    private Rigidbody2D myRigidbody;
    
    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
	private float groundRadius;

    [SerializeField]
    int coins = 0;

    [SerializeField]
    bool flagTaken;

    [SerializeField]
    private string nextScene;

    private bool isGrounded;

    private bool isJump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

	private BoxCollider2D boxCollider2D;

	// Use this for initialization
	void Start ()
    {
		myRigidbody = GetComponent<Rigidbody2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
		facingLeft = true;
		coins = GetCoinScore ();
//    public override void Start ()
//    {
        base.Start();
        myRigidbody = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

	void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");

		isGrounded = IsGrounded();
		myAnimator.SetBool("isGrounded", isGrounded);
		//myAnimator.SetBool("land", !isGrounded);

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleLayers();

        ResetValues();
		print ("Coins: " + coins);
    }

    private void HandleMovement(float horizontal)
    {
        if (myRigidbody.velocity.y < 0 && !isGrounded)
        {
			myAnimator.SetBool("land", true);
		} else if (!isGrounded){
			myAnimator.SetBool("land", false);
		}

		if (horizontal != 0 && (isGrounded || myRigidbody.velocity.y != 0))
		{
		    myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }

        if (isGrounded && isJump)
        {
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
        }
		
		myAnimator.SetFloat("velocityY", myRigidbody.velocity.y);
		myAnimator.SetFloat("velocityX", myRigidbody.velocity.x);
		myAnimator.SetBool("isJump", isJump);
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && facingLeft || horizontal < 0 && !facingLeft)
        {
            changeDirection();
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
//        if (myRigidbody.velocity.y <= 0)
//        {
            foreach (Transform point in groundPoints)
            {
				grounded = Physics2D.OverlapCircle(point.position, groundRadius, whatIsGround);
				if(grounded){
					break;
				}
//				print ("Grounded " + grounded.ToString());
//                for (int i = 0; i < colliders.Length; i++)
//                {
//                    if (colliders[i].gameObject != gameObject)
//                    {
//                        myAnimator.ResetTrigger("jump");
//						myAnimator.SetBool("land", false);
//                        return true;
//                    }
//                }
            }
		//        }
//		print ("whatIsGround: " + whatIsGround.ToString());
		return grounded;
    }

    private void HandleLayers()
    {
        if (isGrounded)
        {
            myAnimator.SetLayerWeight(1, 0);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 1);
        }
    }
	void OnTriggerEnter2D(Collider2D other) {
		GameObject o = other.gameObject;
		if (o.CompareTag ("Pickups")) {
			//			Vector2 otherCenter = new Vector2(o.transform.position.x, o.transform.position.y);
			//			if(boxCollider2D.OverlapPoint(otherCenter)){
			other.gameObject.SetActive (false);
			coins++;
			//			}
			//			print ("boxCollider2D : " + boxCollider2D.transform.position.ToString());
			//			print ("otherCenter : " + otherCenter.ToString());
		} else if (o.CompareTag ("Flag")) {
			//Win condition
			print ("Flag picked");
			SaveCoinScore(coins);
			loadNextScene();
		}
	}

	public void loadNextScene() {
		//float fadeTime = GameObject.Find ("scene2Choose").GetComponent<fading>().BeginFade(1);
		//yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(nextScene);
	}
	private void SaveCoinScore(int coins) {
		PlayerPrefs.SetInt("Score", coins);
	}
	int GetCoinScore() {
		return PlayerPrefs.GetInt("Score", 0);
	}
}
