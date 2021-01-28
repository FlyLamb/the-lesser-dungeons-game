using UnityEngine;

public class Entity : MonoBehaviour {
    public enum DamageType {
        normal,
        fire,
        poison,
        water
    }

    public float health;
    public Room roomIn;

    public virtual void OnRegister() {

    }

    public virtual void Damage(float damage, DamageType type = DamageType.normal) {
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        Destroy(gameObject);
    }

}
