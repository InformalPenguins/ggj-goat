using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudScoreManager : MonoBehaviour {
	Text text;
	public static int score = 0;
	// Use this for initialization
	void Start () {
		text = GetComponent <Text> ();
		int score = PlayerPrefs.GetInt("Score", 0);
		HudScoreManager.score = score;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "SCORE: " + HudScoreManager.score;
		//print ("SCORE: " + score);
	}
}
