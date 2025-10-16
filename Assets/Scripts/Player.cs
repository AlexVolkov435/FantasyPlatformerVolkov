using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed =  5f;
    [SerializeField] private float jumpForce =  8f;
    [SerializeField] private LayerCheck layerCheck;
    
    private Rigidbody2D _rigidbody2D; 
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer  = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        Move();
        Jump();
    }
    
    private void Move()
    {
        if (Input.GetButton("Horizontal"))
        {
            Vector3 dir = transform.right * Input.GetAxis("Horizontal");
            
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir,
                speed * Time.deltaTime);
            
            _spriteRenderer.flipX =  dir.x < 0.0f; 
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
    }
    
    /*
     * Метод обнуружения земли
     * @return  инфрмацию о столкновении с землей
     */
    private bool IsGrounded()
    {
        return layerCheck.IsTouchingLayer;
    }
}