using UnityEngine;
using System.Collections;

public class FarmerScript : EnemyScript {

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject o = other.gameObject;
		if (o.CompareTag("Edge"))
		{
			direction.x = direction.x * -1;
			ChangeDirection ();
		}

	}
}
