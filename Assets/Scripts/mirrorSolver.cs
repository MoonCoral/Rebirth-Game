using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class mirrorSolver : MonoBehaviour {

    private ChestScript chest;

    void Start()
    {
        chest = transform.parent.GetComponentInChildren<ChestScript>();

        // apply rules to the room.
        RuleParser.implement(transform.parent.gameObject, FindObjectOfType<MapEngine>().RoomRules(transform.parent.name));
    }

    void Update()
    {
        //do something usefull

        chest.openned = true;
    }
}
