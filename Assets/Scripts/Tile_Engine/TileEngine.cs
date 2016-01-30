using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileEngine : MonoBehaviour {

	public GameObject floor1, pillar2, stairsComp,stairsLeft,stairsMid,stairsRight,
	Switch, wall1Broken,wall1Ceiling, wall1Floor,wall1,wall2Broken,wall2Ceiling,
	wall2Floor,wall2,wallSide,wallSide2,wallV2, moveable, door, reward;

	private Player player;

	public TextAsset map;
	string[] mapData;

	public bool win;
	public GameObject[] inventory;
	
	private Dictionary<GameObject, Vector3> objectList;
	private GameObject[] skip;
	private string[] rules = new string[20]; 

	private int objects;

	// Use this for initialization
	void Awake () {
		player = FindObjectOfType<Player>();
		objectList = new Dictionary<GameObject, Vector3> ();

		mapData = map.text.Split('\n');
		int i = 0;

		//Background objects/walls come first
		while (i < mapData.Length) {
			string line = mapData[i];
			if (line[0].Equals('-')) {
				i++;
				break;
			}

			for (int j=0; j< line.Length; j++) {
				GameObject tile = getTile (line[j]);

				if (tile == null) {
				} else {	
					Vector3 pos = new Vector3 (j,-i,10);
					GameObject test = (GameObject)Instantiate(tile, pos, transform.rotation);
					test.transform.parent = transform.FindChild("Backgrounds");

				}
			}
			i++;
		}

		//Add in the Objects
		objects = i;
		i += addObjects();

		//Add rules
		int p = -1; //rule count
		int sk = -1; //skip object creation count
		while (i < mapData.Length) {
			string[] line = mapData[i].Split(':');

			//parse result : conditions
			if (line[0].StartsWith("start")) {
				string[] conditions = line[1].Split ('&');
				for (int z = 0; z < conditions.Length; z++) {
					string[] parts = conditions[z].Split(' ');
					if (parts[0].Equals("no")) {
						sk++;
						foreach (KeyValuePair<GameObject, Vector3> entry in objectList) {
							if (entry.Key.name.Equals(parts[1])) {
								skip[sk] = entry.Key;
								print ("yes");
								break;
							}
						}
					}
				}

			} else {
				p++;
				string rule = line[0] + ':' +  line[1];
				if (rule!= null) {
					rules[p] = rule;
				}
			}

			i++;
		}

		//destroy extra objects
		foreach (KeyValuePair<GameObject, Vector3> entry in objectList) {
			if (Array.Exists(skip, element => element.Equals(entry.Key))) {
				//remove object from skip array
				skip[Array.IndexOf(skip,entry.Key)] = null;
				break;
			}
		}
	}

	void Update () {
		//check rules 
		if (checkWin ())
			win = true;

		for (int i = 0; i<rules.Length; i++) {
			if (rules[i] != null) {
				string[] parts = rules[i].Split(':');
				if (parts[0].Equals ("win")) {
					if (checkWin (parts[1])) win = true;
				} else {
					if (parts[0].StartsWith ("create")) {
						string[] result = parts[0].Split(' ');
						create(result[1], parts[1]);
					}
				}
			}
		}

		//if (win)
			//Debug.Log ("won");
	}

	GameObject getTile(char c) {

		switch (c) {
		case 'w':
			return wall1;
			break;
		case 'b' :
			return wall2;
			break;
		case 'f' :
			return floor1;
			break;
		case 'm' : 
			return moveable;
			break;
		case 's' :
			return Switch;
			break;
		case 'd' :
			return door;
			break;
		case 'r' :
			return null;
			break;
		}

		return null;

	}


	void placeTile(int x, int y, GameObject tile) {

	}

	bool checkWin() {
		GameObject[] go = GameObject.FindGameObjectsWithTag("Switch");
		for (int i=0; i<go.Length; i++) {
			if (!go[i].GetComponent<Switch>().triggered) {
				return false;
			}
		}
		return true;
	}

	bool checkWin(String condition) {
		/*string[] conditions = condition.Split ('&');
		for (int i = 0; i<conditions.Length; i++) {
			if (conditions[i].StartsWith("have")) {
				string obj = conditions[i].Split(' ')[1];
				foreach (KeyValuePair<GameObject, Vector3> entry in objectList) {
					if (entry.Key.name.Equals(obj)) {
						if (!Array.Exists (inventory, element => element.Equals(entry.Key))) return false;
					}
				}
			}
		}	*/	
		return true;
	}

	void create(string obName, string condition) {
		/*string[] conditions = condition.Split ('&');
		for (int i = 0; i<conditions.Length; i++) {
			string name = conditions [i].Trim ();
			if (name.Contains ("switch")) {
				foreach (KeyValuePair<GameObject, Vector3> entry in objectList) {
					if (entry.Key.name.Equals (name)) {
						Switch sw = entry.Key.GetComponent<Switch> ();
						//Switch sw = GameObject.Find(name).GetComponent<Switch>();
						print (sw.triggered);
						if (!sw.triggered) {
							return;
						}
					}
				}
			}
		}*/

		if (checkWin()) {
			foreach (KeyValuePair<GameObject, Vector3> entry in objectList) {
				if (entry.Key.name.Equals (obName)) {
					GameObject ob2 = (GameObject)Instantiate (entry.Key, entry.Value, transform.rotation);
					break;
				}
			}
		}
	}

	int addObjects() {
		int i = objects;
		while (i < mapData.Length) {
			string[] line = mapData[i].Split (' ');
			char c = System.Char.Parse(line[0]);
			
			if (line[0][0].Equals('-')) {
				i++;
				break;
			}
			
			if (c.Equals('p'))
			{
				int x = System.Int32.Parse(line[1]);
				int y = System.Int32.Parse(line[2]);
				player.SetPosition(new Vector3(x, -y, -1));
			}
			else
			{
				GameObject ob = getTile(c);
				if (ob != null)
				{
					int x = System.Int32.Parse(line[1]);
					int y = System.Int32.Parse(line[2]);
					string name = line[3];
					Vector3 pos;
					pos = new Vector3(x, -y, 0);
					GameObject ob2 = (GameObject)Instantiate(ob, pos, transform.rotation);
					ob2.name = name;
					ob2.transform.parent = this.transform.FindChild("Objects");
				}

			}
			
			i++;		
		}

		return i - objects;

	}

	public void resetObjects() {
		GameObject objects = GameObject.Find ("Objects");
		int x = objects.transform.childCount;
		for (int i = 0; i< x; i++) {
			Destroy (objects.transform.GetChild(i).gameObject);
		}

		addObjects();
	}

}