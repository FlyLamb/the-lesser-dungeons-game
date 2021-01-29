using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMage : HostileEnemy
{
    private NavMeshAgent agent;
    public float attackDistance;
    public GameObject projectile;

    public float mDelay = 2;
    private float delay = 2;

    public Vector2 throwForce;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        UpdateHealthDisplay();
        agent.SetDestination(Player.instance.transform.position);

        if (agent.remainingDistance < attackDistance && delay <= 0) {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce((Player.instance.transform.position - transform.position).normalized * throwForce.x );
            bullet.GetComponent<Bullet>().EnemyBullet();
            
            delay = mDelay;
            GetComponent<AudioSource>().Play();
        }

        if (agent.remainingDistance < agent.stoppingDistance + 1) {
            transform.LookAt(Player.instance.transform.position);
        }

        if (delay >= 0) {
            delay -= Time.deltaTime;
        }
    }

    public override void Damage(float damage, DamageType type = DamageType.normal) {
        if (type != DamageType.fire)
            base.Damage(damage, type);
    }

}
