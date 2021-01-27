using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public bool n, e, s, w;
    private bool isAccessible = false;

    public Vector2Int myPosition;
    public GameObject doorPrefab;
    public Door doorN, doorE, doorS, doorW;


    public List<GameObject> go;
    public float enemy = 0;

    protected GameObject[,] objTable;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        if (e) {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(2, 0, 0));
        }

        if (w) {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(-2, 0, 0));
        }

        if (n) {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 0, 2));
        }

        if (s) {
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 0, -2));
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

    private void Awake() {
        go = new List<GameObject>();
    }

    public void GenerateDoors() {
        if (n) {
            doorN = Instantiate(doorPrefab, transform.position + new Vector3(0, 0, 4), Quaternion.Euler(0, 0, 0)).GetComponent<Door>();
            doorN.leadsTo = myPosition + new Vector2Int(0, 1);
            doorN.roomIn = this;
        }
        if (s) {
            doorS = Instantiate(doorPrefab, transform.position + new Vector3(0, 0, -4), Quaternion.Euler(0, 180, 0)).GetComponent<Door>();
            doorS.leadsTo = myPosition + new Vector2Int(0, -1);
            doorS.roomIn = this;
        }
        if (e) {
            doorE = Instantiate(doorPrefab, transform.position + new Vector3(6, 0, 0), Quaternion.Euler(0, 90, 0)).GetComponent<Door>();
            doorE.leadsTo = myPosition + new Vector2Int(1, 0);
            doorE.roomIn = this;
        }
        if (w) {
            doorW = Instantiate(doorPrefab, transform.position + new Vector3(-6, 0, 0), Quaternion.Euler(0, 270, 0)).GetComponent<Door>();
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
        objTable = new GameObject[12, 8];
        for (int x = -6; x < 6; x++) {
            for (int y = -4; y < 4; y++) {
                objTable[x+6,y+4] = GenCell(x, y, transform.position + new Vector3(x+0.5f,0,y + 0.5f),Random.Range(0,1000f));
                go.Add(objTable[x + 6, y + 4]);
            }
        }

        if(s) {
            Destroy(objTable[5, 0],0.1f);
            Destroy(objTable[6, 0], 0.1f);
        }

        if (n) {
            Destroy(objTable[5, 7], 0.1f);
            Destroy(objTable[6, 7], 0.1f);
        }

        if(e) {
            Destroy(objTable[0, 3], 0.1f);
            Destroy(objTable[0, 4], 0.1f);
        }

        if (w) {
            Destroy(objTable[11, 3]);
            Destroy(objTable[11, 4]);
        }

        
    }

    public virtual GameObject GenCell(int x, int y, Vector3 ePos, float seed) {
        return null;
    }

    public virtual void OnPlayerEnter(Player p) {
        foreach (var item in go) {
            if (item != null) {
                item.SetActive(true);
            }
        }
    }

    public virtual void OnPlayerLeave(Player p) {
        foreach (var item in go) {
            if (item != null) {
                item.SetActive(false);
            }
        }
    }

    public GameObject AddEntity(Entity e, Vector3 position, Quaternion rotation) {
        GameObject g = Instantiate(e.gameObject, position, rotation);
        g.GetComponent<Entity>().roomIn = this;
        g.GetComponent<Entity>().OnRegister();
        go.Add(g);
        g.SetActive(false);
        return g;
    }
}
