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
        public readonly List<string> Background;
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
                if (data[i].StartsWith("Doors"))
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

    private Dictionary<string, string> levelInfo; 

    public void Awake()
    {
        LoadLevel();
        CreateMap();
    }

    public void LoadLevel()
    {
        rooms = new Dictionary<string, Room>();
        roomNames = new List<string>();
        roomPositions = new Dictionary<Vector4, string>();
        levelInfo = new Dictionary<string, string>();
        usedRooms = new List<string>();

        Legend = new Dictionary<char, string>();

        string[] data = LevelData.text.Split(new string[] { "\n- " }, StringSplitOptions.RemoveEmptyEntries);

        Name = data[0];

        for (int i = 1; i<data.Length; i++)
        {
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
                string[] levelData = data[i].Split('\n');

                for (int j = 1; j<levelData.Length; j++)
                {
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
        int random = randomRange(0, rooms.Count);

        while (usedRooms.Count < int.Parse(levelInfo["RoomNumber"]))
        {
            if (!usedRooms.Contains(roomNames[random]))
            {
                usedRooms.Add(roomNames[random]);
            }
        }


    }

    public void CreateRoom()
    {

    }

    public void ReloadRoom()
    {
        
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
