using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : PlayerMove {

    [SyncVar (hook = "OnHealthChange")]
    public int health = 100;

    private Text healthText;
	// Use this for initialization
	public void PlayerHealthStart () {
        PlayerMoveStart();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
    }

    public void SetHealthText()
    {
        healthText.text =
            "Health " 
            + health.ToString(); 
    }

    public void DeductHealth(int dmg)
    {
        health -= dmg;
        CheckIfDied(health);
    }

    void OnHealthChange(int hlth)
    {
        health = hlth;
        SetHealthText();
    }

    void CheckIfDied(int hlth)
    {
        if(hlth <= 0)
        {
            CmdPleaseDestroyThisThing(gameObject);
        }
    }

    [Command]
    void CmdPleaseDestroyThisThing(GameObject thing)
    {
        NetworkServer.Destroy(thing);
    }
}
