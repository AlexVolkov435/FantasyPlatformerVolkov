using UnityEngine;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private HealthPotion healthPotionPrefab;
    [SerializeField] private Coin goldPrefab;
    [SerializeField] private CoinManager coinManager;
    
    private int _positionX = 2;
    private int _positionY = 2;

    public static LootSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    /* В методе выбирается тип бонуса который находиться в ящике
     * @param переменная isPlayer, счетчик открываний ящика, тип бонуса
     * @return тип бонуса, вызов методов  coinManager.Add, playerHealthSystem.Heal
     */
    public void ChooseTypeBonus(Vector3 position,bool isPlayer, ref int numberCounter, BonusType type)
    {
        if (isPlayer)
        {
            if (type == BonusType.Gold)
            {
                AddGoldToScene(position);
                numberCounter++;
            }

            if (type == BonusType.Hp)
            {
                AddHealthPotionToScene(position);
                numberCounter++;
            }
        }
    }
    
    /*
     * Метод добавляет на сцену количество единиц золота
     * @param позиция объекта количество единиц золота
     * @return добавляется золото на сцену
     */
    private void AddGoldToScene(Vector3 position)
    {
        Vector3 positionObject = new Vector2(position.x + _positionX, position.y + _positionY);

        Instantiate(goldPrefab, positionObject, Quaternion.identity);
    }

    /*
     * Метод добавляет на сцену зелье
     * @param позиция объекта количество единиц жизни
     * @return добавляется зелье на сцену
     */
    private void AddHealthPotionToScene(Vector3 position)
    {
        Vector3 positionObject = new Vector2(position.x + _positionX, position.y + _positionY);

        Instantiate(healthPotionPrefab, positionObject, Quaternion.identity);
    }
}
