using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	GameObject camera;

	public float speed = 1;
	public bool moving;

	public Sprite up;
	public Sprite down;
	public Sprite right;
	public Sprite left;
	public SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = this.GetComponent<SpriteRenderer> ();
		rend.sprite = down;
		camera = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.z = camera.transform.position.z;
		camera.transform.position = pos;
	}

	void FixedUpdate() {


		Vector3 temp = transform.position;
		temp.x += (speed * Input.GetAxis ("Horizontal"));
		temp.y += (speed * Input.GetAxis ("Vertical"));

		if (Input.GetAxis ("Horizontal") > 0)
			rend.sprite = right;
		else if (Input.GetAxis ("Horizontal") < 0)
			rend.sprite = left;
		else if (Input.GetAxis ("Vertical") > 0)
			rend.sprite = up;
		else if (Input.GetAxis ("Vertical") < 0)
			rend.sprite = down;



		transform.position = temp;

	}
}
