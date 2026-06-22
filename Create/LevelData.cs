using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "RPG/Level Data")]
public class LevelData : ScriptableObject
{
    public int level;
    public float maxHealth;
    public float attackPower;
    public float moveSpeed;
    public int expRequired; // 升级所需经验
}