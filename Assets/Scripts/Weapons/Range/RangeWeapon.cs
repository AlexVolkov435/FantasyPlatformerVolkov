using UnityEngine;

public class RangeWeapon : WeaponBase
{
    [Header("Ссылки на объект")] 
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform launchPoint; // точка запуска линии
    [SerializeField] private GameObject projectilePrefab;
    
    private Camera _mainCamera;
    private Vector2 _initialVelocity;
    
    private float _lastShootTime;
    private bool _isCanShoot = true;
    private bool _isCalculatingTrajectory;
    
    private Animator _animator;
    
    /*
     * @param инициализация поведения RangeCombat
     * @return присваиваем значения переменным _playerInput, _fireAction, получаем новое поведение MeleeCombat
     */
    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        SetAttackRangeBehaviour(new RangeСombat(projectilePrefab, launchPoint, lineRenderer));
    }

    /*
     * @return инициализация переменных,LineRenderer, расчет начальной скорости
     */
    private void Start()
    {
        _mainCamera = Camera.main;

        if (lineRenderer != null)
        {
            InitializeLineRenderer();
        }

        _lastShootTime = -rangeWeaponDate.shootCooldown; // Позволяет стрелять сразу
        CalculateInitialVelocity();
    }

    /*
     * @return инициализация переменных для LineRenderer
     */
    private void InitializeLineRenderer()
    {
        lineRenderer.positionCount = rangeWeaponDate.numberPointsTrajectory;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.enabled = false;
    }

    /*
     * @return вызываются методы UpdateCooldown,UpdatePositionWithMouse
     */
    private void Update()
    {
        UpdateCooldown();

        if (_isCalculatingTrajectory)
        {
            RaiseHand();
            UpdatePositionWithMouse();
        }
    }

    /*
     * расчитывается момент паузы перед выстрелом
     * @param rangeWeaponDate.shootCooldown,_lastShootTime
     * @return переменная _isCanShoot
     */
    void UpdateCooldown()
    {
        if (!_isCanShoot && Time.time - _lastShootTime >= rangeWeaponDate.shootCooldown)
        {
            _isCanShoot = true;
        }
    }

    /*
     * @return вызывается метод UpdatePositionWithMouse()
     */
    public void ShowTrajectory( )
    {
        UpdatePositionWithMouse();
        _isCalculatingTrajectory = true;
    }

    /*
     * Вызывается при отжатой клавиши
     * @param _initialVelocity,_isCanShoot
     * @return BalisticAttack,SetVariables
     */
    public void TakeShoot()
    {
        if (!_isCanShoot) return;
        
        _animator.SetBool("isSwingHand", false);
        _animator.SetTrigger("rangeAttack");
        
        BalisticAttack(_initialVelocity);
        SetVariables();
    }
    
    /* Выставляем переменные для отрисовки траектории, паузы между атакой, для атаки сразу
     * @return переменная _isCalculatingTrajectory, _isCanShoot, _lastShootTime
     */
    private void SetVariables()
    {
        _isCalculatingTrajectory = false;
        _isCanShoot = false;
        _lastShootTime = Time.time;
    }

    /*
     * Обновляется позиция мыши
     * @return вызывается методы CalculateInitialVelocity(),DrawTrajectory()
     */
    private void UpdatePositionWithMouse()
    {
        if (!_isCanShoot) return;

        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)launchPoint.position).normalized;

        rangeWeaponDate.angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        CalculateInitialVelocity();
        DrawTrajectory();
    }

    private void RaiseHand()
    {
        if (!_isCanShoot) return;
        
        //if (_playerInputReader.isStarted)
      //  {
      //      _animator.SetBool("isSwingHand", true);
       // }
    }

    /*
     * Рассчитывается начальная скорость
     * @param переменная projectileSpeed
     * @return переменная initialVelocity
     */
    private void CalculateInitialVelocity()
    {
        float angleRad = rangeWeaponDate.angle * Mathf.Deg2Rad;

        _initialVelocity = new Vector2(
            Mathf.Cos(angleRad) * rangeWeaponDate.projectileSpeed,
            Mathf.Sin(angleRad) * rangeWeaponDate.projectileSpeed
        );
    }

    /*
     * Выполняется отрисовка LineRenderer
     * @param позиция launchPoint запуска линии, initialVelocity скорость,
     * @return
     */
    private void DrawTrajectory()
    {
        // Проверка, что LineRenderer проинициализирован
        if (lineRenderer == null) return;

        Vector2 startPos = launchPoint.position;
        Vector2 startVelocity = _initialVelocity;

        // Убедимся, что positionCount равен trajectoryResolution
        if (lineRenderer.positionCount != rangeWeaponDate.numberPointsTrajectory)
        {
            lineRenderer.positionCount = rangeWeaponDate.numberPointsTrajectory;
        }

        // Рассчитываем и устанавливаем точки
        for (int i = 0; i < rangeWeaponDate.numberPointsTrajectory; i++)
        {
            float time = i * 0.06f;
            Vector2 point = startPos + startVelocity * time + 0.5f * Physics2D.gravity * time * time;
            lineRenderer.SetPosition(i, point);
        }

        lineRenderer.enabled = true;
    }
}