using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Weapons : NetworkBehaviour {

    public float fireRate = 5f;
    public int damage = 25;
    public float range = 200;
    public float clipSize = 10;
    
    private Transform camTransform;

    private RaycastHit hit;

    void Awake()
    {
        camTransform = transform.FindChild("Player Camera");
    }

    public void CheckIfShooting()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Command]
    void CmdTellServerWhoWasShot(string uniqueID, int dmg)
    {
        GameObject go = GameObject.Find(uniqueID);
        go.GetComponent<PlayerHealth>().DeductHealth(dmg);
    }

    void Shoot()
    {
        if (Physics.Raycast(camTransform.TransformPoint(0, 0, 0.5f), camTransform.forward, out hit, range))
        {
            //print(hit.transform.tag);
            if (hit.transform.tag == "Player")
            {
                string uidentity = hit.transform.name;
                print("call " + uidentity);
                CmdTellServerWhoWasShot(uidentity, damage);
            }
        }
    }
}
