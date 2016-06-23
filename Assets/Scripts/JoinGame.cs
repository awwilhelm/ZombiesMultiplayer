using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JoinGame : NetworkBehaviour {

    public GameObject playerPrefab;
    public GameObject playerUI;
    public GameObject thisUI;
    private bool firstTime = false;
    private NetworkManagerHUD networkManagerHUD;
    private NetworkConnection conn;
	// Use this for initialization
	void Start ()
    {
        networkManagerHUD = GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Call()
    {

    }

    public void Join()
    {
       // NetworkServer.Spawn(player);
        //CmdJoin();
        //playerUI.SetActive(true);
        //thisUI.SetActive(false);
    }

    //[Command]
    //public void CmdJoin()
    //{
    //    GameObject player = Instantiate<GameObject>(playerPrefab);
    //    player.name = "PlayerJoin(Clone)";
    //    conn = GameObject.Find("PlayerJoin(Clone)").GetComponent<NetworkIdentity>().connectionToClient;
    //    NetworkConnection temp = GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().GetCurrentConnection().GetComponent<NetworkIdentity>().connectionToClient;
    //    print(temp.connectionId);
    //    //conn = player.GetComponent<NetworkIdentity>().connectionToClient;
    //    print(NetworkManager.singleton.client.connection.playerControllers[0].playerControllerId);
    //    if (false)
    //    {
    //        NetworkServer.AddPlayerForConnection(conn, player, 0);
    //    }
    //    else
    //    {
    //        NetworkServer.ReplacePlayerForConnection(conn, player, 0);
    //    }
    //    //conn = player.GetComponent<NetworkIdentity>().connectionToClient;
    //    //NetworkServer.DestroyPlayersForConnection(conn);
    //}
}
