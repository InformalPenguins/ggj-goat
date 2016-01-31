using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour {
	public GameObject[] slides;
	private Vector3[] positions;
	private GameObject current;
	private Vector3 currentV3;
	private int currentIdx = 0;
	public float SlideSpeed = 0.5f;
	bool seenIntro = false;
	// Use this for initialization
	void Start () {
		positions = new Vector3[]{
			new Vector3(-8, 1, 0),
			new Vector3(0, -1, 0),
			new Vector3(8, 1, 0),
			new Vector3(0, 0, 0)
		};
		refreshSlide ();
		seenIntro = PlayerPrefs.GetInt("SeenIntro", 0) > 0;
		//Invoke("refreshSlide", 1);
	}
	private int refreshSlide(){
		if (currentIdx < slides.Length) {
			current = slides [currentIdx];
			currentV3 = positions [currentIdx];
		} else {
			StartCoroutine(WaitNextScene());
		}
		return currentIdx;
	}
	private void nextScene(){
		PlayerPrefs.SetInt ("SeenIntro", 1);
		Application.LoadLevel("Level1");
	}
	bool pending = false;
	private void Update () {
		current.transform.position = Vector3.MoveTowards(current.transform.position, currentV3, SlideSpeed);
		if(!pending && current.transform.position.x == currentV3.x){
			pending = true;
			currentIdx++;
			StartCoroutine(WaitFunction());
		}
		if (Input.GetMouseButtonDown (0) && seenIntro) {
			nextScene();
		}
	}
	IEnumerator WaitFunction()
	{
		yield return new WaitForSeconds(2f);
		refreshSlide ();
		pending = false;
	}
	IEnumerator WaitNextScene() {
		yield return new WaitForSeconds(2f);
		nextScene();
	}
}
