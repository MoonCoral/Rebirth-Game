using UnityEngine;
using System.Collections;

public class BigButton : MonoBehaviour {

	public bool top, bottom, left, right;

    private Temple temple;

	GameObject t, b, l ,r;

	// Use this for initialization
	void Start ()
	{
	    temple = FindObjectOfType<Temple>();

		t = this.transform.parent.FindChild ("northCarpet").gameObject;
		b = this.transform.parent.FindChild ("southCarpet").gameObject;
		l = this.transform.parent.FindChild ("westCarpet").gameObject;
		r = this.transform.parent.FindChild ("eastCarpet").gameObject;
	
	}
	
	// Update is called once per frame
	void Update () {

		top = t.GetComponent<hideSkull> ().collected;
		bottom = b.GetComponent<hideTotem> ().collected;
		left = l.GetComponent<hideSerpent> ().collected;
		right = r.GetComponent<hideKnife> ().collected;
	
	}

	void OnTriggerStay2D (Collider2D other) {

		if (other.name.Equals("Player")) {

			if (top && bottom
			    && left && right)
				temple.Succes();

		}

	}
}
