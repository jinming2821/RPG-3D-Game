using UnityEngine;
using UnityEngine.UI; // 必须引用 UI 命名空间
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _currentHealth;
    public GameObject deathPanel;
    public Slider healthSlider; // 在 Inspector 中把刚才的 Slider 拖进来
    public bool isDead = false; // 新增状态标记
    void Start()
    {       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        deathPanel.SetActive(false);
        _currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; // 【核心修改】如果已经死了，直接跳过伤害逻辑
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        Debug.Log("当前血量: " + _currentHealth);
        UpdateHealthBar();

        if (_currentHealth <= 0)
        {
            Debug.Log("💀 角色已死亡");
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            // 将当前血量除以最大血量，得到 0 到 1 之间的比例
            healthSlider.value = _currentHealth / maxHealth;
        }
    }
    public void RespawnPlayer() // 改成了新的名字
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Die()
    {
        // 1. 显示死亡界面
        isDead = true;
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            // 触发动画转换
            anim.SetTrigger("isDead");
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // 设置为运动学模式，彻底冻结物理
        }
        deathPanel.SetActive(true);
        
        // 2. 核心：禁用输入！
        var inputs = GetComponent<StarterAssets.StarterAssetsInputs>();
        if (inputs != null)
        {
            inputs.inputEnabled = false; 
            inputs.move = Vector2.zero;
            inputs.look = Vector2.zero;
        }

        // 3. 释放鼠标
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // 4. (可选) 如果你还想更彻底，可以禁用移动脚本
        // GetComponent<StarterAssets.FirstPersonController>().enabled = false;
    }
}