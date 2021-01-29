using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon")]
public class Weapon : ScriptableObject {
    public float useDelay = 1;
    public GameObject bullet;
    public GameObject weaponModel;
    public float bulletForce;
    public int magazineSize;
    public float reloadTime;
    public Sprite icon;
    [TextArea]
    public string description;


    public virtual bool Shoot(Player player, Vector3 origin, Vector3 direction) {
        Instantiate(bullet, origin, Quaternion.identity).GetComponent<Rigidbody>().AddForce(direction * bulletForce);
        player.shootDelay = useDelay;
        return true;
    }
}
