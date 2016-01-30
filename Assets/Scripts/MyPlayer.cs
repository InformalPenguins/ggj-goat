using UnityEngine;
using System.Collections;

public class MyPlayer : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    private Animator myAnimator;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
	private float groundRadius;

	[SerializeField]
	int coins = 0;

	[SerializeField]
	bool flagTaken;

	[SerializeField]
	private string nextScene;

    private bool facingLeft;

    private bool isGrounded;

    private bool isJump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

	// Use this for initialization
	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
		facingLeft = true;
	}

    // Update is called once per frame
    void Update()
    {
        HandleInput();
		FixedUpdate ();
    }

	void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");

		isGrounded = IsGrounded();
		myAnimator.SetBool("isGrounded", isGrounded);

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleLayers();

        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
//        Debug.Log(myRigidbody.velocity.y);

        if (myRigidbody.velocity.y < 0 && !isGrounded)
        {
			myAnimator.SetBool("land", true);
        }

		if (horizontal != 0)
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
            facingLeft = !facingLeft;
            Vector3 myScale = transform.localScale;
            myScale.x *= -1;
            transform.localScale = myScale;
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
            myAnimator.SetLayerWeight(1, 0);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 1);
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
