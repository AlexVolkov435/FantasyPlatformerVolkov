using System.Collections;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    [SerializeField] private float pause;

    private IEnumerator _IEnumerator;

    /*
     * Вызывается при столкновении с физическим объектом
     * @param проверяется первое касание с объектом на котором есть скрипт Player
     * если на объекте есть скрипт Player и если столкновениие произошло сверху платформы, то запускается Корутина
     * @return запуск StartCoroutine
     */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector2 collisionNormal = contact.normal;

            if (collisionNormal.y < 0) // Столкновение сверху
            {
                _IEnumerator = Counter();
                StartCoroutine(_IEnumerator);
            }
        }
    }

    /*
     * Корутина, которая через определенное времяуничтожает объект
     * @return время задержки перед уничтожением объекта и метод Destroy
     */

    private IEnumerator Counter()
    {
        var wait = new WaitForSeconds(pause);

        yield return wait;

        Destroy(gameObject);
    }
}