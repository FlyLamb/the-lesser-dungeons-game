using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpider : HostileEnemy {
    private NavMeshAgent agent;
    private ModelAnimator animator;

    public float tmJump;
    public float damage;

    public bool entered;
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<ModelAnimator>();
        StartCoroutine(GoToPointNearPlayer());
    }

    private void Update() {
        UpdateHealthDisplay();
        if (Vector3.Distance(Player.instance.transform.position, transform.position) < 0.4f) {
            if(!entered)
                Player.instance.Damage(damage);
            entered = true;
        } else {
            entered = false;
        }

        if (agent.remainingDistance <= 0.2f) {
            if (animator.currentAnimation.name != "Stop") {
                animator.Play("Stop");
            } 
        } else if (animator.currentAnimation.name != "Walk") {
            animator.Play("Walk");
        }
    }

    private IEnumerator GoToPointNearPlayer() {
        while (true) {
            agent.SetDestination(Player.instance.transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f)));
            yield return new WaitForSeconds(tmJump);
        }
    }
}
