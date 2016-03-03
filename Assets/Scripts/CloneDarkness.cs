using UnityEngine;
using System.Collections;

public class CloneDarkness : MonoBehaviour
{

    GameObject onSwitch, offSwitch;
    Light MCC, MCP;

    // Use this for initialization
    void Start()
    {

        offSwitch = GameObject.Find("Lswitch2");
        onSwitch = GameObject.Find("Lswitch1");
        MCC = transform.parent.Find("Clone").GetComponentInChildren<Light>();
        MCP = GameObject.Find("Player").GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        if (offSwitch.GetComponent<Switch>().triggered)
        {
            MCC.enabled = true;
            MCP.enabled = false;
        }

        if (onSwitch.GetComponent<Switch>().triggered)
        {
            MCC.enabled = false;
            MCP.enabled = true;
        }

    }
}
