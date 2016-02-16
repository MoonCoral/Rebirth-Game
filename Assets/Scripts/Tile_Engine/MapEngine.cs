using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.ConstrainedExecution;
using System.Text;

public class MapEngine : MonoBehaviour
{
	private class Objects
	{
		public Vector2 Position;
		public char Prefab;
		public string Name;

		public Objects(string s)
		{
			string[] line = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			Prefab = char.Parse(line[0]);
			Position = new Vector2(int.Parse(line[1]), int.Parse(line[2]));
			Name = line[3];
		}
	}

	private class Doors
	{
		public Vector2 Position;
		public char Direction;

		public Doors(string s)
		{
			string[] line = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			Position = new Vector2(float.Parse(line[0]), float.Parse(line[1]));
			Direction = char.Parse(line[2]);
		}
	}

	private class Room
	{
		private TextAsset roomData;

		private int width;
		private int height;
		private int size; 
		public readonly Dictionary<char, string> Legend;
		public readonly string Name;
		private char[,] background;
		public readonly List<Objects> Objects;
		public readonly List<Doors> Entries;
		public readonly List<Doors> Exits;
		public readonly List<string> Rules;


		public Room(string path)
		{
			roomData = (TextAsset)Resources.Load(path, typeof(TextAsset));

			string[] data = roomData.text.Split(new string[] {"\n- "}, StringSplitOptions.RemoveEmptyEntries);

			Legend = new Dictionary<char, string>();
			List<string>  background = new List<string>();
			Objects = new List<Objects>();
			Exits = new List<Doors>();
			Entries = new List<Doors>();

			Rules = new List<string>();

			Name = data[0];

			for ( int i = 1; i < data.Length; i++ )
			{
				if (data[i].StartsWith("Legend"))
				{
					string[] legendData = data[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

					for (int j = 1; j < legendData.Length; j++)
					{
						string[] line = legendData[j].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
						Legend.Add(char.Parse(line[0]), line[1]);
					}
				}
				if (data[i].StartsWith("Entries"))
				{
					string[] doorsData = data[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

					for (int j = 1; j < doorsData.Length; j++)
					{
						string[] line = doorsData[j].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
						Entries.Add(new Doors(doorsData[j]));
					}
				}
				else if (data[i].StartsWith("Exits"))
				{
					string[] doorsData = data[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
					
					for (int j = 1; j < doorsData.Length; j++)
					{
						Exits.Add(new Doors(doorsData[j]));
					}
				}
				else if (data[i].StartsWith("Background"))
				{
					string[] backgroundData = data[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

					for (int j = 1; j < backgroundData.Length; j++)
					{
						background.Add(backgroundData[j]);
					}
				}
				else if (data[i].StartsWith("Objects"))
				{
					string[] objectsData = data[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

					for (int j = 1; j < objectsData.Length; j++)
					{
						Objects.Add(new Objects(objectsData[j]));
					}
				}
				else if (data[i].StartsWith("Rules"))
				{
					string[] rulesData = data[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

					for (int j = 1; j < rulesData.Length; j++)
					{
						Rules.Add(rulesData[j]);
					}
				}
			}

			height = background.Count;
			width = 0;
			foreach (var line in background)
			{
				if (line.Length > width)
				{
					width = line.Length;
				}
			}

			size = Math.Max(width, height);
			this.background = new char[size, size];
			for (int x = 0; x < size; x++)
			{
				for (int y = 0; y < size; y++)
				{
					if (x < width && y < height)
					{
						this.background[x, y] = background[height-y-1][x];
					}
					else
					{
						this.background[x, y] = 'b';
					}
			}
			}
		}

		public int GetWidth()
		{
			return width;
		}

		public int GetHeight()
		{
			return height;
		}

		public char GetBackground(int x, int y)
		{
			return background[x, y];
		}

		public void Transpose()
		{
		    for (int x = 0; x < size; x++)
			{
				for (int y = x; y < size; y++)
				{ 
					char c = background[x, y];
					background[x, y] = background[y, x];
					background[y, x] = c;
				}
			}

			for (int i = 0; i < Entries.Count; i++)
			{
				Entries[i].Position.Set(Entries[i].Position.y, Entries[i].Position.x);

				if (Entries[i].Direction == 'n')
				{
					Entries[i].Direction = 'w';
				}
				else if (Entries[i].Direction == 'w')
				{
					Entries[i].Direction = 'n';
				}
				else if (Entries[i].Direction == 's')
				{
					Entries[i].Direction = 'e';
				}
				else if (Entries[i].Direction == 'e')
				{
					Entries[i].Direction = 's';
				}
			}

			for (int i = 0; i < Exits.Count; i++)
			{
				Exits[i].Position.Set(Exits[i].Position.y, Exits[i].Position.x);

				if (Exits[i].Direction == 'n')
				{
					Exits[i].Direction = 'w';
				}
				else if (Exits[i].Direction == 'w')
				{
					Exits[i].Direction = 'n';
				}
				else if (Exits[i].Direction == 's')
				{
					Exits[i].Direction = 'e';
				}
				else if (Exits[i].Direction == 'e')
				{
					Exits[i].Direction = 's';
				}
			}

			for (int i = 0; i < Objects.Count; i++)
			{
				Objects[i].Position.Set(Objects[i].Position.y, Objects[i].Position.x);
			}

			int w = width;
			width = height;
			height = w;
		}

		public void FlipHorizontal()
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height/2; y++)
				{
					char temp = background[x,y];
					background[x,y] = background[x,height - y - 1];
					background[x,height - y - 1] = temp;
				}
			}

			for (int i = 0; i < Entries.Count; i++)
			{
				Entries[i].Position.Set(Entries[i].Position.x, height - Entries[i].Position.y - 1);
				if (Entries[i].Direction == 'n')
				{
					Entries[i].Direction = 's';
				}
				else if (Entries[i].Direction == 's')
				{
					Entries[i].Direction = 'n';
				}
			}

			for (int i = 0; i < Exits.Count; i++)
			{
				Exits[i].Position.Set(Exits[i].Position.x, height - Exits[i].Position.y - 1);
				if (Entries[i].Direction == 'n')
				{
					Entries[i].Direction = 's';
				}
				else if (Entries[i].Direction == 's')
				{
					Entries[i].Direction = 'n';
				}
			}

			for (int i = 0; i < Objects.Count; i++)
			{
				Objects[i].Position.Set(Objects[i].Position.x, height - Objects[i].Position.y - 1);
			}
		}

		public void FlipVertical()
		{
			for (int x = 0; x < width/2; x++)
			{
				for (int y = 0; y < height; y++)
				{
					char temp = background[x,y];
					background[x,y] = background[width - x - 1,y];
					background[width - x - 1,y] = temp;
				}
			}

			for (int i = 0; i < Entries.Count; i++)
			{
				Entries[i].Position.Set(width - Entries[i].Position.x - 1, Entries[i].Position.y);
				if (Entries[i].Direction == 'w')
				{
					Entries[i].Direction = 'e';
				}
				else if (Entries[i].Direction == 'e')
				{
					Entries[i].Direction = 'w';
				}
			}

			for (int i = 0; i < Exits.Count; i++)
			{
				Exits[i].Position.Set(width - Exits[i].Position.x - 1, Exits[i].Position.y);
				if (Entries[i].Direction == 'w')
				{
					Entries[i].Direction = 'e';
				}
				else if (Entries[i].Direction == 'e')
				{
					Entries[i].Direction = 'w';
				}
			}

			for (int i = 0; i < Objects.Count; i++)
			{
				Objects[i].Position.Set(width - Objects[i].Position.x - 1, Objects[i].Position.y);
			}
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

		StringBuilder sb = new StringBuilder();

		curRoom = "";
	}

	public string curRoom;

	public void Update()
	{
		curRoom = ActiveRoom();
	}

	public void LoadLevel()
	{
		rooms = new Dictionary<string, Room>();
		rooms.Add("Outside", new Room("Outside"));
		roomNames = new List<string>();
		roomPositions = new Dictionary<Vector4, string>();
		roomLocations = new Dictionary<string, Vector4>();
		levelInfo = new Dictionary<string, string>();
		usedRooms = new List<string>();

		Legend = new Dictionary<char, string>();

		string[] data = LevelData.text.Split(new string[] { "\n- " }, StringSplitOptions.RemoveEmptyEntries);

		Name = data[0];

		for (int i = 1; i < data.Length; i++)
		{
			if (data[i].StartsWith("Legend"))
			{
				string[] legendData = data[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

				for (int j = 1; j<legendData.Length; j++)
				{
					Legend.Add(legendData[j][0], legendData[j].Split(' ')[1]);
				}
			}
			else if (data[i].StartsWith("Rooms"))
			{
				string[] roomData = data[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

				for (int j = 1; j<roomData.Length; j++)
				{
					Room room = new Room(roomData[j]);
					roomNames.Add(room.Name);
					rooms.Add(room.Name, room);
				}
			}
			else if (data[i].StartsWith("Level"))
			{
				string[] levelData = data[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

				for (int j = 1; j < levelData.Length; j++)
				{
					levelInfo.Add(levelData[j].Split(' ')[0], levelData[j].Split(' ')[1]);
				}
			}
		}

		Height = int.Parse(levelInfo["Height"]);
		Width = int.Parse(levelInfo["Width"]);

		background = new char[Width][];
		for (int i = 0; i < Width; i++)
		{
			background[i] = new char[Height];
			for (int j = 0; j < Height; j++)
			{
				background[i][j] = ' ';
			}
		}
	}

	public void CreateMap()
	{
		int roomNumber = int.Parse (levelInfo["RoomNumber"]);

		usedRooms.Add(levelInfo["StartRoom"]);

		while (usedRooms.Count < roomNumber) {
			int random = Random.Range(0, roomNames.Count);
			if (!usedRooms.Contains(roomNames[random])) {
				usedRooms.Add(roomNames[random]);
			}
		}

		Stack<Doors> exits = new Stack<Doors>();

		CreateRoom(int.Parse(levelInfo["StartRoomX"]), int.Parse(levelInfo["StartRoomX"]), usedRooms[0]);

		foreach (var exit in rooms[usedRooms[0]].Exits)
		{
			exits.Push(exit);
		}

		for (int i = 1; i < roomNumber; i++)
		{
			Room room = rooms[usedRooms[i]];
			int door = Random.Range(0, room.Entries.Count);
			Doors entry = room.Entries[door];
			Doors exit = exits.Pop();

			switch (exit.Direction)
			{
				case 'n':
					switch (entry.Direction)
					{
						case 'n':
							room.FlipHorizontal();
							break;
						case 'w':
							room.Transpose();
							break;
						case 's':
							break;
						case 'e':
							room.Transpose();
							room.FlipHorizontal();
							break;
					}
					if (Random.value < 0.5)
					{
						room.FlipVertical();
					}
					break;
				case 'w':
					switch (entry.Direction)
					{
						case 'n':
							room.Transpose();
							break;
						case 'w':
							room.FlipVertical();
							break;
						case 's':
							room.Transpose();
							room.FlipVertical();
							break;
						case 'e':
							break;
					}
					if (Random.value < 0.5)
					{
						room.FlipHorizontal();
					}
					break;
				case 's':
					switch (entry.Direction)
					{
						case 'n':
							break;
						case 'w':
							room.Transpose();
							room.FlipHorizontal();
							break;
						case 's':
							room.FlipHorizontal();
							break;
						case 'e':
							room.Transpose();
							break;
					}
					if (Random.value < 0.5)
					{
						room.FlipVertical();
					}
					break;
				case 'e':
					switch (entry.Direction)
					{
						case 'n':
							room.Transpose();
							room.FlipVertical();
							break;
						case 'w':
							break;
						case 's':
							room.Transpose();
							break;
						case 'e':
							room.FlipVertical();
							break;
					}
					if (Random.value < 0.5)
					{
						room.FlipHorizontal();
					}
					break;
				}

				int x = 0;
				int y = 0;

				if (exit.Direction == 'n')
				{
					x = (int)exit.Position.x - (int)entry.Position.x;
					y = (int)exit.Position.y + int.Parse(levelInfo["RoomDistance"]) + (int)entry.Position.y + 1;
				}
				else if (exit.Direction == 'w')
				{
					x = (int)exit.Position.x - int.Parse(levelInfo["RoomDistance"]) - (int)entry.Position.x - 1;
					y = (int)exit.Position.y - (int)entry.Position.y;
				}
				else if (exit.Direction == 's')
				{
					x = (int)exit.Position.x - (int)entry.Position.x;
					y = (int)exit.Position.y - int.Parse(levelInfo["RoomDistance"]) - (int)entry.Position.y - 1;
				}
				else if (exit.Direction == 'e')
				{
					x = (int)exit.Position.x + int.Parse(levelInfo["RoomDistance"]) - (int)entry.Position.x + 1;
					y = (int)exit.Position.y - (int)entry.Position.y;
				}

				CreateRoom(x , y, usedRooms[i]);
		    }

		CreateBackground();

		foreach (var room in usedRooms)
		{
			LoadRoom(room);
		}

	}

	private void CreateBackground()
	{
		Transform bgTransform;

		if (!transform.FindChild("Background"))
		{
			GameObject bg = new GameObject();
			bg.name = "Background";
			bg.transform.parent = transform;
			bgTransform = bg.transform;
		}
		else
		{
			bgTransform = transform.FindChild("Background");
		}

		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				GameObject tile;

				char c = background[x][y];

				if (c != ' ')
				{
					tile = Load(c, rooms[RoomAt(x, y)]);
					if (tile != null)
					{
						Vector3 pos = new Vector3(x, y, 0.3f);
						GameObject test = (GameObject) Instantiate(tile, pos, bgTransform.rotation);
						test.transform.parent = bgTransform;
					}
				}
			}
		}
	}

	private GameObject Load(char c, Room room)
	{
		if (c == 0)
		{
			return null;
		}

		if (room.Legend.ContainsKey(c))
		{
			return (GameObject)Resources.Load(room.Legend[c]);
		}
		if (Legend.ContainsKey(c))
		{
			return (GameObject)Resources.Load(Legend[c]);
		}

		Debug.Log(c+" Not Found in: " + room.Name);
		return null;
	}

	public void CreateRoom(int x, int y, string roomName)
	{
		Room room = rooms[roomName];

		
		for (int i = x; i < room.GetWidth() + x; i++)
		{
			for (int j = y; j < room.GetHeight() + y; j++)
			{
				if (room.GetBackground(i - x, j - y) != ' ')
				{
					background[i][j] = room.GetBackground(i - x, j - y);
				}
			}
		}

		roomPositions.Add(new Vector4 (x, y, room.GetWidth() + x, room.GetHeight() + y), room.Name);
		roomLocations.Add(room.Name, new Vector4 (x, y, room.GetWidth() + x, room.GetHeight() + y));

		Debug.Log("Created: "+room.Name+" at: "+ x+ ":"+ y+"::" +(room.GetWidth() + x )+ ":" + (room.GetHeight() + y));

	    for (int i = 0; i < room.Exits.Count; i++)
	    {
	        room.Exits[i].Position.Set(room.Exits[i].Position.x + x, room.Exits[i].Position.y + y);
	    }
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

		
		for (int i = 0; i < room.Objects.Count; i++)
		{
		    char c = room.Objects[i].Prefab;
			Vector3 pos = new Vector3(room.Objects[i].Position.x + X, room.Objects[i].Position.y + Y, 0);

			if (c == 'p')
			{
				Player.SetPosition(new Vector3(pos.x, pos.y, -1));
			}
			else
			{
				GameObject ob = Load(room.Objects[i].Prefab, room);
				if (ob != null)
				{
					GameObject ob2 = (GameObject)Instantiate(ob, pos, transform.rotation);
					ob2.transform.name = room.Objects[i].Name; ;
					ob2.transform.parent = this.transform.FindChild(roomName);
				}
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

		return RoomAt(pos.x, pos.y);
	}

	public string RoomAt(float x, float y)
	{
		foreach (var room in roomPositions.Keys)
		{
			if (room.x <= x && x < room.z)
			{
				if (room.y <= y && y < room.w)
					return roomPositions[room];
			}
		}

		return "Outside";
	}
}
