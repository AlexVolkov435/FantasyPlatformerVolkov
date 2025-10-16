using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    
    private Collider2D _collider;
   

    private bool _playerInSpikeTrigger = false;
    private bool _isFirstTouch = false;

    public bool IsTouchingLayer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    /*
     * Вызывается при столкновении с тригером
     * @param проверяется первое касание с объектом на котором есть скрипт Ground
     * @ return передаются в метод GetValue переменные _playerInSpikeTrigger, _isFirstTouch
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer);
    }
}
