using UnityEngine;

public class Crate : Enemy {
    public GameObject broken;
    public GameObject nonbroken;
    public override void Die() {
        Destroy(GetComponent<Collider>());
        broken.SetActive(true);
        nonbroken.SetActive(false);
        Destroy(gameObject,5);
    }
}
