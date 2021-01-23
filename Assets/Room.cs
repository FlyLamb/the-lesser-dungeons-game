using UnityEngine;

public class Room : MonoBehaviour {
    public bool n, e, s, w;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        
        if (e) Gizmos.DrawLine(transform.position, transform.position + new Vector3(.75f, 0, 0));
        if (w) Gizmos.DrawLine(transform.position, transform.position + new Vector3(-.75f, 0, 0));

        if (n) Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, .75f, 0));
        if (s) Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -.75f, 0));
    }

}
