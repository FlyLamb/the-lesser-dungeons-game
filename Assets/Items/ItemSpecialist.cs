using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item/Specialist")]
public class ItemSpecialist : Item
{
    public override void ApplyAddStats() {
        Player.instance.md_poisonResistance += (1-(Player.instance.md_baseResistance / 20)) * 20;
        Player.instance.md_fireResistance += (1 - (Player.instance.md_baseResistance / 20)) * 20;
    }
}
