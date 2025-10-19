using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int _numberOfCoins;

    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        //_numberOfCoins = TravelerManager.Instance.CoinsCount;
        RefreshCoin();
    }

    /*
     * Добавление монет
     * @param количество монет
     * @return сохраняет монеты в переменную_ numberOfCoins, RefreshCoin();
     */

    public void Add(int coin)
    {
        _numberOfCoins += coin;
        RefreshCoin();
    }

    /*
     * Запись в поле с монетами
     * @return записывает в поле интерфейса количество монет
     */

    private void RefreshCoin() 
    {
        text.text = "Gold " + _numberOfCoins.ToString(); //Обновление поля с монетами
    }

    /*
     * Сохранение прогресса
     * @return записывает в поле прогресса количество монет
     */
    public void SaveToProgress()
    {
//        TravelerManager.Instance.CoinsCount = _numberOfCoins;
    }
}