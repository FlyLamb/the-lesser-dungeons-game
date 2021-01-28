using System.Collections;
using UnityEngine;

public class RoomGenerator : MonoBehaviour {

    public static RoomGenerator generator;

    private void Awake() {
        generator = this;
    }

    public Room[,] mapLayout;

    public int MAP_SIZE = 5;
    public int ITERATION_AMOUNT = 8;
    public int DIR_CHANCE = 8;
    public int DOOR_SPAWN_CHANCE = 50;
    public int MIN_ROOM_AM = 2;

    public GameObject roomPrefab;
    
    public Vector2 roomSize = new Vector2(2, 2);

    private void Clear() { //Destroys all existing rooms
        if (mapLayout == null) {
            return;
        }

        for (int i = 0; i < MAP_SIZE; i++) {
            for (int j = 0; j < MAP_SIZE; j++) {
                if (mapLayout[i, j] != null) {
                    Destroy(mapLayout[i, j].gameObject);
                }
            }
        }
    }

    [ContextMenu("Generate new")]
    public void Generate() {
        Clear();
        mapLayout = new Room[MAP_SIZE, MAP_SIZE];
        while (GetRoomAmount() <= MIN_ROOM_AM) {
            Debug.Log("Running generation task."); 
            Clear();

            mapLayout = new Room[MAP_SIZE, MAP_SIZE];
            Populate(MAP_SIZE / 2, MAP_SIZE / 2);

            for (int i = 0; i < ITERATION_AMOUNT; i++) {
                IterateGeneration();
                //yield return new WaitForSeconds(0.1f);
            }

            for (int i = 0; i < MAP_SIZE; i++) { //Spawn doors
                for (int j = 0; j < MAP_SIZE; j++) {
                    Doors(i, j);   
                }
            }

            //yield return new WaitForSeconds(0.1f);
            PthRooms(MAP_SIZE / 2, MAP_SIZE / 2);


            for (int i = 0; i < MAP_SIZE; i++) { //Clears rooms that are not accessible and generates the insides of those which are
                for (int j = 0; j < MAP_SIZE; j++) {
                    if (mapLayout[i, j] != null && !mapLayout[i,j].IsAccessible()) {
                        //yield return new WaitForSeconds(0.2f);
                        Destroy(mapLayout[i, j].gameObject);
                        mapLayout[i, j] = null;
                    }
                }
            }

            

            Debug.Log($"Finished generation task; Map consists of {GetRoomAmount()} rooms");
        }

        for (int i = 0; i < MAP_SIZE; i++) { 
            for (int j = 0; j < MAP_SIZE; j++) {
                if (mapLayout[i, j] != null) {
                    mapLayout[i, j].Setup();
                    mapLayout[i, j].GenerateRoomContent();
                }
            }
        }

        for (int i = 0; i < MAP_SIZE; i++) { 
            for (int j = 0; j < MAP_SIZE; j++) {
                if (mapLayout[i, j] != null) {
                    mapLayout[i, j].UpdateDoors();
                }
            }
        }
    }


    private void PthRooms(int x, int y) { //Recursive function to check the available paths from a room.

        if (!(x >= 0 && x < MAP_SIZE && y >= 0 && y < MAP_SIZE)) return;
        if (mapLayout[x, y] == null) return;
        if (mapLayout[x, y].IsAccessible()) return;

        mapLayout[x, y].MarkAccessible();
        if (mapLayout[x,y].s) 
             PthRooms(x, y - 1);
        if (mapLayout[x, y].n)
            PthRooms(x, y + 1);
        if (mapLayout[x, y].e)
            PthRooms(x + 1, y);
        if (mapLayout[x, y].w)
            PthRooms(x - 1, y);
    }

    public void IterateGeneration() { //Randomly spreads all the rooms
        for (int i = 0; i < MAP_SIZE; i++) {
            for (int j = 0; j < MAP_SIZE; j++) {
                if (mapLayout[i, j] != null) {
                    int dir = Random.Range(0, DIR_CHANCE);
                    switch (dir) {
                        case 0:
                            Populate(i, j + 1);
                            break;
                        case 1:
                            Populate(i, j - 1);
                            break;
                        case 2:
                            Populate(i + 1, j);
                            break;
                        case 3:
                            Populate(i - 1, j);
                            break;
                    }
                }
            }
        }
    }

    private void Populate(int x, int y) {
        if (!(x >= 0 && x < MAP_SIZE && y >= 0 && y < MAP_SIZE)) {
            return;
        }

        if (mapLayout[x, y] != null) {
            return;
        }

        mapLayout[x, y] = Instantiate(roomPrefab, new Vector3(x * roomSize.x, 0, y * roomSize.y), Quaternion.identity).GetComponent<Room>();
        mapLayout[x, y].myPosition = new Vector2Int(x,y);
    }

    private void Doors(int x, int y) { //Spawns doors for a room
        if (!(x >= 0 && x < MAP_SIZE && y >= 0 && y < MAP_SIZE && mapLayout[x, y] != null)) {
            return;
        }

        if (x + 1 < MAP_SIZE && mapLayout[x + 1, y] != null && Random.Range(0, 100) >= 100 - DOOR_SPAWN_CHANCE) {
            mapLayout[x, y].e = mapLayout[x + 1, y].w = true;
        }
        if (x - 1 >= 0 && mapLayout[x - 1, y] != null && Random.Range(0, 100) >= 100 - DOOR_SPAWN_CHANCE) {
            mapLayout[x, y].w = mapLayout[x - 1, y].e = true;
        }
        if (y + 1 < MAP_SIZE && mapLayout[x, y + 1] != null && Random.Range(0, 100) >= 100 - DOOR_SPAWN_CHANCE) {
            mapLayout[x, y].n = mapLayout[x, y + 1].s = true;
        }
        if (y - 1 >= 0 && mapLayout[x, y - 1] != null && Random.Range(0, 100) >= 100 - DOOR_SPAWN_CHANCE) {
            mapLayout[x, y].s = mapLayout[x, y - 1].n = true;
        }
    }

    public int GetRoomAmount() { //Returns the room count
        if (mapLayout == null) {
            return 0;
        }

        int count = 0;
        foreach (Room i in mapLayout) {
            if (i != null) {
                count++;
            }
        }

        return count;
    }
}
