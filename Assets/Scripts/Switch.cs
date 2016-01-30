using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	public bool triggered;
	public Sprite norm;
	public Sprite pressed;
	SpriteRenderer spriteRend;

	void Start () {
		triggered = false;
		spriteRend = this.GetComponent<SpriteRenderer> ();
		spriteRend.sprite = norm;
	}

	void OnTriggerExit2D(Collider2D other) {
		triggered = false;
		spriteRend.sprite = norm;
	}

	void OnTriggerStay2D(Collider2D other) {
		triggered = true;
		spriteRend.sprite = pressed;
	}
}
