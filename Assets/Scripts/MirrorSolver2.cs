﻿using UnityEngine;
using System.Collections;

public class MirrorSolver2 : MonoBehaviour {
	public GameObject lightSink = null;
    private ChestScript chest;
    private Switch[] switches;

	void Start () {
		chest = transform.parent.GetComponentInChildren<ChestScript>();
		lightSink = transform.parent.transform.Find ("sink").gameObject;
		
		// apply rules to the room.
        RuleParser.implement(transform.parent.gameObject, FindObjectOfType<MapEngine>().RoomRules(transform.parent.name));
	}
	
	void Update () {

		if (!chest.openned) {
			//check mirror angles not in tolerance.
			if (lightSink == null || !lightSink.GetComponent<LightCheck> ().inToleranceFromSource ()) {
				return;
			}
		
			//check switch
			switches = transform.parent.GetComponentsInChildren<Switch> ();
			foreach (var s in switches) {
				if (s.tag == "Switch" && !s.triggered) {
					return;
				}
			}
		
			chest.openned = true;
			FindObjectOfType<Overlay>().ChestOpenText();
		}
	}
}
