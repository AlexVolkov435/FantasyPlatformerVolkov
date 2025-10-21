using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private BonusType type;

    private bool _isPlayer;

    private int _numberCounter;
    private int _countOpen = 1;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            Open();
        }
    }

    /*
     * Вызывается при столкновении с физическим объектом
     * @param проверяется первое касание с объектом на котором есть скрипт Player
     * @return  переменной  _isPlayer присваивается значение true
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_numberCounter < _countOpen)
        {
            if (collision.gameObject.GetComponent<Player>())
            {
                _isPlayer = true;
            }
        }
    }

    /*
     * Вызывается при вызове события OpenChested 
     * @return  метод SetRandomObject, ChooseTypeBonus
     * переменной _isPlayer приравнивается значение false
     */
    private void Open()
    {
        LootSystem.Instance.ChooseTypeBonus(transform.position,_isPlayer, ref _numberCounter, type);
        
        _isPlayer = false;
    }
}