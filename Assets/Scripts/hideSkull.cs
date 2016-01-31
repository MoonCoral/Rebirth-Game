using UnityEngine;
using System.Collections;

public class hideSkull : MonoBehaviour {

	public bool collected;

	// Use this for initialization
	void Start () {
		this.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = false;
		collected = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Objects1").transform.FindChild ("Reward").GetComponent<rewardScript> ().collected) {
			this.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = true;
			collected = true;
		}	
	}
}
