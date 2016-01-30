using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{

    private bool playerControl;

	// Use this for initialization
	void Awake ()
	{
	    playerControl = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPlayerControl(bool toggle)
    {
        playerControl = toggle;
    }
}
