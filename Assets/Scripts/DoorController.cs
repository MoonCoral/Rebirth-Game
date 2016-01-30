using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	Animator anim;
	public bool opened = false;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (opened)
			anim.SetBool ("opened", true);
		else
			anim.SetBool ("opened", false);
	}
}
