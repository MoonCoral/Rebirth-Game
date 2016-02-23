using UnityEngine;
using System.Collections;

public class BridgeFunc : MonoBehaviour {

    private Switch[] switches;
    private PermaSwitch[] permaSwitches;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        switches = transform.parent.GetComponentsInChildren<Switch>();
        permaSwitches = transform.parent.GetComponentsInChildren<PermaSwitch>();
		foreach (var s in permaSwitches){
            if (s.tag == "Switch" && s.triggered) {
	            
	        }
	    }
	
	}
}
