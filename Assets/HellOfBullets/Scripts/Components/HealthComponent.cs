using UnityEngine;
using UnityEngine.UIElements;
using Alchemy.Inspector;

[RequireComponent(typeof(DamageFlashComponent))]
public class HealthComponent: MonoBehaviour, IDamagable, IHealthInit
{
    [SerializeField] private HealthData _healthData;
    private float _currentHealth;

    #if UNITY_EDITOR
        [LabelText("Current Health")]
    [ReadOnly] public float editorCurrentHealth;
    #endif

    private DamageFlashComponent damageFlashComponent;

    private void Awake()
    {
        damageFlashComponent = GetComponent<DamageFlashComponent>();
        InitializeHealth();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        #if UNITY_EDITOR
            editorCurrentHealth = _currentHealth;
        #endif
        damageFlashComponent.StartFlashEffect();
        if(_currentHealth<=0)
            Die();
    }

    public void Die()
    {
        Debug.Log("Enemy with id: " + GetEntityId() + " died", this);
        StopAllCoroutines();
        damageFlashComponent.StopFlashEffect();
    }

    public void InitializeHealth()
    {
        _currentHealth = _healthData.maxHealth;
        #if UNITY_EDITOR
            editorCurrentHealth = _currentHealth;
        #endif
    }
}