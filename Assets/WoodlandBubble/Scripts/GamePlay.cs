using UnityEngine;
using System.Collections;

public enum GameState
{
	Playing,
	Highscore,
	GameOver,
	Pause,
	WaitForPopup,
	BlockedGame
}

public class GamePlay : MonoBehaviour {

	public static GamePlay Instance;
	bool gameOverShow;
	public static GameState gameStatus;
	public GameObject gameOverMenu;
	public GameObject highScorePopup;
	public GameObject backHide;
	public GameObject pauseText;
	public GameObject MusicObject;
	GameObject pauseButton;
	GameObject flash;
	GameObject stage;
	bool highScoreShow;
	static int vipeCounter;
	public static bool ControlAllowed;
	//public float[] NextStageDistance = new float[];
//	[HideInInspector]
	public float NextStageDistance = 1000;

	static float AdTimer;

	void Awake(){
		//		Application.targetFrameRate = 60;
		ControlAllowed = true;
		vipeCounter++;
//		PlayerPrefs.SetFloat("highscore", 0);
//		PlayerPrefs.Save();
		if(PlayerPrefs.GetInt("Hat") == 1)
			InitScript.Hat = true;


		if(InitScript.InfinityLife){
			GameObject life = GameObject.Find("LifePanel");
			if(life != null) life.SetActive(false);
		}
		if(InitScript.Inctance == null) gameObject.AddComponent<InitScript>();
        InitScript.Electric = false;
        InitScript.Changing = false;
        InitScript.Cherry = false;

		GameObject soundBase = GameObject.Find("SoundBase");
		if(soundBase == null) soundBase = Instantiate(Resources.Load("SoundBase")) as GameObject;
		GameObject gameMusic = GameObject.Find("Music");
		if(gameMusic == null) gameMusic = Instantiate(Resources.Load("Music")) as GameObject;
		gameMusic.name = "Music";
		if(Application.loadedLevelName == "game")
			DontDestroyOnLoad(gameMusic);
		if(!gameMusic.GetComponent<AudioSource>().isPlaying) gameMusic.GetComponent<AudioSource>().Play();
        if (AdmobIntegration.THIS != null)
        {
            if (AdmobIntegration.THIS.showBanner)
                StartCoroutine(InitScript.Inctance.ShowAdsBanner());
        }
	}
	// Use this for initialization
	void Start () {
		if(Application.loadedLevelName == "game"){
			PlayerPrefs.SetInt("tutPassed", 1);
			PlayerPrefs.Save();
		}




		Instance = this;
		flash = GameObject.Find("flash");
		pauseButton = GameObject.Find("PauseButton");
        gameStatus = GameState.Playing;
        //if(gameStatus != GameState.BlockedGame) 
        //    gameStatus = GameState.WaitForPopup;
	}


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L)) gameStatus = GameState.GameOver;
		if(gameStatus == GameState.BlockedGame) return;
		if(gameStatus == GameState.Pause){ Time.timeScale = 0; ControlAllowed = false;}
		else {Time.timeScale = 1;  }

		if(gameStatus == GameState.Playing && pauseText.activeSelf)
			pauseText.SetActive(false);
		if(gameStatus == GameState.Pause && !pauseText.activeSelf)
			pauseText.SetActive(true);
		if(gameStatus == GameState.Highscore && !highScoreShow){
			highScoreShow = true;
		//	highScorePopup.SetActive(true);
            gameStatus = GameState.GameOver;
		//	backHide.SetActive(true);
		}
		if(gameStatus == GameState.GameOver && !gameOverShow){
			SoundBase.Instance.gameObject.SetActive(false);
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                //if (!InitScript.ShowedHardAd)
                //{
                if (AdmobIntegration.THIS != null)
                {
                    if( AdmobIntegration.THIS.showInterstitial)
                        StartCoroutine(InitScript.Inctance.ShowAdsAdmob());
                }
                //}
                //else if (vipeCounter % 1 == 0 && !InitScript.WasInApp)
                //{
                //    StartCoroutine(InitScript.Inctance.ShowAdsAdmob());
                //}
                //else if (vipeCounter % 2 == 0 && !InitScript.WasInApp)
                //{
                //    StartCoroutine(InitScript.Inctance.ShowAds("GameOver"));
                //}
            }

			gameOverShow = true;
			gameOverMenu.SetActive(true);
		//	pauseButton.SetActive(false);
			//we check to see if flash is there, then we send him a message that its a game over.
			if(flash != null){
				//here we send the message
				flash.SendMessage("gameOverFlash", SendMessageOptions.DontRequireReceiver);
				
			}
		}
	}



	public void ShowGameOver(){
		mainscript.Instance.CheckBoosts();
		string high = "highscore";
		if(InitScript.Arcade) high = "highscoreArcade";
		if(Score.score > PlayerPrefs.GetFloat(high)){
			PlayerPrefs.SetFloat(high, Score.score);
			PlayerPrefs.Save();
			GamePlay.gameStatus = GameState.Highscore;
			InitScript.Inctance.UpdateScores();


		}
		else{
			GamePlay.gameStatus = GameState.GameOver;
			mainscript.Instance.CheckBoosts();
		}
		
	}


}
