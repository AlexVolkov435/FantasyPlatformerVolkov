using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private float detectionRadius;
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(1f,3f)] private float shootRange; // Интервал времени (в секундах) между нанесением урона игроку
    
    private Animator _animator;
    private EnemyHealthSystem _enemyHealthSystem;
    private GameObject _currentPlayer;
    
    public bool IsStopRun { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyHealthSystem = GetComponent<EnemyHealthSystem>();
    }

    private void Start()
    {
        if (_currentPlayer == null)
        {
            _currentPlayer = GameObject.FindGameObjectWithTag("Player");
        }
    }

    /*
    * @return метод DetermineDistanceToPlayer
    */
    private void Update()
    {
        DetermineDistanceToPlayer();
    }
    
    /*
     * Проверяется дистанция до игрока
     * @param позиция игрока, позиция противника,радиус обнуружения,
     * @return анимации противника и значение при которым противник атакует игрока
     */
    private void DetermineDistanceToPlayer()
    {
        // Проверяем дистанцию до игрока
       
        float distanceToPlayer = Vector2.Distance(_currentPlayer.transform.position,transform.position );
        
        if (distanceToPlayer <= detectionRadius && !_enemyHealthSystem.isDeath)
        {
            if (distanceToPlayer <= shootRange)
            {
                IsStopRun = true;
                _animator.SetBool("isRun", !IsStopRun);
            }
            else
            {
                if (_currentPlayer.transform.position.y < transform.position.y)
                {
                    Chase();
                    _animator.SetBool("isRun", true);
                }
                else
                { 
                    IsStopRun = false;
                    _animator.SetBool("isRun", IsStopRun);
                }
            }
        }
    }
    
    /*
    * метод в котором происходит следование за героем
    * @return направление по которому враг следует за игроком
    */
    private void Chase()
    {
        Vector2 direction = (_currentPlayer.transform.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
