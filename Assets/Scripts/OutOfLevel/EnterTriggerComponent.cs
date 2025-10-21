using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private UnityEvent _action;
    
    /*
     * При столкновении с тригерром с тегом Player вызывается событие.
     * @param _tag
     * @return вызов события
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag))
        {
            _action?.Invoke();   
        }
    }
}
