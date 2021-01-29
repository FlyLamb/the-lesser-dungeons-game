using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item/Adrenaline")]
public class ItemAdrenaline : Item
{
    public override void ApplyAddStats() {
        Player.instance.md_baseResistance += (1f - (Player.instance.health / Player.instance.md_maxHealth)) * 10;
    }
}
