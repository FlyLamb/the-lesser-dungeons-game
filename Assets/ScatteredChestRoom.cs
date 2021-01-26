using UnityEngine;

public class ScatteredChestRoom : Room {

    public GameObject destroyableCratePrefab;
    public float noiseScale;

    public override GameObject GenCell(int x, int y, Vector3 ePos, float seed) {
        float prl = Mathf.PerlinNoise(ePos.x * noiseScale, ePos.z * noiseScale);
        if (prl > 0.7f)
            return Instantiate(destroyableCratePrefab,ePos,Quaternion.identity);
        return null;
    }
}
