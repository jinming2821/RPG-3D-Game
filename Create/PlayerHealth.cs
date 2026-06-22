using UnityEngine;             // 这一行必须有！包含了 GameObject, MonoBehaviour, Debug 等
using UnityEngine.UI;          // 这一行必须有！包含了 Slider
using UnityEngine.SceneManagement;

public class PlayerHealth : BaseCharacter // 继承基类
{
    public GameObject deathPanel;
    public Slider healthSlider;
    public PlayerStatsData playerStats; // 拖入你的数据资产文件

    protected override void Awake()
    {
        base.Awake();
        deathPanel.SetActive(false);

        // 💡 关键：确保游戏开始时，currentHealth 与你的数据资产同步
        currentHealth = playerStats.GetTotalHp(); 
        UpdateHealthBar();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage); // 先扣血，判断死亡
        UpdateHealthBar();
    }

   void UpdateHealthBar()
    {
        float currentMaxHp = playerStats.GetTotalHp();
        healthSlider.maxValue = currentMaxHp;
        
        // 如果你有一个 currentHealth 变量（比如在基类里）
        healthSlider.value = currentHealth; 
    }

    protected override void Die()
    {
        base.Die(); // 触发 isDead 和动画
        
        // 这里放入玩家特有的死亡逻辑（UI、鼠标锁定、输入锁定）
        deathPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var inputs = GetComponent<StarterAssets.StarterAssetsInputs>();
        if (inputs != null) inputs.inputEnabled = false;
    }
    public void RespawnPlayer() 
    {
        // 1. Reset time (in case you paused it)
        Time.timeScale = 1f;
        
        // 2. Reload the current scene to reset everything
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}