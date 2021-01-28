using System.Collections.Generic;
using UnityEngine;

public class ScatteredChestRoom : Room {

    public Entity destroyableCratePrefab;

    public HostileEnemy[] enemies;

    public float noiseScale;

    public List<Vector2Int> poss;

    public override void Setup() {
        noiseScale *= Random.Range(0.5f, 1.25f);
        poss = new List<Vector2Int>();
        for(int i = 0; i < Random.Range(0,8); i++) {
            poss.Add(new Vector2Int(Random.Range(-6,6), Random.Range(-4, 4)));
        }
    }

    public override GameObject GenCell(int x, int y, Vector3 ePos, float seed) {
        float prl = Mathf.PerlinNoise(ePos.x * noiseScale, ePos.z * noiseScale);
        if (prl > 0.7f) {
            return AddEntity(destroyableCratePrefab, ePos, Quaternion.identity);
        } else {
            foreach (Vector2Int w in poss) {
                if (w.x == x && w.y == y) {
                    return AddEntity(enemies[Random.Range(0, enemies.Length - 1)], ePos, Quaternion.identity);
                }
            }
                    
        }
        return null;
    }
}
