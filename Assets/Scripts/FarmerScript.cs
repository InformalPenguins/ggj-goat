using UnityEngine;
using System.Collections;

public class FarmerScript : EnemyScript {
	Animator anim;
	public bool isAttacking = false;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (isAttacking) {
			anim.SetBool ("isAttack", true);
			movement = new Vector2 (0,0);
		} else {
			anim.SetBool ("isAttack", false);
		}
		movement = new Vector2 (speed.x * direction.x,speed.y * direction.y);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject o = other.gameObject;
		if (o.CompareTag ("Edge")) {
			direction.x = direction.x * -1;
			ChangeDirection ();
		} else if (o.CompareTag ("Player")) {
			isAttacking = true;
		}

	}

	void StopAttack () {
		isAttacking = false;
	}

}
