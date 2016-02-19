using UnityEngine;
using System.Collections;
using System.Linq;

public class mirrorSolver : MonoBehaviour {

    private ChestScript chest;

    void Start()
    {
        chest = transform.parent.GetComponentInChildren<ChestScript>();
    }

    void Update()
    {
        //do something usefull

        chest.openned = true;
    }
}
