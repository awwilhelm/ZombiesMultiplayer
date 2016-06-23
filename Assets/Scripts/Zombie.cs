using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerHealth))]
public class Zombie : PlayerHealth {

	// Use this for initialization
	void OnEnable()
    {
        PlayerHealthStart();
        health = 150;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
