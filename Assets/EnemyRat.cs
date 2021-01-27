using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRat : HostileEnemy {

    private NavMeshAgent agent;

    public float attack = 10;
    public float damage;

    private Crate nest;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        GetComponent<ModelAnimator>().Play(0);
        FindNest();
    }

    private void FindNest() {
        List<Crate> crates = new List<Crate>();
        int i = 0;
        while (nest == null && i < 5) {
            foreach (GameObject g in roomIn.go) {
                if (g != null && g.GetComponent<Crate>() != null) {
                    crates.Add(g.GetComponent<Crate>());
                }
            }
            i++;
            nest = crates[Random.Range(0, crates.Count - 1)];
            if (nest.wasDestroyed) nest = null;
        }
    }

    public override void Damage(float damage) {
        base.Damage(damage);
        attack = 0;
    }

    private void Update() {

        if (!gameObject.activeSelf) return;

        if (attack <= 0) {
            agent.SetDestination(Player.instance.transform.position);
            if(Vector3.Distance(Player.instance.transform.position,transform.position) < 0.5f) {
                attack = Random.Range(4f,6f);
                Player.instance.Damage(damage);
            }
        } else {
            if (nest == null) FindNest();
            if (nest.wasDestroyed) FindNest();
            agent.SetDestination(nest.transform.position);
            attack -= Time.deltaTime;
            float dst = Vector3.Distance(nest.transform.position, transform.position);
            if (dst < 1.1f) nest.AddRat(this);
        }

        
    }
}
