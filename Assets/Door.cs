using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2Int leadsTo;
    public Door otherSide;
    public Room roomIn;
    public bool locked;

    private void OnTriggerEnter(Collider other) {
        if(!locked) {
            Player.instance.transform.position = otherSide.transform.position + (Vector3)(Vector2)(leadsTo - roomIn.myPosition);
            Player.instance.current = RoomGenerator.generator.mapLayout[leadsTo.x, leadsTo.y];
        }
    }
}
