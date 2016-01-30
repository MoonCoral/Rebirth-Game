using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

	public Sprite open;
	bool openned;
	GameObject[] stuff;
	TileEngine te;

	// Use this for initialization
	void Start () {
		openned = false;	

	}
	
	// Update is called once per frame


	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (Input.GetButtonUp ("Action")) {

				if (!openned) {

					openned = true;
					this.gameObject.GetComponent<SpriteRenderer>().sprite = open;	

				}
				
			}			
		}
	}
}
