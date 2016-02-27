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
	void Start ()
    {
        if(isLocalPlayer)
        {
            GameObject.Find("Scene Camera").SetActive(false);
            GetComponent<CharacterController>().enabled = true;
            FPSCharacterCam.enabled = true;
            audioListener.enabled = true;
            cameraLookScript.enabled = true;
        }
	}
	
}
