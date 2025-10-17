using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    [SerializeField] private Transform attackPoint; // Ссылка на зону атаки
    [SerializeField] private LayerMask layerMask;
   
    [SerializeField] private bool isPlayer;
    
    private Animator _animator;
    private float _timer;
    private EnemyHealthSystem _enemyHealthSystem;
    private ChasePlayer _chasePlayer; 
    
    /*
     * @param инициализация поведения MeleeCombat
     * @return присваиваем значения переменным _playerInput, _fireAction, получаем новое поведение MeleeCombat
     */
    private void Awake()
    {
        SetAttackMeleeBehaviour(new MeleeCombat(attackPoint, layerMask, meleeWeaponData.damage,
            meleeWeaponData.rangeAttack,isPlayer));
        
         _animator =GetComponentInParent<Animator>();
         
        if(!isPlayer)
        {
             _chasePlayer = GetComponentInParent<ChasePlayer>();
             _enemyHealthSystem = GetComponentInParent<EnemyHealthSystem>();
        }
    }

    /*
     * Рисует гизмос
     * @param позицию атаки, радиус сферы
     * @return присваиваем значения переменным _playerInput, _fireAction, получаем новое поведение MeleeCombat
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, meleeWeaponData.rangeAttack);
    }

    /*
     * Обрабатывается нажатие клавиши атаки ПКМ
     * @param пауза между выстрелами
     * @return вызов метода Attack, время между выстрелами
     */
    private void Update()
    {
        if (isPlayer)
        {
            ApplyDamageEnemy();
        }
        else
        {
            ApplyDamagePlayer();
        }
    }

    private void ApplyDamageEnemy()
    {
        if (_timer <= 0)
        {
            _animator.SetBool("isAttack",Input.GetMouseButtonDown(1));
            
            if (Input.GetMouseButtonDown(1))
            {
                Attack();
                _timer = meleeWeaponData.shootCooldown;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    private void ApplyDamagePlayer()
    {
        if (!_enemyHealthSystem.isDeath)
        {
            if (_timer <= 0)
            {
                if (_chasePlayer.IsStopRun)
                {
                    _animator.SetTrigger("Attack");
                    Attack();
                    _timer = 2;
                }
            }
            else
            { 
                _timer -= Time.deltaTime;
            }
        }
        else
        { ; }
    }
}
