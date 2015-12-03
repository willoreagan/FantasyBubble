using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CustomLobbyManager : NetworkLobbyManager
{
	public CustomLobbyManager():base()
    { 
        
        if (singleton != null)
            Destroy(singleton);

        
    }
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*
    public override void OnStopClient()
    {
        //Destroy(NetworkManager.singleton);
      //  NetworkManager.singleton.
        base.OnStopClient();
        Destroy(GameObject.FindObjectOfType<NetworkLobbyManager>());
    }

    public override void OnStopHost()
    {
        base.OnStopHost();
        Destroy(GameObject.FindObjectOfType<NetworkLobbyManager>());
    }
    */
}
