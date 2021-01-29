using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Game/Loot Table", fileName = "New LT")]
public class LootTable : ScriptableObject {
    public List<LootItem> lootItems;

    public Weapon GetWeaponForRng(float nmb) {
        List<Weapon> wpns = new List<Weapon>();

        foreach(LootItem i in lootItems) {
            if(i.tierFrom >= nmb && i.tierTo <= nmb) {
                wpns.Add(i.weapon);
            }
        }

        return wpns[Random.Range(0, wpns.Count)];
    }
}

[Serializable]
public class LootItem {
    public float tierFrom, tierTo;
    public Weapon weapon;
}
