using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player;

    public Image weapon1, weapon2;
    public TextMeshProUGUI ammo1,ammo2, ammo;

    public TextMeshProUGUI res, fireRes, poisonRes, maxHealth, speed, luck;
    
    public Slider healthBar, shootBar;

    public RectTransform content;

    public GameObject image;

    private List<Image> spwnd;

    private int bcount;

    private Vector2 bsd ;

    private void Start() {
        player = Player.instance;
        spwnd = new List<Image>();
        bsd = content.sizeDelta;
    }


    private void Update() {
        healthBar.value = player.health / player.maxHealth;
        if (player.weapon != null)
            weapon1.sprite = player.weapon.icon;
        else
            weapon1.sprite = Statics.instance.none;

        if (player.secondary != null)
            weapon2.sprite = player.secondary.icon;
        else
            weapon2.sprite = Statics.instance.none;

        ammo1.text = $"{player.ammoS1}";
        ammo2.text = $"{player.ammoS2}";


        res.text = player.md_baseResistance.ToString("0.0");
        fireRes.text = player.md_fireResistance.ToString("0.0");
        poisonRes.text = player.md_poisonResistance.ToString("0.0");
        maxHealth.text = player.md_maxHealth.ToString("0.0");
        speed.text = player.md_speed.ToString("0.0");
        luck.text = player.md_luck.ToString("0.0");

        ammo.text = $"{player.ammo}";
        if (player.weapon != null)
            shootBar.value = player.shootDelay / player.weapon.reloadTime;
        else
            shootBar.value = 1;

        if (bcount != player.items.Count) {
            foreach (Image d in spwnd) Destroy(d.gameObject);
            spwnd.Clear();
            content.sizeDelta = bsd;
            for (int i = 0; i < player.items.Count; i++) {
                content.sizeDelta += new Vector2(31, 0);
                Image img = Instantiate(image, content.transform).GetComponent<Image>();
                img.sprite = player.items[i].icon;
                img.GetComponent<RectTransform>().anchoredPosition = new Vector3(84 * i + 50,0,0);
                spwnd.Add(img);
                bcount = player.items.Count;
            }
        }
    }
}
