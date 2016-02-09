using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    private int damage = 25;
    private float range = 200;

    [SerializeField]
    private Transform camTransform;

    private RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckIfShooting();
	}

    void CheckIfShooting()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        //if(Input.GetButton("Fire1"))
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(Physics.Raycast(camTransform.TransformPoint(0, 0, 0.5f), camTransform.forward, out hit, range))
        {
            print(hit.transform.tag);
            if(hit.transform.tag == "Player")
            {
                string uidentity = hit.transform.name;
                CmdTellServerWhoWasShot(uidentity, damage);
            }
        }
    }

    [Command]
    void CmdTellServerWhoWasShot(string uniqueID, int dmg)
    {
        GameObject go = GameObject.Find(uniqueID);
        go.GetComponent<PlayerHealth>().DeductHealth(dmg);
    }
}
