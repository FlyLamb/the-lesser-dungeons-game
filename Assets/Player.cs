using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance;
    private void Awake() {
        instance = this;
    }

    public float baseSpeed;
    public Room current;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        RoomGenerator.generator.Generate();
        current = RoomGenerator.generator.mapLayout[
            RoomGenerator.generator.MAP_SIZE / 2, RoomGenerator.generator.MAP_SIZE / 2];
    }

    private void Update() {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.MovePosition(transform.position + (inputVector * baseSpeed * Time.deltaTime));
    }
}
