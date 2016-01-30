using UnityEngine;
using System.Collections;

public class GoatController : MonoBehaviour {
	Rigidbody2D rigidBody;
	int jumpForce = 100;
	bool isGrounded = true;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	//Dynamic possible grounds.
	public LayerMask whatIsGround;
//	Animator anim;
	
	// Use this for initialization
	void Start () {
		//	rigidBody = (Rigidbody2D)GetComponent ();
		print ("Samurai Controller started");
		rigidBody = GetComponent<Rigidbody2D> ();
//		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	bool lookingRight = false;
	
	void Update () {
		//Did it hit anywhere that is considered Ground or not?
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
//		anim.SetBool("isGrounded", isGrounded);
		
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		if (horizontal > 0 && !lookingRight) {
			lookingRight = true;
			switchLook(lookingRight);
		} else if(horizontal < 0 && lookingRight) {
			lookingRight = false;
			switchLook(lookingRight);
		}
//		if(isGrounded){
		if(isGrounded && vertical > 0){
			jump();
		}
//		}
	}
	void switchLook(bool lookingRight){
		float scale = Mathf.Abs (transform.localScale.x);
		if(!lookingRight){
			scale *= -1;
		}
		transform.localScale = new Vector3 ( scale, transform.localScale.y, transform.localScale.z);
	}
	void jump(){
		rigidBody.AddForce(new Vector2(0, jumpForce));
	}
}
