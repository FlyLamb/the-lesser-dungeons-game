using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float aliveTime;
    public float damage;
    public Entity.DamageType dmgType;

    private void Start() {
        StartCoroutine(Life());
    }

    protected virtual IEnumerator Life() {
        yield return new WaitForSeconds(aliveTime);
        Destroy(gameObject);
    }

    public void EnemyBullet() {
        gameObject.layer = 9;
    }


    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.gameObject.CompareTag("Enemy")) {
            collision.collider.GetComponent<Entity>().Damage(damage,dmgType);
            Destroy(gameObject);
        } else
        if (collision.collider.gameObject.CompareTag("Player")) {
            collision.collider.GetComponent<Player>().Damage(damage, dmgType);
            Destroy(gameObject);
        } 

        
    }
}
