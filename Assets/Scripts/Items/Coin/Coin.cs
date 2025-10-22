using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int price;
    
    /*
     * Вызывается при столкновении с физическим объектом
     * @param other Коллайдер объекта
     * @return метод Add(передача количества монет в метод CoinManager)
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            FindObjectOfType<CoinManager>().Add(price);
            gameObject.SetActive(false);
        }
    }
}