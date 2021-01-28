using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileEnemy : Entity
{
    protected float maxHealth;
    [SerializeField]
    protected HealthbarDisplay healthbar;

    public override void OnRegister() {
        roomIn.enemy++;
        maxHealth = health;
    }

    protected void UpdateHealthDisplay() {
        if(healthbar != null)
            healthbar.value = health / maxHealth;
    }

    private void OnDestroy() {
        roomIn.enemy--;
    }
}
