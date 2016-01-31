using UnityEngine;
using System.Collections;

public class MyPlayer : Character
{
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

    public Rigidbody2D MyRigidbody { get; set; }

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
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
		MyAnimator.SetBool("isGrounded", isGrounded);

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleLayers();

        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
        if (MyRigidbody.velocity.y < 0 && !isGrounded)
        {
			MyAnimator.SetBool("land", true);
        }

		if (horizontal != 0)
		{
		    MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }

        if (isGrounded && isJump)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
            MyAnimator.SetTrigger("jump");
        }
		
		MyAnimator.SetFloat("velocityY", MyRigidbody.velocity.y);
		MyAnimator.SetFloat("velocityX", MyRigidbody.velocity.x);
		MyAnimator.SetBool("isJump", isJump);
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
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
            MyAnimator.SetLayerWeight(1, 0);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
    }
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Flag"))
		{
			//Win condition
			print ("Flag picked");
			gameScene2();
		} else if (other.gameObject.CompareTag ("Pickups"))
		{
			other.gameObject.SetActive (false);
			coins++;
		}
	}

	public void gameScene2() {
		//float fadeTime = GameObject.Find ("scene2Choose").GetComponent<fading>().BeginFade(1);
		//yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(nextScene);
	}
}
