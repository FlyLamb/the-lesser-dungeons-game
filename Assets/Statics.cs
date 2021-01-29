using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statics : MonoBehaviour
{
    public static Statics instance;
    private void Awake() {
        if(instance == null)
            instance = this;
    }


    public GameObject dropWeaponPrefab;
    public Sprite none;
    public GameObject ratParticles;
    public GameObject ammoPrefab;

    public int mainSceneId = 0;

    public int level = 0;

}
