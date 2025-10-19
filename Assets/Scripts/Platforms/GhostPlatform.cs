using System.Collections;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    [SerializeField] private float pause;

    private SpriteRenderer _platformRenderer;
    private BoxCollider2D _platformCollider;

    private IEnumerator _IEnumerator;

    /*
     * В методе инициализируются переменные
     * @param пременная _platformRenderer, _platformCollider, _IEnumerator
     * @return StartCoroutine
     */
    private void Start()
    {
        _platformRenderer = GetComponent<SpriteRenderer>();
        _platformCollider = GetComponent<BoxCollider2D>();

        _IEnumerator = Counter();
        StartCoroutine(_IEnumerator);
    }

    /*
     * Корутина, в которой через определенное время включается и выключается объект
     * @return время задержки перед отключением объекта, методы DisableGameObject EnableGameObject
     */
    private IEnumerator Counter()
    {
        var wait = new WaitForSeconds(pause);

        while (true)
        {
            if (gameObject != null)
            {
                DisableGameObject();
                yield return wait;

                EnableGameObject();
                yield return wait;
            }
            else
            {
                Debug.Log("Объект не найден.");
            }
        }
    }

    /*
     * метод в котором включаются объекты
     * @return включение объектв
     */
    private void DisableGameObject()
    {
        _platformRenderer.enabled = true;
        _platformCollider.enabled = true;
    }

    /*
     * метод в котором выключаются объекты
     * @return выключение объектв
     */
    private void EnableGameObject()
    {
        _platformRenderer.enabled = false;
        _platformCollider.enabled = false;
    }
}