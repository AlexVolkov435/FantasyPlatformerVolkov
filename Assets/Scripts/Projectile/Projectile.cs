using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int delayDestroy;
    [SerializeField] private float distance;
    
    [SerializeField] private LayerMask layer;
    [SerializeField] private bool isEnemyBullet;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /*
     * Вызывается при столкновением с тегом Player
     * @param проверяется первое касание с объектом на котором есть тег Player
     *  @return вызывается метод TakeDamage
     */
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, layer);
       
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") && isEnemyBullet)
            {
                hit.collider.GetComponent<PlayerHealthSystem>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHealthSystem>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        
        Invoke(nameof(DestroyProjectile), delayDestroy);
    }

    private void FixedUpdate()
    {
        UpdateRotation();
    }

    /*
     * @return уничтожается объект
     */
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    /*  Обновление положения угла снаряда
     * @param _rigidbody.linearVelocity
     *  @return transform.rotation
     */
    private void UpdateRotation()
    {
        float angle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
    }
}