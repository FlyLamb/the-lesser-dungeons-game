using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item/Base")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public GameObject model;
    public string itemname;
    [TextArea]
    public string description;
    public virtual void ApplyAddStats() {

    }

    public virtual void ApplyMultiplyStats() {

    }
}
