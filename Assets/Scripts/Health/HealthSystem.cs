using UnityEngine;

/*
 * Базовая система здоровья, которая может быть унаследована игроком, врагами.
 * Позволяет управлять процессом смерти
 */
public abstract class HealthSystem : MonoBehaviour
{
    [SerializeField] protected AudioSource clipAudioSourceHit;
    
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    
    /*
     * Инициализация здоровья при старте.
     * Устанавливает текущее здоровье равным максимальному.
     */
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }
    
    /*
      * Абстрактный метод получения урона.
      * Уменьшает текущее здоровье на указанное количество урона.
      * Если здоровье падает до 0 или ниже, вызывается метод Die().
      *
      * @param damage Количество урона, наносимого объекту.
      */
    public abstract void TakeDamage(float damage);

    /*
     * Абстрактный метод лечения.
     * Увеличивает текущее здоровье на указанное количество, но не выше максимального.
     *
     * @param amount Количество здоровья, на которое объект будет вылечен.
     */
    public abstract void Heal(float amount);

    /*
    * Абстрактный метод смерти, который должен быть реализован в дочерних классах.
    * Определяет, что происходит, когда объект умирает.
    */
    protected abstract void Die();
}