using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupable : MonoBehaviour
{
    private MeshRenderer rdr;

    private void Start() {
        rdr = GetComponent<MeshRenderer>();
    }

    private void Update() {
        rdr.material.EnableKeyword("_EMISSION");
        rdr.material.SetColor("_EmissionColor", Color.white * Mathf.LinearToGammaSpace((Mathf.Sin(Time.time * 1.5f) + .5f) / 2));
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            Player.instance.ammo += Mathf.RoundToInt(Random.Range(50, 150 * Player.instance.md_luck));
            Destroy(gameObject);
        }
    }
}
