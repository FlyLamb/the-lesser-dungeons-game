using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarDisplay : MonoBehaviour
{
    public GameObject healthbarPrefab;
    private GameObject cnvs;
    private Slider hslider;
    public float value;
    public Vector3 offset;

    private void OnEnable() {
        cnvs = Instantiate(healthbarPrefab);
        hslider = cnvs.transform.GetChild(0).GetComponent<Slider>();
    }

    private void Update() {
        if (cnvs == null) return;
        cnvs.transform.position = transform.position + offset ;
        //cnvs.transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward);
        hslider.value = value;
    }

    private void OnDisable() {
        Destroy(cnvs);
    }
}
