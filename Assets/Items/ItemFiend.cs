using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item/Fiend")]
public class ItemFiend : Item
{
    public override void ApplyAddStats() {
        Player.instance.md_fireResistance += Mathf.Clamp(15 - Player.instance.md_poisonResistance, 0, 15);
    }
}
