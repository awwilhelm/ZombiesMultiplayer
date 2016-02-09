using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMove))]
public class PlayerHealth : PlayerMove {

    [SyncVar (hook = "OnHealthChange")]
    public int health = 100;

    private Text healthText;
	// Use this for initialization
	void Awake () {
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        print("text " + healthText == null);
	}

    void Start()
    {
        GetComponent<Human>().enabled = true;
        //SetHealthText();
    }

    public void SetHealthText()
    {
        print("after");
        if (isLocalPlayer)
        {
            print(healthText == null);
            healthText.text =
                "Health " 
                + health.ToString(); 
        }
        print("new health: " + health);
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
