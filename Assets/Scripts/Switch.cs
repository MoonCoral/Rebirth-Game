using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	public bool triggered;

	void Start () {
		triggered = false;
	}

	void OnTriggerExit2D(Collider2D other) {
		triggered = false;
	}

	void OnTriggerStay2D(Collider2D other) {
		triggered = true;
	}
}
