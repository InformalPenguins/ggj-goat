using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudLivesManager : MonoBehaviour {
	Text text;
	public static int lives = 3;
	// Use this for initialization
	void Start () {
		text = GetComponent <Text> ();
		int lives = PlayerPrefs.GetInt("Lives", 3);
		HudLivesManager.lives = lives;
	}
	public static void reset(){
		PlayerPrefs.SetInt("Lives", 3);
		HudLivesManager.lives = 3;
	}
	// Update is called once per frame
	void Update () {
		text.text = "LIVES: " + HudLivesManager.lives;
		if (HudLivesManager.lives <= 0) {
			//GameOver
			Application.LoadLevel("GameOver");
		}
	}
}
