using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public float speed = 18f;
	public GameObject Balloon;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Balloon != null ){
			if(Camera.main.WorldToScreenPoint(Balloon.transform.localPosition).x >= Screen.width/1.5f){
				speed =2.5f;
			}
			else{
				speed = 2f;
			}
			if(speed > 4f){
				speed = 4f;
			}
			else if(speed < 1){
				speed = 1;
			}
		}
		if( GamePlay.gameStatus != GameState.WaitForPopup)
			transform.Translate(speed*Time.deltaTime,0,0);

	}

	public void receiveSpeed( float theSpeed){
		speed = theSpeed;
	}
}
