using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerHealth))]
public class Human : PlayerHealth {

	// Use this for initialization
	void OnEnable ()
    {
        health = 110;
        //GetComponent<PlayerHealth>().SetHealthText();
        print("change");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
