using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour
{
    public PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerMove.enabled = true;
    }
}
