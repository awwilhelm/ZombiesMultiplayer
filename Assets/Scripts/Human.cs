using UnityEngine;
using System.Collections;


public class Human : PlayerHealth {
    
	// Use this for initialization
	void Start ()
    {
        PlayerHealthStart();
        health = 110;
        //GetComponent<PlayerHealth>().SetHealthText();
	}
	
	// Update is called once per frame
	void Update () {
        PlayerMoveFixedUpdate();
	}
}
