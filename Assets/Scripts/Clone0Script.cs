using UnityEngine;
using System.Collections;

public class Clone0Script : MonoBehaviour {
	private Player player;
	private CameraControl cam;
    private MapEngine mapEngine;
	private GameObject clone;
	private bool canMove;
	
    public float speed = 1f;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();	
		cam = FindObjectOfType<CameraControl>();
		clone = transform.parent.Find("Clone").gameObject;
		mapEngine = FindObjectOfType<MapEngine>();
		canMove = false;
	}
	
	void FixedUpdate () {
		//move only when player is in the room
		if (mapEngine.ActiveRoom() == transform.parent.gameObject.name) {
			if (Input.GetButtonUp ("Action")) { //toggle can move by action button
				if (canMove) canMove = false;
				else canMove = true;
			}
		} else {
			canMove = false;
		}
		if (canMove) {
			player.SetPlayerControl(false);
			cam.SetClone(clone);
			Vector2 temp = clone.transform.position;
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");
			temp.x += x * speed * (Time.deltaTime * 4);
			temp.y += y * speed * (Time.deltaTime * 4);		
			clone.transform.position = temp;
		} else {
			player.SetPlayerControl(true);
		}
	}
}
