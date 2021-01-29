using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupable : MonoBehaviour
{
    public Weapon weapon;

    private GameObject mdl;
    private MeshRenderer rdr;

    private void Update() {
        if(mdl == null && weapon != null) {
            mdl = Instantiate(weapon.weaponModel, transform.GetChild(0));
            rdr = mdl.GetComponentInChildren<MeshRenderer>();
        }

        if(mdl != null) {
            rdr.material.EnableKeyword("_EMISSION");
            rdr.material.SetColor("_EmissionColor", Color.white * Mathf.LinearToGammaSpace((Mathf.Sin(Time.time * 1.5f)+.5f)/2));
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player")) {
            if(Player.instance.interactHeld) {
                Player.instance.interactHeld = false;

                if (Player.instance.secondary == null)
                    Player.instance.secondary = weapon;
                else {
                    Player.instance.DropWeapon();
                    Player.instance.SetWeapon(weapon);
                }
                Destroy(gameObject);
            }
        }
    }
}
