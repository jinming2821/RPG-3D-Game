using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AiChasing : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent _agent;
    private Animator _animator;

    public float attackRange = 2f;
    public float attackCooldown = 2f;
    private float _lastAttackTime;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        
        // 初始化时寻找一次玩家
        FindPlayer();
    }

   void Update()
{
    if (player == null)
    {
        FindPlayer();
        return;
    }

    // --- 调试代码：查看到底是谁没动 ---
    // ---------------------------------

    float distance = Vector3.Distance(transform.position, player.position);

    if (distance <= attackRange)
    {
        HandleAttackState();
    }
    else
    {
        HandleChaseState();
    }
}

    void FindPlayer()
    {
        GameObject targetObj = GameObject.FindGameObjectWithTag("Target");
        if (targetObj != null)
        {
            player = targetObj.transform;
            Debug.Log("✅ 成功锁定玩家！");
        }
    }

    void HandleChaseState()
    {
        _agent.isStopped = false;
        _agent.SetDestination(player.position);
        if (_animator != null) _animator.SetBool("isMoving", true);
    }

    void HandleAttackState()
    {
        _agent.isStopped = true; // 停下攻击
        if (_animator != null) _animator.SetBool("isMoving", false);
        
        TryAttack();
    }

    void TryAttack()
    {
        if (Time.time - _lastAttackTime >= attackCooldown)
        {
            if (_animator != null) _animator.SetTrigger("EnemyPunch");
            _lastAttackTime = Time.time;

            // --- 核心修改：确保 player 真的存在且能获取到脚本 ---
            if (player != null)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(10f);
                    Debug.Log("👹 敌人造成了 10 点伤害！");
                }
                else
                {
                    Debug.LogError("❌ 错误：在玩家身上找不到 PlayerHealth 脚本！请检查玩家物体是否挂载了该脚本。");
                }
            }
        }
    }
}