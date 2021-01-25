using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float aliveTime;

    private void Start() {
        StartCoroutine(Life());
    }

    private IEnumerator Life() {
        yield return new WaitForSeconds(aliveTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.gameObject.CompareTag("Enemy")) {
            //damage the enemy
        }
        Destroy(gameObject);
    }
}
