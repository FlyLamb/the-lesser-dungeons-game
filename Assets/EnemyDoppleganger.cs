using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoppleganger : HostileEnemy
{
    public float speed;

    private Rigidbody rb;
    public ModelAnimator mdl;
    public ModelAnimator pan;


    public GameObject bullet; 

    private void Start() {
        rb = GetComponent<Rigidbody>();
        mdl = GetComponent<ModelAnimator>();
        pan = Player.instance.animator.GetComponent<ModelAnimator>();
        mdl.Play(0);
    }

    private void Update() {
        UpdateHealthDisplay();
        rb.velocity = -Player.instance.GetComponent<Rigidbody>().velocity * speed;

        

        if(mdl.currentAnimation.name != pan.currentAnimation.name)
            mdl.Play(pan.currentAnimation.name);

        if(Player.instance.shotBullet) {
            GameObject g = Instantiate(bullet, transform.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().AddForce(-Player.instance.shootVector * 50);
            g.GetComponent<Bullet>().EnemyBullet();
        }
    }
}
