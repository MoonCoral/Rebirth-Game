using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

	public Sprite open;
	bool openned;
	GameObject[] stuff;
	TileEngine te;
	int capacity;

	// Use this for initialization
	void Start () {
		te = GameObject.Find ("MapImporter").GetComponent<TileEngine> ();
		openned = false;	
		capacity = 1;
		stuff = new GameObject[capacity];
		for (int i = 0; i<capacity; i++) {
			stuff [i] = GameObject.FindGameObjectWithTag ("Reward");
		}
	}
	
	// Update is called once per frame


	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (Input.GetButtonUp ("Action")) {

				if (!openned && te.getReward) {

					//transfer contents from chest to player inventory
					te.getInventory()[0] = stuff[0];
					/*
					if (capacity != 0) {
						for (int i = 0; i < capacity; i++) {
							te.inventory[te.inventory.Length + i] = stuff[i];
							stuff[i] = null;
						}
						capacity = 0;
					}*/

					openned = true;
					this.gameObject.GetComponent<SpriteRenderer>().sprite = open;	

				}
				
			}			
		}
	}
}
