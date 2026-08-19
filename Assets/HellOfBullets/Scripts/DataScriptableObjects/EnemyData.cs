using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : AEntityData
{
    public float detectionRange;
    public float attackCooldown;
}
