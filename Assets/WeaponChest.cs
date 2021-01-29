using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChest : Entity {

    public float tierBase;

    public List<LootTable> possibleDrops;

    private GameObject wt;

    private bool used = false;

    private void Start() {
        wt = transform.GetChild(0).gameObject;
        wt.SetActive(false);
    }

    private Weapon GetRandomWeapon() {
        return possibleDrops[Random.Range(0, possibleDrops.Count)].GetWeaponForRng(tierBase + Player.instance.md_luck / 100);
    }


    private void OnTriggerStay(Collider other) {
        if (used) return;
        if (Player.instance.interactHeld) {
            Player.instance.interactHeld = false;
            GetComponent<ModelAnimator>().Play(0);
            StartCoroutine("Particle");
            used = true;
        }
    }

    private IEnumerator Particle() {
        yield return new WaitForSeconds(0.4f);
        wt.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        GameObject d = Instantiate(Statics.instance.dropWeaponPrefab, transform.position + new Vector3(Random.Range(-.6f, .6f), 1f, Random.Range(-.6f, .6f)), Quaternion.identity,transform);
        
        
        d.GetComponent<WeaponPickupable>().weapon = GetRandomWeapon();
       
    }
}
