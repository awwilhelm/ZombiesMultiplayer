using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerSyncPosition : NetworkBehaviour {

    [SyncVar (hook = "SyncPositionValues")]
    private Vector3 syncPos;

    [SerializeField]
    Transform myTransform;

    private float lerpRate;
    private float normalLerpRate = 14;
    private float fasterLerpRate = 25;

    private Vector3 lastPos;
    private float movementThreshhold = 0.5f;

    private NetworkClient nClient;
    private Text latencyText;
    private int latency;

    public List<Vector3> syncPosList = new List<Vector3>();
    public int syncPosListCount=0;

    private bool useHistoricLerping = false;
    private float closeEnough = 0.1f;


    void Start()
    {
        lerpRate = normalLerpRate;
        //nClient = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().client;
        latencyText = GameObject.Find("LatencyText").GetComponent<Text>();
    }

    void Update()
    {
        LerpPosition();
        ShowLatency();
        syncPosListCount = syncPosList.Count;

    }

	void FixedUpdate()
    {
        TransmitPosition();
    }

    void LerpPosition()
    {
        if(!isLocalPlayer)
        {
            if(useHistoricLerping)
            {
                HistoricalLerping();
            } else
            {
                OrdinaryLerping();
            }
        }

    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if(isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > movementThreshhold)
        {
            CmdProvidePositionToServer(myTransform.position);
            lastPos = myTransform.position;
        }
    }

    [ClientCallback]
    void SyncPositionValues(Vector3 latestPos)
    {
        syncPos = latestPos;
        if(!isLocalPlayer)
        {
            syncPosList.Add(syncPos);
        }
    }

    void ShowLatency()
    {
        if(isLocalPlayer)
        {
            //latency = nClient.GetRTT();
            //latencyText.text = latency.ToString();
            //if (syncPosList.Count > 10)
            //{
             //   latencyText.text = latency.ToString() + " " + syncPosList.Count;
            //} else
        }
    }


    void OrdinaryLerping()
    {
        myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        
    }

    void HistoricalLerping()
    { 
        if(syncPosList.Count > 0)
        {
            //print(syncPosList.Count);
            myTransform.position = Vector3.Lerp(myTransform.position, syncPosList[0], Time.deltaTime * lerpRate);
            if (Vector3.Distance(myTransform.position, syncPosList[0]) < closeEnough)
            {
                syncPosList.RemoveAt(0);
            }

            if(syncPosList.Count > 10)
            {
                print("fast");
                lerpRate = fasterLerpRate;
            } else
            {
                print("norm");
                lerpRate = normalLerpRate;
            }
        }
    }

}
