using UnityEngine;

public class SpinningTrap : MonoBehaviour
{
    public Transform pivotPoint;     
    public float rotationSpeed = 1f;   
    public int damageAmount = 10;      

    // Основной цикл обновления кадра, используется для постоянного вращения ловушки
    private void Update()
    {
        if (pivotPoint != null)
        {
            transform.RotateAround(pivotPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    // Этот метод срабатывает при столкновении объекта с другим объектом.
    // Проверяет, является ли столкнувшийся объект игроком ("Player"), и если да, наносит ему урон
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerHealth = collision.gameObject.GetComponent<PlayerHealthSystem>();
            
            if (playerHealth != null)
                playerHealth.TakeDamage(damageAmount);  
        }
    }
}