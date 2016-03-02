using UnityEngine;
using System.Collections;

public class CloneEast : MonoBehaviour {

    private Vector3 push;

    void Start()
    {
        push = new Vector3(1, 0, 0);

        //return flipHorizontal * 4 + flipVertical * 2 + transposed;

        switch (FindObjectOfType<MapEngine>().RoomOrientation(transform.parent.name))
        {
            case 0:
                break;
            case 1:
                push = new Vector3(push.y, push.x, push.z);
                break;
            case 2:
                push = new Vector3(push.x, -push.y, push.z);
                break;
            case 3:
                push = new Vector3(push.y, push.x, push.z);
                push = new Vector3(push.x, -push.y, push.z);
                break;
            case 4:
                push = new Vector3(-push.x, push.y, push.z);
                break;
            case 5:
                push = new Vector3(push.y, push.x, push.z);
                push = new Vector3(-push.x, push.y, push.z);
                break;
            case 6:
                push = new Vector3(push.x, -push.y, push.z);
                push = new Vector3(-push.x, push.y, push.z);
                break;
            case 7:
                push = new Vector3(push.y, push.x, push.z);
                push = new Vector3(push.x, -push.y, push.z);
                push = new Vector3(-push.x, push.y, push.z);
                break;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "ShadowPlayer")
        {
            other.gameObject.GetComponent<ShadowPlayer>().SetVelocity(push);
        }
    }
}
