using UnityEngine;
using UnityEngine.AI;

public class EnemySlime : HostileEnemy {
    private NavMeshAgent agent;
    public GameObject poisonSplatterPrefab;

    private Vector3 pPos;
    private float travelled = 0;
    
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        GetComponent<ModelAnimator>().Play(0);
    }

    private void Update() {
        UpdateHealthDisplay();
        travelled += Vector3.Distance(transform.position, pPos);

        if(travelled >= 2) {
            GameObject g = Instantiate(poisonSplatterPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
            g.transform.localScale *= .5f;
            g.GetComponent<PotionEffectCloud>().preDissapear = 6;
            travelled = 0;
        }

        agent.SetDestination(Player.instance.transform.position);
        pPos = transform.position;
    }
}
