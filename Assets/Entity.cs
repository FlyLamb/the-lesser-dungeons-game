using UnityEngine;

public class Entity : MonoBehaviour {

    public float health;
    public Room roomIn;

    public virtual void OnRegister() {

    }

    public virtual void Damage(float damage) {
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        Destroy(gameObject);
    }

}
