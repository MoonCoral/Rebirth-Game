using UnityEngine;
using System.Collections;

public class CloneBumper : MonoBehaviour
{

    private Vector3 push;

    void Start()
    {
        switch (name[0])
        {
            case 'n':
                push = new Vector3(0, 1, 0);
                break;
            case 'w':
                push = new Vector3(-1, 0, 0);
                break;
            case 's':
                push = new Vector3(0, -1, 0);
                break;
            case 'e':
                push = new Vector3(1, 0, 0);
                break;
        }

        switch (FindObjectOfType<MapEngine>().RoomOrientation(transform.parent.name))
        {
            case 0:
                break;
            case 1:
                push = new Vector3(push.y, push.x, push.z);
                break;
            case 2:
                push = new Vector3(-push.x, push.y, push.z);
                break;
            case 3:
                push = new Vector3(-push.y, push.x, push.z);
                break;
            case 4:
                push = new Vector3(push.x, -push.y, push.z);
                break;
            case 5:
                push = new Vector3(push.y, -push.x, push.z);
                break;
            case 6:
                push = new Vector3(-push.x, -push.y, push.z);
                break;
            case 7:
                push = new Vector3(-push.y, -push.x, push.z);
                break;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
		//move when player in the room
        if (other.gameObject.tag == "ShadowPlayer" && FindObjectOfType<MapEngine>().ActiveRoom() == transform.parent.gameObject.name)
        {
            other.gameObject.GetComponent<ShadowPlayer>().SetVelocity(push);
        }
    }
}
