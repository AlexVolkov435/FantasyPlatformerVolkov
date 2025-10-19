using System;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] public float healAmount = 20f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (other.gameObject.TryGetComponent(out PlayerHealthSystem health))
            {
                health.Heal(healAmount);
            }
            else
            {
                Debug.LogWarning("PlayerHealthSystem не найден!");
            }

            Destroy(gameObject);
        }
    }
}