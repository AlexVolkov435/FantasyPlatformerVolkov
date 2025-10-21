using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed =  3f;
    [SerializeField] private float jumpForce =  8f;
    [SerializeField] private LayerCheck layerCheck;
    [SerializeField] private TextMeshProUGUI textHP;
    
    private const string Horizontal = nameof(Horizontal);
    
    private Rigidbody2D _rigidbody2D; 
    private Animator _animator;
    private Vector3 _direction;
    private PlayerHealthSystem _playerHealthSystem;
    
    private bool _isFacingLeft;
    private bool _isFacingRight = true;
    private bool _isTrap;
    
    private int _layerPlatform = 10;
    private int _layerPlayer = 7;
    
    public bool IsFacingLeft { get { return _isFacingLeft; } set { _isFacingLeft = value; } }
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerHealthSystem = GetComponent<PlayerHealthSystem>();
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            GoDownPlatform();
        }
        
        textHP.text = $"{_playerHealthSystem.CurrentHealth}/{_playerHealthSystem.MaxHealth}";
        
        if (IsGrounded()) State = States.Idle;
        
        Move();
        Jump();
        ChooseTurn();
        
        if (IsGrounded())
        {
            _isTrap = false;
        }
    }
    
    /*
     * @param CompareTag("Spike")
     * @return значение _isTrap
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Spike"))
        {
            _isTrap = true;
        }
    }
    
    /*
     * Метод передвижения
     * @param Horizontal
     * @return  transform.position
     */
    private void Move()
    {
        if (Input.GetButton(Horizontal))
        {
            if (IsGrounded()) State = States.Run;
           
            Vector3 direction = transform.right * Input.GetAxis(Horizontal);
           
            _direction = direction;
            
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction,
                speed * Time.deltaTime);
        }
    }
    
    /*
     * Метод прыжка игрока
     * @param jumpForce
     * @return прыжок
     */
    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        _animator.SetBool("isJump", IsGrounded());
    }
    
    /*
     * Метод выбора поворота игрока
     * @param _direction.x _isFacingRight
     * @return выбор стороны поворота
     */
    private void ChooseTurn()
    {
        if (!_isFacingRight && _direction.x > 0f)
        {
            IsFacingLeft = false;
            Flip();
        }
        else if (_isFacingRight && _direction.x < 0f)
        {
            IsFacingLeft = true;
            Flip();
        }
    }
    
    /*
     * Метод поворота по направления движения игрока
     * @param вектор localScale для изменения поворота игрока
     * @return  transform.localScale
     */
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    
    /*
     * Метод обнуружения земли
     * @return  инфрмацию о столкновении с землей
     */
    private bool IsGrounded()
    {
        return layerCheck.IsTouchingLayer;
    }
    
    /*
     * Метод передачи значений в аниматор
     * @return  значения для аниматора
     */
    private States State
    {
        get { return (States)_animator.GetInteger("State"); }
        set { _animator.SetInteger("State", (int)value); }
    }
    
    /*
     * Метод прохождение платформы насквозь
     * @param ссылка из PlayerInputReader()
     * @return  игнороируем слои платформы и игрока, чтобы пройти сквозь платформу
     */
    public void GoDownPlatform()
    {
        float pause = 0.5f;

        Physics2D.IgnoreLayerCollision(_layerPlatform, _layerPlayer, true);
        Invoke(nameof(IgnoreLayerOff), pause);// чтобы успеть пройти сквозь платформу
    }

    /*
     * Метод выключения игнорирования слоёв
     * @param ссылка из GoDownPlatform()
     * @return выключаем игнорирование слоев
     */
    private void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(_layerPlatform, _layerPlayer, false);
    }
    
    private enum States
    {
        Idle,// 0
        Run,// 1
    }
}