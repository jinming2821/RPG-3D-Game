using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // for player to tranform damage here 
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"💥 monster receive damage {damage} point damage, remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 monster dead。");
        Destroy(gameObject); // remove monster
    }
}