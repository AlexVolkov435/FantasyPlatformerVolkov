using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject _currentPlayer;
    
    public bool IsFacingRight { get; private set; }
    
    
    private void Start()
    {
        if (_currentPlayer == null)
        {
            _currentPlayer = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        ChooseTurn();
    }
    

    /* метод выбора направления поворота спрайта
     * @param _isFacingRight,transform.position.x,player.transform.position.x
     * @return Flip
     */
    private void ChooseTurn()
    {
        if (IsFacingRight && transform.position.x < _currentPlayer.transform.position.x)
        {
            Flip();
        }
        else if (!IsFacingRight && transform.position.x > _currentPlayer.transform.position.x)
        {
            Flip();
        }
    }
    
    /* Метод поворота спрайта
     * @param _isFacingRight,
     * @return transform.localScale
     */
    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
