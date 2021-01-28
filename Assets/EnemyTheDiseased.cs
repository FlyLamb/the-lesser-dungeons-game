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
        GetComponent<ModelAnimator>().Play(0);
    }

    private void Update() {
        UpdateHealthDisplay();
        agent.SetDestination(Player.instance.transform.position);

        if (agent.remainingDistance < attackDistance && delay <= 0) {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce((transform.forward * throwForce.x + transform.up * throwForce.y) * Random.Range(0.8f,1.3f));
            bullet.GetComponent<Bullet>().EnemyBullet();
            delay = mDelay;
        }

        if(agent.remainingDistance < agent.stoppingDistance + 1) {
            transform.LookAt(Player.instance.transform.position);
        }

        if (delay >= 0) {
            delay -= Time.deltaTime;
        }
    }

    public override void Damage(float damage, DamageType type = DamageType.normal) {
        if(type != DamageType.poison)
            base.Damage(damage, type);
    }
}
