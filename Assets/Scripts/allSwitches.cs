using UnityEngine;
using System.Collections;

public class allSwitches : MonoBehaviour
{

    private ChestScript chest;
    private Switch[] switches;
	private bool triggered;

	void Awake()
	{
		triggered = false;
	}

    void Start ()
	{
	    chest = transform.parent.GetComponentInChildren<ChestScript>();
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (!triggered) {
			switches = transform.parent.GetComponentsInChildren<Switch> ();

			foreach (var s in switches) {
				if (s.tag == "Switch" && !s.triggered) {
					return;
				}
			}

			FindObjectOfType<Overlay>().ChestOpenText();
			chest.openned = true;
			triggered = true;
		}
	}
}
