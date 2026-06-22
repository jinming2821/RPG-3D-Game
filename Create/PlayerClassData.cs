using UnityEngine;

[CreateAssetMenu(fileName = "NewClass", menuName = "RPG/Class Data")]
public class PlayerClassData : ScriptableObject
{
    public string className;
    
    [Header("升级成长值")]
    public float hpGainPerLevel = 5f;
    public float atkGainPerLevel = 1f;
    public float speedGainPerLevel = 0.05f; // 升级增加的微量速度
    [Header("初始属性")]
    public float baseAttackRange = 2f;
    public float baseHp = 100f;
    public float baseAtk = 10f;
    public float baseSpeed = 5f; // 初始速度
}