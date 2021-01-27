using System.Collections;
using UnityEngine;

public class PotionEffectCloud : MonoBehaviour {
    public float preDissapear;

    private void Start() {
        StartCoroutine(CloudLife());
    }

    private IEnumerator CloudLife() {
        yield return new WaitForSeconds(preDissapear);

        while (transform.localScale.magnitude > 0.1) {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
