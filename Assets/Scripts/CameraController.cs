using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	[SerializeField]
	private Transform target;
	[SerializeField]
	private Camera camera;
	private Vector2 velocity = Vector2.zero;
	private float dampTime = 0.3f;
	// Use this for initialization
	void Start () {
		camera = this.GetComponent<Camera> ();
		target = this.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(target) {
			Vector3 point = camera.WorldToViewportPoint(target.position);
//			//Vector2 delta = target.position - camera.ViewportToWorldPoint(Vector2(0.5, 0.5));
//			Vector2 destination = transform.position + delta;
//			transform.position = point //Vector2.SmoothDamp(transform.position, destination, velocity, dampTime);
			//camera.transform.position = point;
			print(point.ToString() + " --- " + camera.transform.position.ToString());
		}
		//print( "  TARGET --- " + target.ToString());
	}
}
