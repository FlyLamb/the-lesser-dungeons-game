using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public Sprite icon;
    public string itemname;
    [TextArea]
    public string description;
    public virtual void ApplyAddStats() {

    }

    public virtual void ApplyMultiplyStats() {

    }
}
