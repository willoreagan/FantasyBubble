using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Button : MonoBehaviour
{

	GameObject On;
	GameObject Off;
	GameObject cam;
	System.Collections.Generic.Dictionary<string,string> parameters;
	bool WaitForPickupFriends;
	bool WaitForAksFriends;
	bool WaitForLoginToLeadboard;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find("Main Camera");
		if(name == "Sound" ){ 
			On = transform.FindChild("Background").gameObject;
			Off = transform.FindChild("Background1").gameObject;
			if(!InitScript.sound) {
				On.SetActive(false);
				Off.SetActive(true);
			}
		}
		if(name == "Music" ){ 
			On = transform.FindChild("Background").gameObject;
			Off = transform.FindChild("Background1").gameObject;
			if(!InitScript.music) {
				On.SetActive(false);
				Off.SetActive(true);
			}
		}
		if(InitScript.InfinityLife){
			if(transform.parent.name == "LifePanel"){
				transform.parent.gameObject.SetActive(false);
			}
		}
		if(name == "Changing"){
			if(!InitScript.Changing){
				transform.FindChild("Background").gameObject.SetActive(true);
				transform.FindChild("Background1").gameObject.SetActive(false);
				transform.Find("Price").GetComponent<Text>().text = "8";
                if (PlayerPrefs.GetInt("Changing") == 0) transform.Find("Price").GetComponent<Text>().text = "1";
				transform.Find("Price").gameObject.SetActive(true);
			}
			else{
				transform.FindChild("Background").gameObject.SetActive(false);
				transform.FindChild("Background1").gameObject.SetActive(true);
				transform.Find("Price").gameObject.SetActive(false);
			}
		}
		if(name == "Cherry"){
			if(!InitScript.Cherry){
				transform.FindChild("Background").gameObject.SetActive(true);
				transform.FindChild("Background1").gameObject.SetActive(false);
                transform.Find("Price").GetComponent<Text>().text = "5";
                if (PlayerPrefs.GetInt("Cherry") == 0) transform.Find("Price").GetComponent<Text>().text = "1";
				transform.Find("Price").gameObject.SetActive(true);
			}
			else{
				transform.FindChild("Background").gameObject.SetActive(false);
				transform.FindChild("Background1").gameObject.SetActive(true);
				transform.Find("Price").gameObject.SetActive(false);
			}
		}
		if(name == "Electric"){
			if(!InitScript.Electric){
				transform.FindChild("Background").gameObject.SetActive(true);
				transform.FindChild("Background1").gameObject.SetActive(false);
				transform.Find("Price").gameObject.SetActive(true);
                transform.Find("Price").GetComponent<Text>().text = "8";
                if (PlayerPrefs.GetInt("Electric") == 0) transform.Find("Price").GetComponent<Text>().text = "1";
			}
			else{
				transform.FindChild("Background").gameObject.SetActive(false);
				transform.FindChild("Background1").gameObject.SetActive(true);
				transform.Find("Price").gameObject.SetActive(false);
			}
		}
	}

	void returnControl(){
		mainscript.StopControl = false;
		GamePlay.ControlAllowed = true;
	}

	void offBlock(){
		InitScript.ShowedHardAd = true;
	}
	
	public void OnPress ( bool isDown){
		if(!isDown){

			if(!InitScript.ShowedHardAd && transform.parent.name == "GameOver" && (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform== RuntimePlatform.WP8Player)){
				Invoke("offBlock",3);
				return;
			}


			SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);

			if(name == "PauseButton"){
				if(GamePlay.gameStatus != GameState.Pause && GamePlay.gameStatus != GameState.GameOver ){
					GamePlay.ControlAllowed = false;
					mainscript.StopControl = true;
					GamePlay.gameStatus = GameState.Pause;
				}
				else if(GamePlay.gameStatus == GameState.Pause){
					//BalloonControl.Instance.ChangeSkin(PlayerPrefs.GetInt("ActiveSkin"));
					GamePlay.gameStatus = GameState.Playing;
					mainscript.Instance.CheckBoosts();
					Invoke("returnControl", 0.5f);
				}
			}
			if(name == "Play"){
				Application.LoadLevel("game");
			}
            if (name == "TutorialButton")
            {
                GameObject.Find("Camera").transform.Find("Tutorial").gameObject.SetActive(true);
            }

			if(name == "PlayTutorial"){
			
				Application.LoadLevel("tut1");
			}
			if(name == "Skip"){
				Application.LoadLevel("game");
			}
			if(name == "Settings"){
				transform.parent.parent.Find("Options").gameObject.SetActive(true);
			}
			if(name == "AddLife"){
				GameObject.Find("Camera").transform.Find("Refill").gameObject.SetActive(true);
			}
			if(name == "AddGem"){
				GameObject.Find("Camera").transform.Find("GemsShop").gameObject.SetActive(true);
			}
			if(name == "Changing"){
				if(!InitScript.Changing){
                    if (InitScript.Gems >= int.Parse(transform.Find("Price").GetComponent<Text>().text))
                    {
						InitScript.Changing = true;
                        InitScript.Inctance.SpendGems(int.Parse(transform.Find("Price").GetComponent<Text>().text));
						SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.Cash);
                        InitScript.Changing = true;
                        mainscript.Instance.CheckBoosts();
                        transform.FindChild("Background").gameObject.SetActive(false);
                        transform.FindChild("Background1").gameObject.SetActive(true);
                        transform.Find("Price").gameObject.SetActive(false);
                        //PlayerPrefs.SetInt("Changing", 1);
                        //PlayerPrefs.Save();
					}
					else{
						GameObject.Find("Camera").transform.Find("GemsShop").gameObject.SetActive(true);
					}
				}
			}
			if(name == "Cherry"){
				if(!InitScript.Cherry){
                    if (InitScript.Gems >= int.Parse(transform.Find("Price").GetComponent<Text>().text))
                    {
						InitScript.Cherry = true;
                        InitScript.Inctance.SpendGems(int.Parse(transform.Find("Price").GetComponent<Text>().text));
						SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.Cash);
                        InitScript.Cherry = true;
                        mainscript.Instance.CheckBoosts();
                        transform.FindChild("Background").gameObject.SetActive(false);
                        transform.FindChild("Background1").gameObject.SetActive(true);
                        transform.Find("Price").gameObject.SetActive(false);
                        //PlayerPrefs.SetInt("Cherry", 1);
                        //PlayerPrefs.Save();
					}
					else{
						GameObject.Find("Camera").transform.Find("GemsShop").gameObject.SetActive(true);
					}
				}
			}
			if(name == "Electric"){
				if(!InitScript.Electric){
                    if (InitScript.Gems >= int.Parse(transform.Find("Price").GetComponent<Text>().text))
                    {
						InitScript.Electric = true;
                        InitScript.Inctance.SpendGems(int.Parse(transform.Find("Price").GetComponent<Text>().text));
						SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.Cash);
                        InitScript.Electric = true;
                        mainscript.Instance.CheckBoosts();

                        transform.FindChild("Background").gameObject.SetActive(false);
                        transform.FindChild("Background1").gameObject.SetActive(true);
                        transform.Find("Price").gameObject.SetActive(false);
                        //PlayerPrefs.SetInt("Electric", 1);
                        //PlayerPrefs.Save();
					}
					else{
						GameObject.Find("Camera").transform.Find("GemsShop").gameObject.SetActive(true);
					}
				}
			}
			if(name == "Shop"){
				GameObject.Find("Camera").transform.Find("Shop").gameObject.SetActive(true);
			}
			if(name == "Close"){
				transform.parent.gameObject.SetActive(false);
			}
			if(name == "Back"){
				transform.parent.parent.gameObject.SetActive(false);
			}
			if(name == "Later"){
				transform.parent.gameObject.SetActive(false);
			}
			if(name == "Rate"){
				PlayerPrefs.SetInt("Rated",1);
				PlayerPrefs.Save();
				transform.parent.gameObject.SetActive(false);
				Application.OpenURL("market://details?id=com.gaulsgames.woodlandbubble");
			}
			if(name == "Never"){
				PlayerPrefs.SetInt("Rated",1);
				PlayerPrefs.Save();
				transform.parent.gameObject.SetActive(false);
			}
			if(name == "Download"){
				Application.OpenURL("market://details?id=com.gaulsgames.woodlandbubble");
				Application.Quit();
			}
			
			if(name == "MoreGames"){
				Application.OpenURL("market://search?q=pub:GAUL%60S+GAMES");
			}
			if(name == "HowToPlay"){
				GameObject.Find("Camera").transform.Find("HelpInfo").gameObject.SetActive(true);
			}
			if(name == "BoostInfo"){
				GameObject.Find("Camera").transform.Find("BoostInfo").gameObject.SetActive(true);
			}
			if(name == "StartGame"){
				InitScript.Arcade = false;
                Application.LoadLevel("game");
				//GameObject.Find("Camera").transform.Find("MenuPlay").gameObject.SetActive(true);
			}
			if(name == "StartArcadeGame"){
				InitScript.Arcade = true;
                Application.LoadLevel("game");
			//	GameObject.Find("Camera").transform.Find("MenuPlay").gameObject.SetActive(true);
			}
			if(name == "Menu"){
				GamePlay.gameStatus = GameState.WaitForPopup;
				Application.LoadLevel("menu");
			}
			if(name == "BuyInfinityLife"){
                if (InitScript.Gems >= int.Parse(transform.Find("Count").GetComponent<Text>().text))
                {
					SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.Cash);
                    InitScript.Inctance.SpendGems(int.Parse(transform.Find("Count").GetComponent<Text>().text));
					InitScript.InfinityLife = true; 
					PlayerPrefs.SetInt("InfinityLife", 1); 
					PlayerPrefs.Save();
					gameObject.SetActive(false);
					transform.parent.transform.Find("LifePanel").gameObject.SetActive(false);
					GameObject.Find("Camera").transform.Find("LifePanel").gameObject.SetActive(false);
				}
				else
					GameObject.Find("Camera").transform.Find("GemsShop").gameObject.SetActive(true);

			}
			if(name == "Again"){
                print("again");
				Application.LoadLevel("game");
			}
			if(name == "Exit"){
				Application.Quit();
			}
			if(name == "Sound" || name == "Music"){
				if(On.activeSelf){
					if(name == "Sound") InitScript.Inctance.Sound(false);
					if(name == "Music") InitScript.Inctance.Music(false);

					On.SetActive(false);
					Off.SetActive(true);
				}
				else{
					if(name == "Sound") InitScript.Inctance.Sound(true);
					if(name == "Music") InitScript.Inctance.Music(true);

					On.SetActive(true);
					Off.SetActive(false);
				}
			}

			if(name == "Boost_change"){
				mainscript.Instance.ChangeBoost();

			}
		}
	}


}
