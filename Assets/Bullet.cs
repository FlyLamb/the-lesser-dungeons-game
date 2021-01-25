using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float aliveTime;
    public float damage;

    private void Start() {
        StartCoroutine(Life());
    }

    private IEnumerator Life() {
        yield return new WaitForSeconds(aliveTime);
        Destroy(gameObject);
    }

    public void EnemyBullet() {
        gameObject.layer = 7;
    }


    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.gameObject.CompareTag("Enemy")) {
            collision.collider.GetComponent<Enemy>().Damage(damage);
        }
        Destroy(gameObject);
    }
}
