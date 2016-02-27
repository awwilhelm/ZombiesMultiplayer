using UnityEngine;
using System.Collections;

public class AK_47 : Weapons {

	// Use this for initialization
    
	void Start ()
    {
        WeaponsStart();
        
	}

    void Reset()
    {
        fireRate = 10;
        damage = 30;
        range = 300;
        clipSize = 10;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
