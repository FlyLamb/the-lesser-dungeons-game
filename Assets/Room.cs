using UnityEngine;

public class Room : MonoBehaviour {
    public bool n, e, s, w;
    private bool isAccessible = false;

    public Vector2Int myPosition;
    public GameObject doorPrefab;
    public Door doorN, doorE, doorS, doorW;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        if (e) {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(2, 0, 0));
        }

        if (w) {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(-2, 0, 0));
        }

        if (n) {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 2, 0));
        }

        if (s) {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -2, 0));
        }
    }

    public bool HasAnyDoors() {
        return n || e || s || w;
    }

    public void MarkAccessible() {
        isAccessible = true;
    }

    public bool IsAccessible() {
        return isAccessible;
    }

    public void GenerateDoors() {
        if (n) {
            doorN = Instantiate(doorPrefab, transform.position + new Vector3(0, 4, 0), Quaternion.Euler(0,0,0)).GetComponent<Door>();
            doorN.leadsTo = myPosition + new Vector2Int(0, 1);
            doorN.roomIn = this;
        }
        if (s) {
            doorS = Instantiate(doorPrefab, transform.position + new Vector3(0, -4, 0), Quaternion.Euler(0, 0, 180)).GetComponent<Door>();
            doorS.leadsTo = myPosition + new Vector2Int(0, -1);
            doorS.roomIn = this;
        }
        if (e) {
            doorE = Instantiate(doorPrefab, transform.position + new Vector3(6, 0, 0), Quaternion.Euler(0, 0, 90)).GetComponent<Door>();
            doorE.leadsTo = myPosition + new Vector2Int(1, 0);
            doorE.roomIn = this;
        }
        if (w) {
            doorW = Instantiate(doorPrefab, transform.position + new Vector3(-6, 0, 0), Quaternion.Euler(0, 0, 270)).GetComponent<Door>();
            doorW.leadsTo = myPosition + new Vector2Int(-1, 0);
            doorW.roomIn = this;
        }
    }

    public void UpdateDoors() {
        if (n) {
            doorN.otherSide = RoomGenerator.generator.mapLayout[myPosition.x, myPosition.y + 1].doorS;
        }

        if (s) {
            doorS.otherSide = RoomGenerator.generator.mapLayout[myPosition.x, myPosition.y - 1].doorN;
        }

        if (e) {
            doorE.otherSide = RoomGenerator.generator.mapLayout[myPosition.x + 1, myPosition.y].doorW;
        }

        if (w) {
            doorW.otherSide = RoomGenerator.generator.mapLayout[myPosition.x - 1, myPosition.y].doorE;
        }
    }


    public virtual void GenerateRoomContent() {
        GenerateDoors();
    }

}
