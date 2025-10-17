using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed =  3f;
    [SerializeField] private float jumpForce =  8f;
    [SerializeField] private LayerCheck layerCheck;
    
    private const string Horizontal = nameof(Horizontal);
    private Rigidbody2D _rigidbody2D; 
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Vector3 _direction;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer  = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (IsGrounded()) State = States.Idle;
        
        СhooseAnimation();
        
        Move();
        Jump();
    }
    
    private void Move()
    {
        if (Input.GetButton(Horizontal))
        {
            if (IsGrounded()) State = States.Run;
            Vector3 direction = transform.right * Input.GetAxis(Horizontal);
            
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction,
                speed * Time.deltaTime);
            
            _spriteRenderer.flipX =  direction.x < 0.0f;
            
        }
    }
    
    /*
     * Метод прыжка игрока
     * @param 
     * @return 
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
     * Метод выбора анимации между бегом и состоянием покоя
     * @param _direction.x
     * @return анимация
     */
    private void СhooseAnimation()
    {
        
        //_animator.SetBool("isJump", IsGrounded());
    }
    
    /*
     * Метод обнуружения земли
     * @return  инфрмацию о столкновении с землей
     */
    private bool IsGrounded()
    {
        return layerCheck.IsTouchingLayer;
    }
    
    private States State
    {
        get { return (States)_animator.GetInteger("State"); }
        set { _animator.SetInteger("State", (int)value); }
    }
    
    public enum States
    {
        Idle,// 0
        Run,// 1
        Jump// 2
    }
}