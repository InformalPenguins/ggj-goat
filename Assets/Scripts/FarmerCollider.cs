using UnityEngine;
using System.Collections;

public class FarmerCollider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject o = other.gameObject;
		if (o.CompareTag ("Player")) {
			print("coin");
		} 
	}
}
