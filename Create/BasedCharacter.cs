using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseCharacter : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 100f;
    protected float currentHealth;
    public bool isDead = false;

    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log($"收到伤害: {damage}, 当前血量: {currentHealth}"); // 检查伤害值是否异常
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        isDead = true;
        if (anim != null) anim.SetTrigger("isDead");
    }
}