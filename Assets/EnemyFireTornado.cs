using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFireTornado : HostileEnemy
{
    private NavMeshAgent agent;
    
    public float damage;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    private void Update() {
        UpdateHealthDisplay();
        agent.SetDestination(Player.instance.transform.position);
        if(Vector3.Distance(Player.instance.transform.position,transform.position) < 4) {
            Player.instance.transform.position += Vector3.ClampMagnitude(transform.position - Player.instance.transform.position,2) * Time.deltaTime;
        }

        if(Vector3.Distance(Player.instance.transform.position,transform.position) <= 1.5f) {
            Player.instance.Damage(damage * Time.deltaTime, Entity.DamageType.fire);
        }

        transform.GetChild(0).Rotate(new Vector3(0, Time.deltaTime * 360, 0));
    }

    public override void Damage(float damage, DamageType type = DamageType.normal) {
        if (type != DamageType.fire)
            base.Damage(damage, type);
    }
}
