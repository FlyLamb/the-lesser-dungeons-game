using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileEnemy : Entity
{
    public override void OnRegister() {
        roomIn.enemy++;
    }

    private void OnDestroy() {
        roomIn.enemy--;
    }
}
