using UnityEngine;
using System.Collections;

public class MirrorSolver2 : MonoBehaviour {

    private ChestScript chest;
    private Switch[] switches;
	
	void Start () {
		chest = transform.parent.GetComponentInChildren<ChestScript>();	
	}
	
	void Update () {
		//check mirror angles
		//....
		
		//check switch
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
