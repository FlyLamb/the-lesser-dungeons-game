using UnityEngine;
using UnityEngine.AI;

public class EnemyTheDiseased : HostileEnemy {
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
        agent.SetDestination(Player.instance.transform.position);

        if (agent.remainingDistance < attackDistance && delay <= 0) {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce.x + transform.up * throwForce.y);
            bullet.GetComponent<Bullet>().EnemyBullet();
            delay = mDelay;
        }

        if (delay >= 0) {
            delay -= Time.deltaTime;
        }
    }
}
