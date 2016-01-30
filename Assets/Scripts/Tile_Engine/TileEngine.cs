using UnityEngine;
using System.Collections;

public class TileEngine : MonoBehaviour {

	public GameObject floor1, pillar2, stairsComp,stairsLeft,stairsMid,stairsRight,
	Switch, wall1Broken,wall1Ceiling, wall1Floor,wall1,wall2Broken,wall2Ceiling,
	wall2Floor,wall2,wallSide,wallSide2,wallV2, moveable, door, player, reward;

	public TextAsset map;
	string mapData;


	// Use this for initialization
	void Start () {

		string[] mapData = map.text.Split('\n');
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
					test.transform.parent = transform;

				}
			}
			i++;
		}
		//Add in the Objects
		while (i < mapData.Length) {
			string[] line = mapData[i].Split (' ');

			GameObject ob = getTile (System.Char.Parse(line[0]));
			if (ob != null) {
				int x = System.Int32.Parse (line[1]);
				int y = System.Int32.Parse (line[2]);
				string name = line[3];
				Vector3 pos;
				if (name.Equals("Player"))
				    pos = new Vector3 (x, -y, -1);
				else
				    pos = new Vector3 (x, -y, 0);
				GameObject ob2 = (GameObject) Instantiate(ob, pos, transform.rotation);
				ob2.name = name;
				
			}

			i++;		
		}







	
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
			return floor1;
			break;
		case 'p':
			return player;
			break;
		}

		return null;

	}


	void placeTile(int x, int y, GameObject tile) {

	}
	

}
