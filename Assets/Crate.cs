using System.Collections.Generic;
using UnityEngine;

public class Crate : Entity {
    public GameObject broken;
    public GameObject nonbroken;
    public bool wasDestroyed = false;
    public List<EnemyRat> ratsInside;

    private void Start() {
        ratsInside = new List<EnemyRat>();
    }

    public void AddRat(EnemyRat rat) {
        if (wasDestroyed) { rat.attack = 0; return; }
        ratsInside.Add(rat);
        rat.gameObject.SetActive(false);
    }

    private void Update() {
        
        foreach (EnemyRat r in ratsInside) {
            r.attack -= Time.deltaTime;
            if (r.attack <= 0) {
                ratsInside.Remove(r);
                r.gameObject.SetActive(true);
                break;
            }
        }
    }

    public override void Die() {
        wasDestroyed = true;
        foreach(EnemyRat r in ratsInside) {
            r.gameObject.SetActive(true);
            r.attack = 0;
        }
        Destroy(GetComponent<Collider>());
        broken.SetActive(true);
        nonbroken.SetActive(false);
        Destroy(gameObject,5);
    }

}
