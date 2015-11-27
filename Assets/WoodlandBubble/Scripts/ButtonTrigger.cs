using UnityEngine;
using System.Collections;

public class ButtonTrigger : MonoBehaviour {

	public bool fireButtonDown;
	public static ButtonTrigger instance = null;
	// Use this for initialization
	void Start () {
		instance = this;
	}

	public void OnFireButtonDown()
	{
		//Debug.Log("OnFireButttonDown");
		fireButtonDown = true;
		
		
		
		//fireButtonDown = false;
	}
	
	public void OnFireButtonUp()
	{
		//Debug.Log("OnFireButtonUp");
		fireButtonDown = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
