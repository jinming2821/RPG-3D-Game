using UnityEngine;
using System.Collections;
public class MagicCircleSpawner : MonoBehaviour
{
    [Header("生成设置")]
    public GameObject enemyPrefab; 
    [Range(0, 50)] public int spawnCount = 5; 
    public float spawnRadius = 3f;
    public float spawnInterval = 2.0f; // 每个怪物生成的时间间隔（秒）
    [Header("特效设置")]
    public GameObject spawnEffect; // 拖入你的魔法阵特效 Prefab

    [Header("触发设置")]
    public bool autoSpawn = true; // 勾选则自动生成，不勾选则需玩家触发

    private bool _hasSpawned = false; // 防止重复生成

    void Start()
    {
        if (autoSpawn)
        {
            ActivateMagicCircle();
        }
    }

    // 方案：玩家靠近自动触发
    private void OnTriggerEnter(Collider other)
    {
        if (!autoSpawn && other.CompareTag("Target") && !_hasSpawned)
        {
            ActivateMagicCircle();
        }
    }

    public void ActivateMagicCircle()
    {
        if (_hasSpawned) return;
        _hasSpawned = true;
        StartCoroutine(SpawnRoutine());
        // 1. 播放特效
        if (spawnEffect != null)
        {
            Instantiate(spawnEffect, transform.position, transform.rotation);
        }

        // 2. 批量生成怪物
        IEnumerator SpawnRoutine(){
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-spawnRadius, spawnRadius), 
                0.5f, // 给个高度，防止怪物卡在地下
                Random.Range(-spawnRadius, spawnRadius)
            );
            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            Debug.Log($"生成了第 {i + 1} 只怪物");
            // 3. 等待指定的时间间隔，然后再进入下一次循环
            yield return new WaitForSeconds(spawnInterval);
        }
        }
        

        Debug.Log($"✨ 魔法阵启动：已生成 {spawnCount} 只怪物！");
        if (spawnEffect != null)
        {
            // 生成特效实例
            GameObject effect = Instantiate(spawnEffect, transform.position, transform.rotation);
            
            // 让特效在 2 秒后自动销毁，防止占用内存
            Destroy(effect, 2.0f); 
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; // 改成黄色表示魔法阵范围
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}