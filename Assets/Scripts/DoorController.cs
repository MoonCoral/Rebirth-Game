using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	Animator anim;
	public bool opened = false;
	BoxCollider2D col;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator>();
		col = this.gameObject.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (opened) {
			anim.SetBool ("opened", true);
			col.enabled = false;
		} else {
			anim.SetBool ("opened", false);
			col.enabled = true;
		}
	}
}
