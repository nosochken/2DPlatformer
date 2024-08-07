using UnityEngine;

public class EnemySpawner : Spawner<SpawnEnemy>
{
    protected override void ActOnGet(SpawnEnemy item)
    {
        base.ActOnGet(item);
        item.GetSpawnZone(GiveSpawnZone());
    }

    protected override void SetSpawnPosition(SpawnEnemy item, SpawnZone spawnZone)
    {
        float averager = 2f;
        float averageX = (spawnZone.LeftmostX + spawnZone.RightmostX) / averager;

        Vector2 spawnPosition = new Vector2(averageX, spawnZone.Y + item.gameObject.transform.localScale.y);
        item.transform.position = spawnPosition;
    }
}