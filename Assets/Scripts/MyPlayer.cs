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
        Debug.Log(myRigidbody.velocity.y);

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
//        if (myRigidbody.velocity.y <= 0)
//        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
//        }
        return false;
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
}
