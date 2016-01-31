using UnityEngine;
using System.Collections;

public class rewardScript : MonoBehaviour {

	GameObject roomChest;
	public bool collected;

	// Use this for initialization
	void Start () {

		roomChest = this.transform.parent.transform.FindChild ("Chest").gameObject;
	
	}
	
	// Update is called once per frame
	void Update () {

		collected = roomChest.GetComponent<ChestScript>().openned;


	
	}
}
