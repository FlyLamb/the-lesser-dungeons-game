using UnityEngine;

public class PotionBottle : Bullet {
    public GameObject potionEffectPrefab;
    private void OnCollisionEnter(Collision collision) {
        Instantiate(potionEffectPrefab, new Vector3(transform.position.x, 0, transform.position.z),Quaternion.identity);
        Destroy(gameObject);
    }
}
