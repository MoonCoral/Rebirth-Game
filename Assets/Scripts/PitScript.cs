using UnityEngine;
using System.Collections;

public class PitScript : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other) {

		if (other.gameObject.tag == "Player") {
			other.transform.position = new Vector3(24, -23, -1);				
		}
		else if (other.gameObject.name.Equals("move")) {
			BoxCollider2D[] cols  = other.gameObject.GetComponents<BoxCollider2D> ();
			for (int i = 0; i < cols.Length; i++)
				cols[i].enabled = false;
			this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}
}
