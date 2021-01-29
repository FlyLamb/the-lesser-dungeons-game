using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item/Degree")]
public class ItemDegree : Item
{
    public override void ApplyAddStats() {
        Player.instance.md_poisonResistance += Mathf.Clamp(15 - Player.instance.md_fireResistance, 0, 15);
    }
}
