using System;
using UnityEngine;
using System.Collections;

public class CoinCollector : MonoBehaviour
{
	//[SerializeField]
    public Transform Target;
	public float CoinSpeed = 1f;
	ArrayList coins = new ArrayList();
    private void Update()
    {
		transform.position = Target.position;
		foreach(GameObject coin in coins){
			coin.transform.position = Vector3.MoveTowards(coin.transform.position, Target.position, CoinSpeed);
		}
	}
	private void OnTriggerEnter2D(Collider2D other) {
		GameObject o = other.gameObject;
		if (o.CompareTag ("Pickups")) {
			coins.Add(o);
		}
	}
}
