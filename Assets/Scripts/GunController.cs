using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GunController : NetworkBehaviour {

    public Weapons gun;
    [SerializeField]
    private Transform camTransform;

    void Start ()
    {
        gun = GetComponent<AK_47>();
        gun.enabled = true;
    }
	
	void Update ()
    {
        gun.CheckIfShooting();
    }
}
