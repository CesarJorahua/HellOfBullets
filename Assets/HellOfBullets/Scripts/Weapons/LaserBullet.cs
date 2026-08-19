using System;
using UnityEngine;

public class LaserBullet: MonoBehaviour, IDamager
{
    [SerializeField] private float damageDeal = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out IDamagable damageable);
        damageable?.TakeDamage(damageDeal);
    }
}