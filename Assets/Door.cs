using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2Int leadsTo;
    public Door otherSide;
    public Room roomIn;
    public bool locked;
    public bool entered;

    public GameObject closed, caged, open;

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player")) return;
        if(!locked && roomIn.enemy <= 0) {
            Vector2 moveDirection = (Vector2)(roomIn.myPosition - leadsTo);
            Player.instance.transform.position = otherSide.transform.position + new Vector3(-moveDirection.x, Player.instance.transform.position.y, -moveDirection.y);
            Player.instance.current = RoomGenerator.generator.mapLayout[leadsTo.x, leadsTo.y];
            roomIn.OnPlayerLeave(Player.instance);
            RoomGenerator.generator.mapLayout[leadsTo.x, leadsTo.y].OnPlayerEnter(Player.instance);
            entered = true;
        }

        UpdateGraphic();
    }

    private void UpdateGraphic() {
        if (entered) {
            closed.SetActive(false);
            caged.SetActive(false);
            open.SetActive(true);
        } else {
            if (locked) {
                closed.SetActive(false);
                open.SetActive(false);
                caged.SetActive(true);
            } else {
                closed.SetActive(true);
                open.SetActive(false);
                caged.SetActive(false);
            }
        }
    }

    private void Start() {
        UpdateGraphic();
    }
}
