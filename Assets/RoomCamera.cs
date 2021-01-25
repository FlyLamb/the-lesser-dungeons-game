using UnityEngine;

public class RoomCamera : MonoBehaviour {
    [SerializeField]
    private Vector3 offset;

    private Vector3 roomCenter;

    public float lerpSpeed = 10;
    public float playerRotating = 0.3f;
    public float playerTracking = 0.5f;



    private void LateUpdate() {
        roomCenter = Vector3.Lerp(roomCenter,Player.instance.current.transform.position + offset,Time.deltaTime * lerpSpeed);
        transform.position = Vector3.Lerp(roomCenter, Player.instance.transform.position + offset, playerTracking);
        Quaternion qrot = Quaternion.Euler(90,0,0);
        transform.LookAt(Player.instance.transform.position,Vector3.forward);
        Quaternion lrot = transform.rotation;

        transform.rotation = Quaternion.Lerp(qrot, lrot, playerRotating);
    }
}
