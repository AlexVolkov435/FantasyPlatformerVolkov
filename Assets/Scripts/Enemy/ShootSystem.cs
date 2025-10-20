using System.Collections;
using UnityEngine;

public abstract class ShootSystem : MonoBehaviour
{
    [Header("Настройка снаряда")]
    [SerializeField] protected Rigidbody2D projectilePrefab;
    [SerializeField] protected Transform startPointProjectile;
    
    // Определение методов
    public abstract IEnumerator MakeShoot();
    public abstract void DetermineDistance();
    public abstract void Shoot();
}