using UnityEngine;
using System.Collections;

public class ColorBallScript : MonoBehaviour {
	public Sprite[] sprites;

	// Use this for initialization
	void Start () {

	}

	public void SetColor(int color){
		GetComponent<SpriteRenderer>().sprite = sprites[color];
	}

	// Update is called once per frame
	void Update () {
	
	}
}
