using UnityEngine;
[RequireComponent(typeof(DamageFlashComponent))]
public class Enemy :MonoBehaviour
{
    private DamageFlashComponent _damageFlashComponent;
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindAnyObjectByType<PlayerMovementComponent>().transform;
        _damageFlashComponent = GetComponent<DamageFlashComponent>();
    }
}
