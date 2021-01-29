using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelElevator : MonoBehaviour
{

    private void Update() {
        GetComponent<BoxCollider>().enabled = RoomGenerator.generator.enemyAmount <= 1;
        GetComponent<MeshRenderer>().enabled = RoomGenerator.generator.enemyAmount <= 1;
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        Statics.instance.level++;
       
        RoomGenerator.generator.Generate();
        Player.instance.current = RoomGenerator.generator.mapLayout[RoomGenerator.generator.MAP_SIZE / 2, RoomGenerator.generator.MAP_SIZE / 2];
        Player.instance.transform.position = new Vector3(128, 1, 96);
    }
}
