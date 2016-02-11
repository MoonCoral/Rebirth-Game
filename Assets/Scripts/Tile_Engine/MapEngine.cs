using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.ConstrainedExecution;

public class MapEngine : MonoBehaviour
{

    private class Room
    {
        private TextAsset roomData;

        public readonly int Width;
        public readonly int Height;
        public readonly Dictionary<char, string> Legend;
        public readonly string Name;
        public readonly List<string> Background; //change to char[][]
        public readonly List<string> Objects;
        public readonly List<string> Rules;
        public readonly List<Vector2> Doors;
        

        public Room(string path)
        {
            roomData = (TextAsset)Resources.Load(path, typeof(TextAsset));

            string[] data = roomData.text.Split(new string[] {"\n- "}, StringSplitOptions.RemoveEmptyEntries);

            Legend = new Dictionary<char, string>();
            Background = new List<string>();
            Objects = new List<string>();
            Rules = new List<string>();
            Doors = new List<Vector2>();

            Name = data[0];

            for ( int i = 1; i < data.Length; i++ )
            {
                if (data[i].StartsWith("Legend"))
                {
                    string[] legendData = data[i].Split('\n');
                    

                    for (int j = 1; j < legendData.Length; j++)
                    {
                        Legend.Add(legendData[j][0], legendData[j].Split(' ')[1]);
                    }
                }
                if (data[i].StartsWith("Entries"))
                {
                    string[] doorsData = data[i].Split('\n');

                    for (int j = 1; j < doorsData.Length; j++)
                    {
                        string[] line = doorsData[j].Split(' ');
                        Doors.Add(new Vector2(float.Parse(line[0]), float.Parse(line[0])));
                    }
                }
				else if (data[i].StartsWith("Exits"))
				{
					string[] doorsData = data[i].Split('\n');
					
					for (int j = 1; j < doorsData.Length; j++)
					{
						string[] line = doorsData[j].Split(' ');
						Doors.Add(new Vector2(float.Parse(line[0]), float.Parse(line[0])));
					}
				}
                else if (data[i].StartsWith("Background"))
                {
                    string[] backgroundData = data[i].Split('\n');

                    for (int j = 1; j < backgroundData.Length; j++)
                    {
                        Background.Add(backgroundData[j]);
                    }
                }
                else if (data[i].StartsWith("Objects"))
                {
                    string[] objectsData = data[i].Split('\n');

                    for (int j = 1; j < objectsData.Length; j++)
                    {
                        Objects.Add(objectsData[j]);
                    }
                }
                else if (data[i].StartsWith("Rules"))
                {
                    string[] rulesData = data[i].Split('\n');

                    for (int j = 1; j < rulesData.Length; j++)
                    {
                        Rules.Add(rulesData[j]);
                    }
                }
            }

            Height = Background.Count;
            Width = 0;
            foreach (var line in Background)
            {
                if (line.Length > Width)
                {
                    Width = line.Length;
                }
            }
        }

		public void RotateCounterClockwise(int amount)
		{
			Debug.Log ("Not implemented!");
		}

		public void FlipHorizontal()
		{
			Debug.Log ("Not implemented!");
		}

		public void FlipVertical()
		{
			Debug.Log ("Not implemented!");
		}

    }

    public TextAsset LevelData;
    public Player Player;

    private int Width;
    private int Height;
    private Dictionary<char, string> Legend;
    private string Name;
    private char[][] background;

    private List<string> roomNames;
    private List<string> usedRooms; 
    private Dictionary<string, Room> rooms;
    private Dictionary<Vector4, string> roomPositions;
	private Dictionary<string, Vector4> roomLocations;

    private Dictionary<string, string> levelInfo; 

    public void Awake()
    {
        LoadLevel();
        CreateMap();
    }

    public void LoadLevel()
    {
		Debug.Log("1!");
		
        rooms = new Dictionary<string, Room>();
        roomNames = new List<string>();
        roomPositions = new Dictionary<Vector4, string>();
		roomLocations = new Dictionary<string, Vector4>();
        levelInfo = new Dictionary<string, string>();
        usedRooms = new List<string>();

        Legend = new Dictionary<char, string>();

        string[] data = LevelData.text.Split(new string[] { "\n- " }, StringSplitOptions.RemoveEmptyEntries);

        Name = data[0];
		
		Debug.Log(Name+data.Length);

        for (int i = 1; i < data.Length; i++)
        {
			Debug.Log(data[i]);
            if (data[i].StartsWith("Legend"))
            {
                string[] legendData = data[i].Split('\n');

                for (int j = 1; j<legendData.Length; j++)
                {
                    Legend.Add(legendData[j][0], legendData[j].Split(' ')[1]);
                }
            }
            else if (data[i].StartsWith("Rooms"))
            {
                string[] roomData = data[i].Split('\n');

                for (int j = 1; j<roomData.Length; j++)
                {
                    roomNames.Add(roomData[j]);
                    rooms.Add(roomData[j], new Room(roomData[j]));
                }
            }
            else if (data[i].StartsWith("Level"))
            {
				Debug.Log("LevelDat");
				
                string[] levelData = data[i].Split('\n');

                for (int j = 1; j<levelData.Length; j++)
                {
					Debug.Log("LevelDat "+levelData[j]);
                    levelInfo.Add(levelData[j].Split(' ')[0], levelData[j].Split(' ')[1]);
                }
            }
        }

        Height = int.Parse(levelInfo["Height"]);
        Width = int.Parse(levelInfo["Width"]);

        background = new char[Height][];
        for (int i = 0; i < background.Length; i++ )
        {
            background[i] = new char[Width];
        }

    }

    public void CreateMap()
    {
		int random = randomRange (0, rooms.Count);

		int roomNumber = int.Parse (levelInfo ["RoomNumber"]);

		while (usedRooms.Count < roomNumber) {
			if (!usedRooms.Contains (roomNames [random])) {
				usedRooms.Add (roomNames [random]);
			}
		}

		for (int i = 0; i < roomNumber; i++) {
			CreateRoom(i*20 , i*20, usedRooms[i]);
		}

		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				GameObject tile;

				if (background[x][y].Equals('p')) {
					Player.SetPosition(new Vector3 (x, -y, -1));
					tile = (GameObject)Resources.Load(Legend['f']);
				} else {
					tile = (GameObject)Resources.Load(Legend[background[x][y]]);
				}
				
				if (tile != null) {	
					Vector3 pos = new Vector3 (x,-y,0.3f);
					GameObject test = (GameObject)Instantiate(tile, pos, transform.rotation);
					test.transform.parent = transform.FindChild("Background");
				}
			}
		}

    }

	public void CreateRoom(int x, int y, string roomName)
	{
		Room room = rooms[roomName];

		for (int i = x; i < room.Width + x; i++) {
			for (int j = y; j < room.Height + y; j++) {
				background[i][j] = room.Background[i][j];
			}
		}

		roomPositions.Add(new Vector4 (x, y, room.Width + x, room.Height + y), room.Name);
		roomLocations.Add(room.Name, new Vector4 (x, y, room.Width + x, room.Height + y));
	}

	public void LoadRoom(string roomName)
	{
		int X = (int)roomLocations [roomName].x;
		int Y = (int)roomLocations [roomName].y;

		Room room = rooms[roomName];

		if (!this.transform.FindChild (roomName)) {
			GameObject roomObject = new GameObject ();
			roomObject.name = roomName;
			roomObject.transform.parent = this.transform;
		}

		
		for (int i = 0; i < room.Objects.Count; i++) {

			string[] line = room.Objects[i].Split(' ');
			
			GameObject ob = (GameObject)Resources.Load(Legend[line[0][0]]);
			if (ob != null)
			{
				int x = System.Int32.Parse(line[1]) + X;
				int y = System.Int32.Parse(line[2]) + Y;
				string name = line[3];
				Vector3 pos;
				pos = new Vector3(x, -y, 0);
				GameObject ob2 = (GameObject)Instantiate(ob, pos, transform.rotation);
				ob2.transform.name = name;
				ob2.transform.parent = this.transform.FindChild(roomName);
			}
					
		}
	}

    public void ReloadRoom()
    {
		string name = ActiveRoom();
		GameObject room = GameObject.Find(name);
		int x = room.transform.childCount;
		for (int i = 0; i< x; i++) {
			Destroy (room.transform.GetChild(i).gameObject);
		}

		LoadRoom (name);
    }

    public string ActiveRoom()
    {
        Vector3 pos = Player.GetPosition();

        foreach (var room in roomPositions.Keys)
        {
            if (room.x <= pos.x && pos.x <= room.z)
            {
                if (room.y > pos.y && pos.y > room.w)
                    return roomPositions[room];
            }
        }

        return "Outside";
    }

    private int randomRange(int start, int end)
    {
        double r = Random.value;
        r *= end;
        r += 0.5 + end;
        return (int) Math.Round(r);
    }
    
}
