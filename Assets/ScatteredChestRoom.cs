using UnityEngine;

public class ScatteredChestRoom : Room {

    public Entity destroyableCratePrefab;

    public HostileEnemy[] enemies;

    public float noiseScale;

    private void Start() {
        noiseScale *= Random.Range(0.5f, 1.25f);
    }

    public override GameObject GenCell(int x, int y, Vector3 ePos, float seed) {
        float prl = Mathf.PerlinNoise(ePos.x * noiseScale, ePos.z * noiseScale);
        if (prl > 0.7f) {
            return AddEntity(destroyableCratePrefab, ePos, Quaternion.identity);
        } else {
            if(enemy < 6 && Random.Range(0,4) == 1) {
                return AddEntity(enemies[Random.Range(0,enemies.Length-1)], ePos, Quaternion.identity);
            } else
                return null;
        }
    }
}
