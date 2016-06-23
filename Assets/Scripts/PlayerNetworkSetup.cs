using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    [SerializeField]
    public Camera FPSCharacterCam;

    [SerializeField]
    public AudioListener audioListener;

    [SerializeField]
    public CameraLook cameraLookScript;

    [SerializeField]
    public GameObject player;

    private static bool localPlayer;

    void Start()
    {
        print("Wake Wake");
        localPlayer = isLocalPlayer;
        print(isLocalPlayer);
        if (isLocalPlayer)
        {
            print("here");
            GameObject.Find("Scene Camera").SetActive(false);
            player.SetActive(true);
            //player.GetComponent<CharacterController>().enabled = true;
            //FPSCharacterCam.enabled = true;
            //audioListener.enabled = true;
            //cameraLookScript.enabled = true;
        }
    }

    public static bool IsLocal()
    {
        return localPlayer;
    }
	
}
