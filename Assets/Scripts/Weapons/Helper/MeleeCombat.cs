using UnityEngine;

public class MeleeCombat : IAttackMelee
{
    private Transform _attackPoint;
    private LayerMask _damageableLayerMack;
    
    private float _damage;
    private float _attackrange;
    private bool _isObject;
    private bool _isPlayer;
    
    /* Конструктор
     * @param attackPoint, damageableLayerMack, damage, attackrange
     * @return переменные _attackPoint,_damageableLayerMack,_damage,_attackrange,_player
     */
    public MeleeCombat(Transform attackPoint, LayerMask damageableLayerMack, float damage, float attackrange,
        bool isPlayer)
    {
        _attackPoint = attackPoint;
        _damageableLayerMack = damageableLayerMack;
        _damage = damage;
        _attackrange = attackrange;
        _isPlayer = isPlayer;
    }
    
    /* метод атаки
     * @param _attackPoint.position, _attackrange, _damageableLayerMack, _damage,_player.gameObject
     * @return обнаруживаем объект со скриптом EnemyHealthSystem и наносим урон TakeDamage
     */
    public void Attack()
    {
        Collider2D[] enemies =
            Physics2D.OverlapCircleAll(_attackPoint.position, _attackrange, _damageableLayerMack);

        if (enemies.Length != 0)
        {
            foreach (Collider2D obj in enemies)
            {
                if (_isPlayer)
                {
                    if (obj.TryGetComponent<EnemyHealthSystem>(out EnemyHealthSystem enemyHealth))
                    {
                        enemyHealth.TakeDamage(_damage);
                    }
                }
                else
                {
                    if (obj.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem enemyHealth))
                    {
                        enemyHealth.TakeDamage(_damage);
                    }
                }
            }
        }
    }
}