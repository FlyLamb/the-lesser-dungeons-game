using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupable : MonoBehaviour
{
    public Item item;

    private GameObject mdl;
    private MeshRenderer rdr;



    private void Update() {
        if (mdl == null && item != null) {
            mdl = Instantiate(item.model, transform.GetChild(0));
            rdr = mdl.GetComponentInChildren<MeshRenderer>();
        }

        if (mdl != null) {
            rdr.material.EnableKeyword("_EMISSION");
            rdr.material.SetColor("_EmissionColor", Color.white * Mathf.LinearToGammaSpace((Mathf.Sin(Time.time * 1.5f) + .5f) / 2));
        }
    }

    private void OnTriggerStay(Collider other) {
        if (!other.CompareTag("Player")) return;
        if(Player.instance.interactHeld) {
            Player.instance.items.Add(item);
            Destroy(gameObject);
        }
    }
}
