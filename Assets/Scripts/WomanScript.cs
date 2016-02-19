using UnityEngine;
using System.Collections;

public class WomanScript : EnemyScript {

	void Start() {
		WomanBehaviour();
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D> ().velocity = movement;
	}

	void WomanBehaviour() {
		Invoke("ChangeDirection", 3);
	}

	public void ChangeDirection()
	{
		direction.y = direction.y * -1;
		WomanBehaviour ();
	}
}
