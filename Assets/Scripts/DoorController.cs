using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	Animator anim;
	public bool opened = false;
	BoxCollider2D col;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (opened) {
			anim.SetBool ("opened", true);
		} else {
			anim.SetBool ("opened", false);
		}
		opened = false;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			opened=true;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			AudioSource audio = gameObject.GetComponent<AudioSource>();
			if (!audio.isPlaying)
			{
				audio.PlayOneShot(audio.clip);
			}
		}
	}

	void OnTriggerLeave2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			AudioSource audio = gameObject.GetComponent<AudioSource>();
			if (!audio.isPlaying)
			{
				audio.PlayOneShot(audio.clip);
			};
		}
	}
}
