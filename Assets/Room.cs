using UnityEngine;

public class Room : MonoBehaviour {
    public bool n, e, s, w;
    private bool isAccessible = false;

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

    public virtual void GenerateRoomContent() {

    }

}
