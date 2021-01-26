using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public Room roomIn;

    public virtual void Damage(float damage) {
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        roomIn.enemy--;
        Destroy(gameObject);
    }
}
