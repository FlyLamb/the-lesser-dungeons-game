using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public float health;
    public Room roomIn;

    private void Update() {
        GetComponent<NavMeshAgent>().destination = Player.instance.transform.position;
    }

    public void Damage(float damage) {
        health -= damage;

        if(health <= 0) {
            Die();
        }
    }

    public void Die() {
        roomIn.enemy--;
        Destroy(gameObject);
    }
}
