using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 25;
    public float attackRange = 3f;
    private Animator _animator;

    void Start()
    {
        // 💡 加上这一行看看是否找到了 Animator
        _animator = GetComponent<Animator>();
        
        if (_animator == null)
        {
            Debug.LogError("🚨 警告：没找到 Animator！请检查 PlayerAttack 脚本是否挂在有 Animator 的物体上！");
        }
        else
        {
            Debug.Log("✅ Animator 已成功连接！");
        }
    }

    void Update()
    {
        if (!GetComponent<StarterAssets.StarterAssetsInputs>().inputEnabled)
         {
        return; 
        }
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            PerformAttack();
        }

    }

    void PerformAttack()
    {
        // 💡 3. 核心：如果 Animator 存在，就发送 "Punch" 指令！
        if (_animator != null)
        {
            _animator.SetTrigger("Punch"); // 触发攻击
        
        }

        // 下面是原有的伤害逻辑
    }
    public void HitEvent()
        {
            Vector3 rayStart = transform.position + Vector3.up;
            Vector3 rayDirection = transform.forward;
            Debug.DrawRay(rayStart, rayDirection * attackRange, Color.red, 2.0f);

            Debug.Log("💥 动画事件 HitEvent 已触发！");
            Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, attackRange))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(attackDamage);
                        Debug.Log("💥 拳头击中！造成伤害：" + attackDamage);
                    }
                }
            }
        }
}
