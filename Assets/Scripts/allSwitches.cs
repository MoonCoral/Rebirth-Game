using UnityEngine;
using System.Collections;

public class allSwitches : MonoBehaviour
{

    private ChestScript chest;
    private Switch[] switches;

    void Start ()
	{
	    chest = transform.parent.GetComponentInChildren<ChestScript>();
    }
	
	// Update is called once per frame
	void Update ()
	{
        switches = transform.parent.GetComponentsInChildren<Switch>();

        foreach (var s in switches)
	    {
            if (s.tag == "Switch" && !s.triggered)
	        {
	            return;
	        }
	    }

	    chest.openned = true;
	}
}
