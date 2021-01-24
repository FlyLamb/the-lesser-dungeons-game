using UnityEngine;

public class RoomCamera : MonoBehaviour {
    [SerializeField]
    private Vector3 offset;

    public float lerpSpeed = 10;

    private void Update() {
        transform.position = Vector3.Lerp(transform.position, Player.instance.current.transform.position + offset, Time.deltaTime * lerpSpeed);
    }
}
