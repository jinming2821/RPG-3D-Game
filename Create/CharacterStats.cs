using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    [Header("UI References")]
    public Slider expSlider;
    public TextMeshProUGUI levelText;

    [Header("Data")]
    public PlayerStatsData playerStats; // 你的 ScriptableObject 资产

    void Start()
    {
        UpdateUI();
    }

    public void GainExp(int amount)
    {
        // 操作 PlayerStatsData 中的数据
        playerStats.exp += amount;
        
        // 循环检查是否足以升级
        while (playerStats.exp >= playerStats.expToNextLevel)
        {
            playerStats.exp -= playerStats.expToNextLevel;
            LevelUp();
        }
        
        UpdateUI();
    }

    void LevelUp()
    {
        playerStats.level++; 
        playerStats.freeStatPoints += 1; 
        
        // 升级难度增加
        playerStats.expToNextLevel += 50; 

        // 应用职业成长属性
        if (playerStats.playerClass != null)
        {
            // 注意：这里使用的是 baseHp，对应我们之前在 Data 里定义的 baseHp
            playerStats.baseHp += playerStats.playerClass.hpGainPerLevel;
            playerStats.baseAtk += playerStats.playerClass.atkGainPerLevel;
            playerStats.baseSpeed += playerStats.playerClass.speedGainPerLevel;
        }
        
        Debug.Log("恭喜升级！当前等级: " + playerStats.level);
    }

    public void UpdateUI()
    {
        if (expSlider != null)
        {
            // 使用存储在 Data 里的 expToNextLevel 进行比例计算
            expSlider.value = (float)playerStats.exp / playerStats.expToNextLevel;
        }
            
        if (levelText != null)
        {
            levelText.text = "Lv. " + playerStats.level;
        }

    }
}