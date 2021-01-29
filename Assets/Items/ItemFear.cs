using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item/Fear")]
public class ItemFear : Item
{
    public override void ApplyAddStats() {
        Player.instance.md_speed += (1f - (Player.instance.health / Player.instance.md_maxHealth)) * 7;
    }
}
