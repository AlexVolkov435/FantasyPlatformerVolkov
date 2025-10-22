using System.Collections;
using UnityEngine;

public class BallisticShoot : ShootSystem
{
    [SerializeField] private Transform player;
    [SerializeField] private int delay = 2;

    [Header("Угол запуска в градусах (от 1 до 89)")]
    [Range(1f, 89f)]
    [SerializeField] private float launchAngle = 45f;

    [Header("Растояние стрельбы")]
    [SerializeField] private float detectionRadius = 5f;

    private bool _canShoot = true;
    private float _gravity;
    private string _deathAnimationName = "Attack";
    
    private Animator _animator;
    private Enemy _enemy;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
    }

    /*
    * Рассчитывается ускорение свободного падения 
    * @return ускорение свободного падения
    */

   private void Start()
   {
       player = GameObject.FindWithTag("Player").transform;
        
        _gravity = Mathf.Abs(Physics2D.gravity.y);// ускорение свободного падения

        if (_gravity == 0)
        {
            _gravity = 9.81f; // ускорение свободного падения
        }
    }

    /*
   * @param позиция игрока,  позиция противника
   * @return расчитывается дистанция до игрока выполняется стрельба
   */

  private void Update()
    {
        float distanceToEnemy = Vector2.Distance(player.position, transform.position);

        if (distanceToEnemy <= detectionRadius && _canShoot)
        {
            StartCoroutine(MakeShoot());
        }
    }

    /*
    * Вызывается при приближении игрока, расстояние задаётся в инспекторе
    * @param угол полета, префаб снаряда
    * @return расчитывается скорость и напрвыление, создаётся снаряд
    */

    public override IEnumerator MakeShoot()
    {
        var wait = new WaitForSeconds(delay);
       
        float angleStart = 0;
        
        _canShoot = false;

        if (projectilePrefab == null || startPointProjectile == null || player == null)
        {
            Debug.LogWarning("Добавть в инспектор projectilePrefab, startPoint, targetEnemy ");
            yield return null;
        }
    
        Vector2 launchPosition = startPointProjectile.position;
        Vector2 targetPostion = player.position;

        float directionX = targetPostion.x - launchPosition.x;
        float directionY = targetPostion.y - launchPosition.y;

        // перевод в радианы
        float angleRad = launchAngle * Mathf.Deg2Rad;

        float cosAngle = Mathf.Cos(angleRad);
        float sinAngle = Mathf.Sin(angleRad);

        float distance = Mathf.Abs(directionX);

        // Вычисление  начальной скорости, формулу движения снаряда
        // v = sqrt( g * d^ 2 / (2 * cos^ 2 (угол) * (d * tan(угол) - h)) )
        float denominator = 2 * cosAngle * cosAngle * (distance * Mathf.Tan(angleRad) - directionY);

        if (denominator <= 0)
        {
            Debug.LogWarning("Недопустимый угол");
            yield return null;
        }
        else
        {
            float initialVelocity = Mathf.Sqrt(_gravity * distance * distance / denominator);

            // Строим вектор скорости

            float speedX = initialVelocity * cosAngle * Mathf.Sign(directionX);
            float speedY = initialVelocity * sinAngle;

            Vector2 launchVelocity = new Vector2(speedX, speedY);

            DetermineAngleProjectile(ref angleStart);
            
            StartCoroutine(DyingCoroutine());
            
            Rigidbody2D projectile = Instantiate(projectilePrefab, startPointProjectile.position,
                Quaternion.Euler(0, 0, angleStart));
            
            projectile.velocity = launchVelocity;
            
            yield return wait;
        }

        _canShoot = true;
    }
    
    /*
     * Расчитываетя угол снаряда в зависимомти от направления противника
     * @param нулевая переменная angleStart
     * @return angleStart 
     */
    private void DetermineAngleProjectile( ref float angleStart)
    {
        if (_enemy.IsFacingRight)
        {
            angleStart = 135;
        }
        else
        {
            angleStart = 45;
        }
    }
    
    /*
     * Корутина, которая делает задержку равную времени анимации
     * @param имя тригера, компонент Rigidbody2D,Collider2D
     * @return время анимации, уничтожает объект
     */
    private IEnumerator DyingCoroutine()
    {
        _animator.SetTrigger("Attack");
        
        float AnimationLength = GetAnimationLength(_deathAnimationName);
        yield return new WaitForSeconds(AnimationLength +100);
    }
    
    /*
     * Метод который возвращает длину анимации
     * @param animName
     * @return длина анимации
     */
    private float GetAnimationLength(string animName)
    {
        RuntimeAnimatorController ac = _animator.runtimeAnimatorController;
        
        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == animName)
                return clip.length;
        }
        
        return 1f; // Значение по умолчанию, если анимация не найдена
    }

    public override void DetermineDistance(){ }
    public override void Shoot(){ }
}
