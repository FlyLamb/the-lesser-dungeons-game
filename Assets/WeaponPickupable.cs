using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupable : MonoBehaviour
{
    public Weapon weapon;

    private GameObject mdl;

    private void Update() {
        if(mdl == null && weapon != null) {
            mdl = Instantiate(weapon.weaponModel, transform);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player")) {
            if(Player.instance.interactHeld) {
                Player.instance.interactHeld = false;
                Player.instance.DropWeapon();
                Player.instance.weapon = weapon;
                Destroy(gameObject);
            }
        }
    }
}
