using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

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
	}
	
	// Update is called once per frame
	void Update () {
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
