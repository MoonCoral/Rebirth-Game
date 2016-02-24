using UnityEngine;
using System.Collections;

public class BlockDestroy : MonoBehaviour {
	private bool activated;
	
	void Awake() { activated = false; }
	
	void Start () {}

	void Update () {}
	
	public void activate() {
		if (!activated) {
			GameObject.Destroy(gameObject);
			activated = true;
		}
	}
}
