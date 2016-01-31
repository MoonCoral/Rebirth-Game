using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileEngine : MonoBehaviour {

	public GameObject floor1, pillar2, stairsComp,stairsLeft,stairsMid,stairsRight,
	Switch, wall1Broken,wall1Ceiling, wall1Floor,wall1,wall2Broken,wall2Ceiling,
	wall2Floor,wall2,wallSide,wallSide2,wallV2, moveable, door, reward, chest, pit1;

	private Player player;

	public TextAsset map1, map2, map3,map4, mapBG;

	string[] mapData;

	public bool win;

	public bool getReward;
	GameObject[] inventory;
	public int capacity;
	
	private Dictionary<string, Dictionary<GameObject, Vector3>> objectList;
	private GameObject[] skip;
	private Dictionary<string, string[]> rules; 

	// Use this for initialization
	void Awake () {

		getReward = false;
		inventory = new GameObject[4]; // 4 = user inventory capacity

		player = FindObjectOfType<Player>();
		objectList = new Dictionary<string, Dictionary<GameObject, Vector3>>();
		rules = new Dictionary<string, string[]>();

		mapData = mapBG.text.Split('\n');
		int i = 0;
		if (!this.transform.FindChild ("Background")) {
			GameObject bg = new GameObject ();
			bg.name = "Background";
			bg.transform.parent = this.transform;
		}


		//Background objects/walls come first
		while (i < mapData.Length) {
			string line = mapData[i];
			if (line[0].Equals('-')) {
				i++;
				break;
			}

			for (int j=0; j< line.Length; j++) {
				GameObject tile;
				if (line[j].Equals('p')) {
					player.SetPosition(new Vector3 (j, -i, -1));
					tile = floor1;
				} else {
					tile = getTile (line[j]);
				}

				if (tile != null) {	
					Vector3 pos = new Vector3 (j,-i,10);
					GameObject test = (GameObject)Instantiate(tile, pos, transform.rotation);
					test.transform.parent = transform.FindChild("Background");
				}
			}
			i++;
		}


		//Add in the Objects
		CreateRoom (map1.text.Split ('\n'));
		CreateRoom (map2.text.Split ('\n'));
		CreateRoom (map3.text.Split ('\n'));

	}

	void CreateRoom(string[] mapData) {


		int i = addObjects(mapData);

		string objects = mapData[i].Split (' ')[1];
		objectList.Add(objects, new Dictionary<GameObject, Vector3>());

		string[] ts = new string[20];
		rules.Add(objects, ts);

		i++;

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
						foreach (KeyValuePair<GameObject, Vector3> entry in objectList[objects]) {
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
					rules[objects][p] = rule;
				}
			}

			i++;
		}

		//destroy extra objects
		foreach (KeyValuePair<GameObject, Vector3> entry in objectList[objects]) {
			if (Array.Exists(skip, element => element.Equals(entry.Key))) {
				//remove object from skip array
				skip[Array.IndexOf(skip,entry.Key)] = null;
				break;
			}
		}


	}

	void Update () {

		//check rules 
		if (checkSwitches ())
			getReward = true;
		if (getReward) {
			if (checkWin ())
				win = true;
		}

		if (playerRoom () != 0) {

			string ruleSet = "Rules" + playerRoom ();

			for (int i = 0; i<rules[ruleSet].Length; i++) {
				if (rules [ruleSet] [i] != null) {
					string[] parts = rules [ruleSet] [i].Split (':');
					if (parts [0].Equals ("win")) {
						if (checkWin ())
							win = true;
					} else {
						if (parts [0].StartsWith ("create")) {
							string[] result = parts [0].Split (' ');
							create (result [1], parts [1]);
						}
					}
				}
			}
		}

		if (win)
			Debug.Log ("won");
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
		case 'c' :
			return chest;
			break;
		case 'k' :
			return pit1;
			break;
		case 'r' :
			return reward;
			break;
		}


		return null;

	}


	void placeTile(int x, int y, GameObject tile) {

	}

	public bool checkWin() {
		GameObject r = GameObject.FindGameObjectWithTag ("Reward");
		for (int i = 0; i<inventory.Length; i++) {
			if (inventory[i] != null) {
				if (inventory[i].Equals(r)){ 
					return true;
				}
			}
		}
		return false;
	}
	
	public bool checkSwitches() {
		GameObject[] go = GameObject.FindGameObjectsWithTag("Switch");
		for (int i=0; i<go.Length; i++) {
			if (!go[i].GetComponent<Switch>().triggered) {
				return false;
			}
		}
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

		string ruleSet = "Rules"+playerRoom();

		if (checkWin()) {
			foreach (KeyValuePair<GameObject, Vector3> entry in objectList[ruleSet]) {
				if (entry.Key.name.Equals (obName)) {
					GameObject ob2 = (GameObject)Instantiate (entry.Key, entry.Value, transform.rotation);
					break;
				}
			}
		}
	}

	int addObjects(string[] mapData) {
		int i = 0;
		string objects = mapData [i].Split (' ')[1];
		if (!this.transform.FindChild (objects)) {
			GameObject bg = new GameObject ();
			bg.name = objects;
			bg.transform.parent = this.transform;
		}

		i++;

		string[] line = mapData[i].Split (' ');
		int ofx = System.Int32.Parse(line[0]);
		int ofy = System.Int32.Parse(line[1]);

		i++;

		while (i < mapData.Length) {
			line = mapData[i].Split (' ');
			char c = System.Char.Parse(line[0]);
			
			if (line[0][0].Equals('-')) {
				break;
			}

			GameObject ob = getTile(c);
			if (ob != null)
			{
				int x = System.Int32.Parse(line[1]) + ofx;
				int y = System.Int32.Parse(line[2]) + ofy;
				string name = line[3];
				Vector3 pos;
				pos = new Vector3(x, -y, 0);
				GameObject ob2 = (GameObject)Instantiate(ob, pos, transform.rotation);
				ob2.name = name;
				ob2.transform.parent = this.transform.FindChild(objects);
			}

			
			i++;		
		}

		return i;

	}
	Vector4 getCo(string[] line) {

		return new Vector4 (float.Parse (line [0]),
		                    -float.Parse (line [1]),
		                    float.Parse (line [2]),
		                    -float.Parse (line [3]));

	}

	int playerRoom() {

		Vector3 pos = player.GetPosition ();
		string[] line = map1.text.Split ('\n')[1].Split (' ');
		Vector4 room = getCo (line);

		if (room.x <= pos.x && pos.x <= room.z) {
			if (room.y > pos.y && pos.y > room.w)
				return 1;
		}

		line = map2.text.Split ('\n')[1].Split (' ');
		room = getCo(line);
		if (room.x <= pos.x && pos.x <= room.z) {
			if (room.y > pos.y && pos.y > room.w)
				return 2;
		}

		line = map3.text.Split ('\n')[1].Split (' ');
		room = getCo(line);
		if (room.x <= pos.x && pos.x <= room.z) {
			if (room.y > pos.y && pos.y > room.w)
				return 3;
		}

		return 0;

	
	}

	public void resetObjects() {

		string name = "Objects"+playerRoom();
		GameObject objects = GameObject.Find (name);
		int x = objects.transform.childCount;
		for (int i = 0; i< x; i++) {
			Destroy (objects.transform.GetChild(i).gameObject);
		}

		switch (playerRoom ()) {
		case 1:
			addObjects(map1.text.Split ('\n'));
			break;
		case 2:
			addObjects(map2.text.Split ('\n'));
			break;
		case 3:
			addObjects(map3.text.Split ('\n'));
			break;
		/*
		case 4:
			addObjects(map4.text.Split ('\n'));
			break;
		*/
		default:
			break;
			
		}
	}

	public GameObject[] getInventory() {
		return inventory;
	}

}