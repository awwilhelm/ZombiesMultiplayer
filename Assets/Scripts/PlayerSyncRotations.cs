using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerSyncRotations : NetworkBehaviour {

    [SyncVar (hook = "OnPlayerRotSynced")]
    private float syncPlayerRotation;

    [SyncVar (hook = "OnCamRotSynced")]
    private float syncCamRotation;

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform camTransform;
    private float lerpRate = 8;

    private float lastPlayerRot;
    private float lastCamRot;
    private float rotationThreshold = 1;

    private List<float> syncPlayerRotList = new List<float>();
    private List<float> syncCamRotList = new List<float>();
    private float closeEnough = 0.3f;
    
    private bool useHistoricalInterpolation = false;


    void Update()
    {
        LerpRotations();
    }

	void FixedUpdate ()
    {
        TransmitRotations();
	}

    void LerpRotations()
    {
        //playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
        //camTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
        if (!PlayerNetworkSetup.IsLocal())
        {
            if (useHistoricalInterpolation)
            {
                HistoricalInterpolation();
            }
            else
            {
                OrdinaryLerping();
            }
        }
    }

    void HistoricalInterpolation()
    {
        if (syncPlayerRotList.Count > 0)
        {
            LerpPlayerRot(syncPlayerRotList[0]);

            if (Mathf.Abs(playerTransform.localEulerAngles.y - syncPlayerRotList[0]) < closeEnough)
            {
                syncPlayerRotList.RemoveAt(0);
            }
        }

        if (syncCamRotList.Count > 0)
        {
            LerpCamRot(syncCamRotList[0]);

            if(Mathf.Abs(camTransform.localEulerAngles.x - syncCamRotList[0]) < closeEnough)
            {
                syncCamRotList.RemoveAt(0);
            }
        }
        print(syncCamRotList.Count);
    }

    void OrdinaryLerping()
    {
        LerpPlayerRot(syncPlayerRotation);
        LerpCamRot(syncCamRotation);
    }

    void LerpPlayerRot(float rotAngle)
    {
        Vector3 playerNewRot = new Vector3(0, rotAngle, 0);
        playerTransform.localRotation = Quaternion.Lerp(playerTransform.localRotation, Quaternion.Euler(playerNewRot), lerpRate * Time.deltaTime);
    }
    
    void LerpCamRot(float rotAngle)
    {
        Vector3 camNewRot = new Vector3(rotAngle, 0, 0);
        camTransform.localRotation = Quaternion.Lerp(camTransform.localRotation, Quaternion.Euler(camNewRot), lerpRate * Time.deltaTime);
    }

    [Command]
    void CmdProvideRotationsToServer(float playerRot, float camRot)
    {
        syncPlayerRotation = playerRot;
        syncCamRotation = camRot;
    }

    [ClientCallback]
    void TransmitRotations()
    {
        //if (Quaternion.Angle(playerTransform.rotation, lastPlayerRot) > rotationThreshold || Quaternion.Angle(camTransform.rotation, lastCamRot) > rotationThreshold)
        if(CheckIfBeyondThreshold(playerTransform.localEulerAngles.y, lastPlayerRot) || CheckIfBeyondThreshold(camTransform.localEulerAngles.x, lastCamRot))
        {
            lastPlayerRot = playerTransform.localEulerAngles.y;
            lastCamRot = camTransform.localEulerAngles.x;
            CmdProvideRotationsToServer(lastPlayerRot, lastCamRot);
        }
    }

    bool CheckIfBeyondThreshold(float rot1, float rot2)
    {
        if(Mathf.Abs(rot1-rot2) >rotationThreshold)
        {
            return true;
        }
        return false;
    }

    [Client]
    void OnPlayerRotSynced(float latestPlayerRot)
    {
        syncPlayerRotation = latestPlayerRot;
        syncPlayerRotList.Add(syncPlayerRotation);
    }

    [Client]
    void OnCamRotSynced(float latestCamRot)
    {
        syncCamRotation = latestCamRot;
        syncCamRotList.Add(syncCamRotation); 
    }


}
