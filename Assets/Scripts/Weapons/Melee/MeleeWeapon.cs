using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    [SerializeField] private Transform attackPoint; // Ссылка на зону атаки
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject player;

    private Animator _animator;
    private float _timer;

    /*
     * @param инициализация поведения MeleeCombat
     * @return присваиваем значения переменным _playerInput, _fireAction, получаем новое поведение MeleeCombat
     */
    private void Awake()
    {
        SetAttackMeleeBehaviour(new MeleeCombat(attackPoint, layerMask, meleeWeaponData.damage,
            meleeWeaponData.rangeAttack, player));
         _animator =GetComponentInParent<Animator>();
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
}
