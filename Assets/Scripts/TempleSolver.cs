using UnityEngine;
using System.Collections;

public class TempleSolver : MonoBehaviour {

	private GameObject clone;
	public GameObject sister;
	
	// Use this for initialization
	void Start () {
		clone = transform.parent.Find("Clone").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Reward[] rewards = FindObjectsOfType<Reward>();
		if (rewards[0].Gathered() && rewards[1].Gathered() && rewards[2].Gathered() && rewards[3].Gathered())
		{
			//destroy clone, create sister
			GameObject.Destroy(clone);
			Vector3 vec3 = transform.parent.parent.FindChild(FindObjectOfType<MapEngine>().ActiveRoom()).FindChild("PitSpawnClone").position;
			GameObject s = (GameObject) Instantiate(sister, new Vector3(vec3.x, vec3.y, -1.0f), transform.rotation);	
			s.transform.parent = gameObject.transform;			
        }	
	}
}
