using UnityEngine;

public class RoomGenerator : MonoBehaviour {

    public Room[,] mapLayout;
    public const int MAP_SIZE = 5;

    public void Generate() {
        mapLayout = new Room[MAP_SIZE,MAP_SIZE];
    }
}
