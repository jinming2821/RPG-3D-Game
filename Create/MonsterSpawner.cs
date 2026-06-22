using UnityEngine;
using System.Collections;

public class MagicCircleSpawner : MonoBehaviour
{
    [Header("生成设置")]
    public GameObject enemyPrefab;      // 这是你要生成的怪物
    public GameObject magicCirclePrefab; // 这是魔法阵特效
    [Range(0, 50)] public int spawnCount = 5; 
    public float spawnRadius = 3f;
    public float spawnInterval = 2.0f; 

    [Header("触发设置")]
    public bool autoSpawn = true;
    private bool _hasSpawned = false;

    void Start()
    {
        if (autoSpawn) ActivateMagicCircle();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 确保你的玩家 Tag 是 "Player"，如果是其他名字请自行修改
        if (!autoSpawn && other.CompareTag("Player") && !_hasSpawned)
        {
            ActivateMagicCircle();
        }
    }

    public void ActivateMagicCircle()
    {
        if (_hasSpawned) return;
        _hasSpawned = true;

        // 1. 先生成魔法阵特效（只生成一次）
        if (magicCirclePrefab != null)
        {
            GameObject effect = Instantiate(magicCirclePrefab, transform.position, transform.rotation);
            Destroy(effect, 3.0f); // 3秒后销毁魔法阵
        }

        // 2. 启动生成怪物的协程
        StartCoroutine(SpawnRoutine());
    }

    // 正确：把协程定义在函数外面！
    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-spawnRadius, spawnRadius), 
                0.5f, 
                Random.Range(-spawnRadius, spawnRadius)
            );

            // 生成怪物
            if (enemyPrefab != null)
            {
                Instantiate(enemyPrefab, randomPos, Quaternion.identity);
                Debug.Log($"生成了第 {i + 1} 只怪物");
            }
            else
            {
                Debug.LogError("🚨 错误：enemyPrefab 没有被赋值！");
            }

            yield return new WaitForSeconds(spawnInterval);
        }
        Debug.Log("✨ 所有怪物生成完毕！");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}