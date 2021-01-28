using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player;

    public Slider healthBar;

    private void Start() {
        player = Player.instance;
    }


    private void Update() {
        healthBar.value = player.health / player.maxHealth;    
    }
}
