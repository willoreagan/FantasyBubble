using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {
	Text font;
	// Use this for initialization
	void Start () {
        font = GetComponent<Text>();
		if(!InitScript.Arcade)
			font.text = PlayerPrefs.GetFloat("highscore").ToString();
		else
			font.text = PlayerPrefs.GetFloat("highscoreArcade").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if(font.text == ""){
			if(!InitScript.Arcade)
				font.text = PlayerPrefs.GetFloat("highscore").ToString();
			else
				font.text = PlayerPrefs.GetFloat("highscoreArcade").ToString();
		}

	}
}
