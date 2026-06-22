using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "RPG/Player Stats")]
public class PlayerStatsData : ScriptableObject
{
    [Header("职业模版")]
    public PlayerClassData playerClass;

    [Header("等级与经验")]
    public int level = 1;
    public int exp = 0;
    public int expToNextLevel = 100;
    public int freeStatPoints = 0;

    [Header("基础属性 (随等级增长)")]
    public float baseHp;   // 随等级提升的Hp
    public float baseAtk;
    public float baseSpeed;

    [Header("玩家自由加点 (Bonus)")]
    public float bonusHp = 0f;
    public float bonusAtk = 0f;
    public float bonusSpeed = 0f;

    // --- 最终属性获取 ---
    public float GetTotalHp() 
    {
        float baseVal = (playerClass != null) ? playerClass.baseHp : 0;
        return baseVal + baseHp + bonusHp;
    }

    public float GetTotalAtk() 
    {
        float baseVal = (playerClass != null) ? playerClass.baseAtk : 0;
        return baseVal + baseAtk + bonusAtk;
    }
    public float GetTotalSpeed() 
    {
        float baseVal = (playerClass != null) ? playerClass.baseSpeed : 0;
        return baseVal + baseSpeed + bonusSpeed;
    }
    public float GetTotalAttackRange()
    {
        if (playerClass != null)
            {
                return playerClass.baseAttackRange;
            }
        return 2f; // 如果没设置职业，给个默认值
    }
    // 初始化方法：在游戏开始或选职业时调用
    public void Initialize(PlayerClassData selectedClass)
    {
        playerClass = selectedClass;
        level = 1;
        exp = 0;
        freeStatPoints = 0;
        
        baseHp = selectedClass.baseHp;
        baseAtk = selectedClass.baseAtk;
        baseSpeed = selectedClass.baseSpeed;
    }
}