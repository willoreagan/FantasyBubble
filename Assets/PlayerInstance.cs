using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

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
    public void CmdSendCluster(uint networkId)
    {
       
       foreach(PlayerInstance player in _otherPlayers)
        {
            if (player.GetComponent<NetworkIdentity>().netId.Value == networkId)
                continue;
            else
                player.RpcRecieveCluster((int)networkId);
        }
        if (LocalPlayer.GetComponent<NetworkIdentity>().netId.Value != networkId)
            LocalPlayer.RpcRecieveCluster((int)networkId);
    }

    public void sendCluster()
    {
        Debug.Log("sending cluster");
        CmdSendCluster(GetComponent<NetworkIdentity>().netId.Value);
    }

    [ClientRpc]
    public void RpcRecieveCluster(int target)
    {
        if (!isLocalPlayer) return;
        Debug.Log("Recieved cluster");
        target *= 100;
        mainscript.Instance.PopupScore(10000 + (int)gameObject.GetComponent<NetworkIdentity>().netId.Value + target, debugMarker.transform.position);
    }

}
