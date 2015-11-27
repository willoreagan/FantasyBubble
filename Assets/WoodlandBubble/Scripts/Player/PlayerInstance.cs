using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityStandardAssets.Network;

public class PlayerInstance : NetworkBehaviour
{
    [HideInInspector]
    public GameObject debugMarker;
    public static PlayerInstance LocalPlayer;
    public static List<PlayerInstance> _otherPlayers = new List<PlayerInstance>();

	void Start ()
    {
        if (isLocalPlayer)
        {
            LocalPlayer = this;

        }
        else
        {
            _otherPlayers.Add(this);
        }
        debugMarker = GameObject.Find("debug marker");
	}

    void OnDestroy()
    {
        Debug.Log("Removing me from _otherPlayers");
        _otherPlayers.Remove(this);
        LocalPlayer = null;
    }
	
	void Update ()
    { 
	}

    [Command]
    public void CmdSendCluster(uint networkId, int num)
    {
       
       foreach(PlayerInstance player in _otherPlayers)
        {
            if (player.GetComponent<NetworkIdentity>().netId.Value == networkId)
                continue;
            else
                player.RpcRecieveCluster((int)networkId, num);
        }
        if (LocalPlayer.GetComponent<NetworkIdentity>().netId.Value != networkId)
            LocalPlayer.RpcRecieveCluster((int)networkId, num);
    }

    public void sendCluster(int num)
    {
        Debug.Log("sending cluster");
        CmdSendCluster(GetComponent<NetworkIdentity>().netId.Value, num);
    }

    [ClientRpc]
    public void RpcRecieveCluster(int target, int num)
    {
        if (!isLocalPlayer) return;
        Debug.Log("Recieved cluster");
        target *= 100;
        mainscript.Instance.createRow += num;
        GameObject.Find("Incoming Lines").GetComponent<Text>().text = "Incoming Lines: " + mainscript.Instance.createRow;
        int numRows = 10000 * num;

        PopupText("" + (numRows + (int)gameObject.GetComponent<NetworkIdentity>().netId.Value + target),debugMarker.transform.position);
    }

    [ClientRpc]
    public void RpcGameOver()
    {
        LobbyManager.s_Singleton.StopClient();
        GamePlay.gameStatus = GameState.GameOver;
    }

    public void PopupText(string value, Vector3 pos)
    {
        pos += Vector3.right * 0.5f;
        Transform parent = GameObject.Find("CanvasScore").transform;
        GameObject poptxt = Instantiate(mainscript.Instance.popupScore, pos, Quaternion.identity) as GameObject;
        poptxt.transform.GetComponentInChildren<Text>().text = "" + value;
        poptxt.transform.SetParent(parent);
        poptxt.transform.localScale = Vector3.one;
        Destroy(poptxt, 1);
    }

}
