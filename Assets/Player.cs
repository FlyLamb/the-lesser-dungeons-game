using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance;


    private void Awake() {
        instance = this;
    }

    public float baseSpeed;
    public Room current;
    public PlayerControl control;

    public Weapon weapon;

    private Rigidbody rb;

    [HideInInspector]
    public Vector3 inputVector;
    [HideInInspector]
    public Vector3 shootVector;

    public float health = 100f;
    public float maxHealth = 100f;

    public float shootDelay;
    public PlayerAnimator animator;

    private float poisonedTime = 0;

    public bool interactHeld = false;
    public bool shotBullet;

    private void Start() {
        control = new PlayerControl();
        control.Enable();

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

        current.OnPlayerEnter(this);
    }

    public WeaponPickupable DropWeapon() {
        GameObject g = Instantiate(Statics.instance.dropWeaponPrefab,transform.position + new Vector3(0,2,0),Quaternion.identity);
        g.GetComponent<WeaponPickupable>().weapon = weapon;
        weapon = null;
        return g.GetComponent<WeaponPickupable>();
    }

    public void Damage(float dmg, Entity.DamageType damageType = Entity.DamageType.normal) {
        health -= dmg; // TODO: apply modifiers like armor and defense or any other stats
        if (health <= 0) {
            Die();
        }
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    public void Effect(Entity.DamageType effect, float time) {
        if(effect == Entity.DamageType.poison)
            poisonedTime = time;
    }

    public void Die() {
        Application.Quit();
    }

    private void Update() {
        shotBullet = false;
        rb.velocity = inputVector * baseSpeed;

        if (weapon != null && shootVector.magnitude > 0.1f && shootDelay <= 0) {
            shotBullet = true;
            weapon.Shoot(this, transform.position, shootVector);
        }

        if (shootDelay > 0) {
            shootDelay -= Time.deltaTime;
        }
        if (poisonedTime >= 0) {
            Damage(Time.deltaTime * 20, Entity.DamageType.poison);
            poisonedTime -= Time.deltaTime;
        }
        
        animator.Animate(rb.velocity,shootVector);
    }
}
