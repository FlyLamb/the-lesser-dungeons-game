using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public static Player instance;


    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public float baseSpeed;
    public Room current;
    public PlayerControl control;

    public Weapon weapon, secondary;

    private Rigidbody rb;

    [HideInInspector]
    public Vector3 inputVector;
    [HideInInspector]
    public Vector3 shootVector;

    public float health = 100f;
    public float maxHealth = 100f;

    public float md_maxHealth;
    public float md_poisonResistance;
    public float md_fireResistance;
    public float md_baseResistance;
    public float md_speed;
    public float md_luck;

    [HideInInspector]
    public float shootDelay;
    public PlayerAnimator animator;

    private float poisonedTime = 0;

    [HideInInspector]
    public bool interactHeld = false;
    [HideInInspector]
    public bool shotBullet;

    public int ammo = 100;
    [HideInInspector]
    public int ammoS1, ammoS2;

    public List<Item> items;


    private void Start() {
        control = new PlayerControl();
        control.Enable();
        items = new List<Item>();
        rb = GetComponent<Rigidbody>();
        RoomGenerator.generator.Generate();
        current = RoomGenerator.generator.mapLayout[
            RoomGenerator.generator.MAP_SIZE / 2,
            RoomGenerator.generator.MAP_SIZE / 2];


        control.Player.Movement.performed += ctx => {
            inputVector = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
        };

        control.Player.Movement.canceled += ctx => {
            inputVector = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
        };

        control.Player.Shooting.performed += ctx => {
            shootVector = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
        };

        control.Player.Shooting.canceled += ctx => {
            shootVector = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
        };

        control.Player.Interact.performed += ctx => {
            interactHeld = true;
        };
        control.Player.Interact.canceled += ctx => {
            interactHeld = false;
        };

        control.Player.SwitchWeapon.performed += ctx => {
            if (shootDelay <= 0) {
                SwitchWeapons();
            }
        };

        control.Player.Reload.performed += ctx => { Reload(); };

        current.OnPlayerEnter(this);
        ApplyModifiers();
    }

    public WeaponPickupable DropWeapon() {
        GameObject g = Instantiate(Statics.instance.dropWeaponPrefab, transform.position + new Vector3(Random.Range(-.5f, .5f), .5f, Random.Range(-.5f, .5f)), Quaternion.identity);
        g.GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, 4);
        g.GetComponent<WeaponPickupable>().weapon = weapon;
        weapon = null;
        return g.GetComponent<WeaponPickupable>();
    }

    public void Damage(float dmg, Entity.DamageType damageType = Entity.DamageType.normal) {


        if (damageType == Entity.DamageType.normal) dmg -= md_baseResistance;
        if (damageType == Entity.DamageType.poison) dmg -= md_poisonResistance;
        if (damageType == Entity.DamageType.fire) dmg -= md_fireResistance;

        dmg = Mathf.Clamp(dmg, 0, 1000);

        health -= dmg; // TODO: apply modifiers like armor and defense or any other stats
        if (health <= 0) {
            Die();
        }
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    public void Effect(Entity.DamageType effect, float time) {
        if (effect == Entity.DamageType.poison) {
            poisonedTime = time;
        }
    }

    public void SetWeapon(Weapon w) {
        weapon = w;
        ammo += ammoS1;
        ammoS1 = 0;

    }

    private void SwitchWeapons() {
        Weapon w = weapon;
        int tam = ammoS1;
        SetWeapon(secondary);
        ammoS1 = ammoS2;
        secondary = w;
        ammoS2 = tam;
        ammo -= tam;
    }

    public void Die() {
        SceneManager.LoadScene(0);
    }

    private void Reload() {
        if (weapon == null || ammo <= 0) {
            return;
        }
        ammo += ammoS1;
        shootDelay = weapon.reloadTime;
        if (ammo > weapon.magazineSize) {
            ammoS1 = weapon.magazineSize;
            ammo -= weapon.magazineSize;
        } else {
            ammoS1 = ammo;
            ammo = 0;
        }
    }

    private void ApplyModifiers() {
        md_baseResistance = 0;
        md_fireResistance = 0;
        md_poisonResistance = 0;
        md_speed = baseSpeed;
        md_maxHealth = 100;
        md_luck = 1;

        foreach (Item i in items) {
            i.ApplyAddStats();
        }

        foreach (Item i in items) {
            i.ApplyMultiplyStats();
        }
    }

    public void AddItem(Item i) {
        items.Add(i);
        ApplyModifiers();
    }

    public void RemoveItem(Item i) {
        items.Remove(i);
        ApplyModifiers();
    }

    private void Update() {
        

        shotBullet = false;
        rb.velocity = inputVector * md_speed;

        if (weapon != null && shootVector.magnitude > 0.1f && shootDelay <= 0) {
            if (ammoS1 <= 0) {
                Reload();
            } else {
                shotBullet = true;
                weapon.Shoot(this, transform.position, shootVector);
                ammoS1--;
            }

        }

        if (shootDelay > 0) {
            shootDelay -= Time.deltaTime;
        }
        if (poisonedTime >= 0) {
            Damage(Time.deltaTime * 20, Entity.DamageType.poison);
            poisonedTime -= Time.deltaTime;
        }

        animator.Animate(rb.velocity, shootVector);
    }
}
