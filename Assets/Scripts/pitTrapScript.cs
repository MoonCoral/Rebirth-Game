using UnityEngine;
using System.Collections;

public class pitTrapScript : MonoBehaviour {

	public Sprite pit;
	public bool triggered;
	public GameObject sw;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;	
		triggered = false;
		audio = this.gameObject.GetComponent<AudioSource> ();
		char n = this.name[4];
		sw = GameObject.Find ("switchT" + n);

	}
	
	// Update is called once per frame
	void Update () {

		char n = this.name[4];
		sw = GameObject.Find ("switchT" + n);

		if (sw != null) {
			if (sw.GetComponent<Switch> ().triggered == true) {
				triggered = true;
				this.GetComponent<SpriteRenderer> ().sprite = pit;
				this.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			}	
		}
	}



	void OnTriggerStay2D (Collider2D other) {
		if(!audio.isPlaying)
		audio.Play ();
		if (other.gameObject.tag == "Player") {
			other.transform.position = new Vector3(24, -23, -1);				
		}
	}
}
