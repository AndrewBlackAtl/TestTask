using UnityEngine;

[CreateAssetMenu]
public class EnemySpawnerConfig : ScriptableObject
{
    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _screenOffsetDistance;
    
    public EnemyConfig EnemyConfig => _enemyConfig;
    public float Cooldown => _cooldown;
    public float ScreenOffsetDistance => _screenOffsetDistance;
}
