using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private void Update() {
        GetComponent<NavMeshAgent>().destination = Player.instance.transform.position;
    }
}
