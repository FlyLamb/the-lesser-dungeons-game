using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item/Tank")]
public class ItemTank : Item
{
    public override void ApplyAddStats() {
        Player.instance.md_baseResistance = (1f - (Player.instance.md_speed / 20)) * 20;
    }
}
