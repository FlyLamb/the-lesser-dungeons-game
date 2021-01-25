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
        if (!other.gameObject.CompareTag("Player")) return;
        if(!locked) {
            Vector2 moveDirection = (Vector2)(roomIn.myPosition - leadsTo);
            Player.instance.transform.position = otherSide.transform.position + new Vector3(-moveDirection.x, Player.instance.transform.position.y, -moveDirection.y);
            Player.instance.current = RoomGenerator.generator.mapLayout[leadsTo.x, leadsTo.y];
            RoomGenerator.generator.mapLayout[leadsTo.x, leadsTo.y].OnPlayerEnter(Player.instance);
        }
    }
}
