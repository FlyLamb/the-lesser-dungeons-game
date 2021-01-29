using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    public ModelAnimator animator;
    private void Start() {
        animator = GetComponent<ModelAnimator>();
    }
    public void Animate(Vector3 movement, Vector3 shooting) {

        Vector3 lm;
        if (shooting.magnitude <= 0.2f) lm = movement;
        else lm = shooting;

        if (movement.magnitude >= 0.2f) {
            if (!animator.isPlaying || animator.currentAnimation.name != "Walk") {
                animator.Play("Walk");
            }
        } else {
            if (!animator.isPlaying || animator.currentAnimation.name != "Idle") {
                animator.Play("Idle");
            }
        }

        transform.LookAt(transform.position + lm, Vector3.up);
    }
}
