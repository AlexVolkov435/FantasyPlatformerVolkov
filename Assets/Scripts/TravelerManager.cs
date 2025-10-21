using UnityEngine;

public class TravelerManager : MonoBehaviour
{
    public static TravelerManager Instance;
    
    // можно дописать еще что нужно сохронять глобально
    public int CoinsCount;
    
    /*
     * Сохранение прогреса объекта на сцене
     * @return записывает в поле прогреса количество монет
     */
    private void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);// не уничтожается объект при переходе между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }
}