using System.Collections;
using UnityEngine;

public class PotionEffectCloud : MonoBehaviour {
    public float preDissapear;
    public float damageWait = 1;
    public float damage = 5;
    public float desiredSize = 1f;
    public Entity.DamageType damageType;

    private void Start() {
        transform.localScale = Vector3.one * 0.1f;
        
        StartCoroutine(CloudLife());
        StartCoroutine(Damage());
        transform.position += new Vector3(0, Random.Range(0, 1000f) / 100000f, 0);
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player"))
        other.gameObject.GetComponent<Player>().Effect(damageType, damageWait * 2f);
    }

    private IEnumerator Damage() {
        while(true) {
            yield return new WaitForSeconds(damageWait);
            Collider[] cs = Physics.OverlapSphere(transform.position, transform.localScale.x / 2f);
            foreach(Collider c in cs) {
                if (c.GetComponent<Entity>() != null)
                    c.GetComponent<Entity>().Damage(damage, damageType);
                if (c.GetComponent<Player>() != null) {
                    c.GetComponent<Player>().Damage(damage, damageType);
                    
                }
            }
        }
    }

    private IEnumerator CloudLife() {

        while (transform.localScale.magnitude < desiredSize) {
            transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(preDissapear);

        while (transform.localScale.magnitude > 0.1) {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
