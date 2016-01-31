using UnityEngine;
using System.Collections;

public class DarknessScript : MonoBehaviour {

	GameObject onSwitch, offSwitch;
	Light MC;

	// Use this for initialization
	void Start () {

		offSwitch = GameObject.Find ("Lswitch2");
		onSwitch = GameObject.Find ("Lswitch1");
		MC = GameObject.Find ("Main Camera").GetComponent<Light>();

	}
	
	// Update is called once per frame
	void Update () {

		if (offSwitch.GetComponent<Switch> ().triggered) {
			MC.spotAngle = 16;
		}

		if (onSwitch.GetComponent<Switch> ().triggered) {
			MC.spotAngle = 100;
		}

	}
}
