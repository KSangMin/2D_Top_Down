using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Rect spawnArea;

    [SerializeField] private List<GameObject> monsterPrefabs;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float randX = Random.Range(spawnArea.xMin, spawnArea.xMax);
        float randY = Random.Range(spawnArea.yMin, spawnArea.yMax);

        Vector2 randomPos = new Vector2(randX, randY);

        PickRandom(randomPos);

        yield return new WaitForSeconds(1);

        StartCoroutine(Spawn());
    }

    void PickRandom(Vector2 randomPos)
    {
        int randId = Random.Range(0, monsterPrefabs.Count);
        Poolable monster = PoolManager.Instance.Get(monsterPrefabs[randId]);

        for (int i = 0; i < 10; i++)
        {
            if (monster != null) break;

            randId = Random.Range(0, monsterPrefabs.Count);
            monster = PoolManager.Instance.Get(monsterPrefabs[randId]);
        }

        if (monster == null) return;

        monster.transform.position = randomPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector2 center = new Vector2(spawnArea.center.x, spawnArea.center.y);
        Vector2 size = new Vector2(spawnArea.width, spawnArea.height);
        Gizmos.DrawWireCube(center, size);
    }
}
