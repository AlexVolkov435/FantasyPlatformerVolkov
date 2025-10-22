using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] private float speed = 2f; 
    [SerializeField] private float moveDistance = 3f; 
    [SerializeField] private float damage;

    private Vector3 _startPosition; 
    private Vector3 _targetPosition; 
    
    private bool _movingRight = true;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private EnemyHealthSystem _healthSystem;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _healthSystem = GetComponent<EnemyHealthSystem>();
    }

    /*
     * Инициализирует начальные параметры моба при старте
     */
    private void Start()
    {
        _startPosition = transform.position; 
        _startPosition.z = -0.5f; 
        transform.position = _startPosition; 
        _targetPosition = new Vector3(_startPosition.x + moveDistance, _startPosition.y, _startPosition.z); // Устанавливаем целевую позицию
    }
    
    private void Update()
    {
        Move();
    }
    
    /* @param триггер,damage
     * Инициализирует начальные параметры моба при старте
     * @return количество урона
     */ 
    private void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<PlayerHealthSystem>(out PlayerHealthSystem enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
    
    /* @param transform.position, _targetPosition
     * Метод передвижения противника
     * передвижение противника
     */ 
    private void Move()
    {
        if (!_healthSystem.isDeath)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
           
            _animator.SetBool("isRun", true);
            
            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                if (_movingRight)
                {
                    _targetPosition = new Vector3(_startPosition.x - moveDistance, _startPosition.y, _startPosition.z);
                    _spriteRenderer.flipX = true;
                }
                else
                {
                    _targetPosition = new Vector3(_startPosition.x + moveDistance, _startPosition.y, _startPosition.z);
                    _spriteRenderer.flipX = false;
                }
                
                _movingRight = !_movingRight;
            }
        }
    }
}
