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
    private Vector3 inputVector;
    private Vector3 shootVector;

    public float health = 100f;
    public float maxHealth = 100f;

    public float shootDelay;
    public PlayerAnimator animator;

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

        current.OnPlayerEnter(this);
    }

    public void Damage(float dmg) {
        health -= dmg; // TODO: apply modifiers like armor and defense or any other stats
        if (health <= 0) {
            Die();
        }
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    public void Die() {
        Application.Quit();
    }

    private void Update() {
        
        rb.velocity = inputVector * baseSpeed;

        if (weapon != null && shootVector.magnitude > 0.1f && shootDelay <= 0) {
            weapon.Shoot(this, transform.position, shootVector);
        }

        if (shootDelay > 0) {
            shootDelay -= Time.deltaTime;
        }

        animator.Animate(rb.velocity,shootVector);
    }
}
