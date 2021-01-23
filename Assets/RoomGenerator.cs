using UnityEngine;

public class RoomGenerator : MonoBehaviour {

    public Room[,] mapLayout;

    public int MAP_SIZE = 5;
    public int ITERATION_AMOUNT = 8;
    public int DIR_CHANCE = 8;

    public GameObject roomPrefab;
    public Vector2 roomSize = new Vector2(2,2);

    private void Start() {
        Generate();
    }

    public void Generate() {
        mapLayout = new Room[MAP_SIZE,MAP_SIZE];
        Populate(MAP_SIZE / 2, MAP_SIZE / 2);

        for(int i = 0; i < ITERATION_AMOUNT; i++) {
            IterateGeneration();
        }

        for (int i = 0; i < MAP_SIZE; i++) {
            for (int j = 0; j < MAP_SIZE; j++) {
                Doors(i, j);
            }
        }   
    }

    [ContextMenu("Iterate")]
    public void IterateGeneration() {
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

    public void Populate(int x, int y) {
        if (!(x >= 0 && x < MAP_SIZE && y >= 0 && y < MAP_SIZE)) return;
        if (mapLayout[x, y] != null) return;

        mapLayout[x, y] = Instantiate(roomPrefab, new Vector2(x*roomSize.x,y*roomSize.y), Quaternion.identity).GetComponent<Room>();
    }

    public void Doors(int x, int y) {
        if (!(x >= 0 && x < MAP_SIZE && y >= 0 && y < MAP_SIZE && mapLayout[x,y] != null)) return;

        if(x + 1 < MAP_SIZE && mapLayout[x + 1,y] != null) {
            mapLayout[x, y].e = mapLayout[x + 1, y].w = true;
        } if (x - 1 >= 0 && mapLayout[x - 1, y] != null) {
            mapLayout[x, y].w = mapLayout[x - 1, y].e = true;
        } if (y + 1 < MAP_SIZE && mapLayout[x, y + 1] != null) {
            mapLayout[x, y].n = mapLayout[x, y + 1].s = true;
        } if (y - 1 >= 0 && mapLayout[x, y - 1] != null) {
            mapLayout[x, y].s = mapLayout[x, y - 1].n = true;
        }
    }
}
