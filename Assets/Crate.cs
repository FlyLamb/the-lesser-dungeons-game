using System.Collections.Generic;
using UnityEngine;

public class Crate : Entity {
    public GameObject broken;
    public GameObject nonbroken;
    public bool wasDestroyed = false;
    public List<EnemyRat> ratsInside;
    public float luckM = 10;

    private GameObject bw;

    private void Start() {
        ratsInside = new List<EnemyRat>();
    }

    public void AddRat(EnemyRat rat) {
        if (wasDestroyed) { rat.attack = 0; return; }
        ratsInside.Add(rat);
        rat.gameObject.SetActive(false);
    }

    private void Update() {
        if (bw == null && ratsInside.Count > 0)
            bw = Instantiate(Statics.instance.ratParticles, transform);

        if (ratsInside.Count <= 0)
            Destroy(bw);
        


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

        if(Random.Range(0,100) < (Player.instance.md_luck) * luckM) {
            Instantiate(Statics.instance.ammoPrefab,transform.position,Quaternion.identity);
        }

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
