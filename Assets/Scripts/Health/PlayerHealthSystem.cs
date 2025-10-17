using UnityEngine;
using System.Collections;

/*
 * Система здоровья для игрока.
 * Наследуется от HealthSystem и реализует методы
 */
public class PlayerHealthSystem : HealthSystem
{
    public ReloadLevelComponent reloadLevelComponent;
    private Animator _animator;
    
    private string _deathAnimationName = "Death";
   // private DamageRendering _damageRendering;
    
    private void Awake()
    {
        Initialization();
    }
    
    /*
     * Инициализация переменных
     */
    private void Initialization()
    {
        _animator = GetComponent<Animator>();
       // _damageRendering = GetComponent<DamageRendering>();
    }
    
    
    /*
    * Метод получения урона без учета статов
    * Уменьшает текущее здоровье на указанное количество урона.
    * Если здоровье падает до 0 или ниже, вызывается метод Die().
    * @param damage Количество урона, наносимого объекту.
    */
    public override void TakeDamage(float damage)
    {
        CalculateDamage(damage);
    }

    private void CalculateDamage(float damage)
    {
        _currentHealth -= damage;
        
        _animator.SetTrigger("Hit");
        
       // _damageRendering.ShowDamageText(damage);
     
        Debug.Log($"Player took {damage} damage. Current health: {_currentHealth}");
        
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }
    
    /*
     * Метод лечения.
     * Увеличивает текущее здоровье на указанное количество, но не выше максимального.
     *
     * @param amount Количество здоровья, на которое объект будет вылечен.
     */
    public override void Heal(float amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        Debug.Log($"Player healed by {amount}. Current health: {_currentHealth}");
    }

    /*
    * Метод, вызываемый при смерти игрока.
    */
    protected override void Die()
    {
        StartCoroutine(DyingCoroutine());
    }
    
    /*
     * Корутина, которая делает задержку равную времени анимации
     * @param имя тригера, компонент Rigidbody2D,Collider2D
     * @return время анимации, уничтожает объект
     */
    private IEnumerator DyingCoroutine()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        
        _animator.SetTrigger("Death");
        
        float AnimationLength = GetAnimationLength(_deathAnimationName);
        
        yield return new WaitForSeconds(AnimationLength);
        
        reloadLevelComponent.Reload();
    }

    /*
     * Метод который возвращает длину анимации
     * @param animName
     * @return длина анимации
     */
    private float GetAnimationLength(string animName)
    {
        RuntimeAnimatorController ac = _animator.runtimeAnimatorController;
        
        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == animName)
                return clip.length;
        }
        
        return 1f; // Значение по умолчанию, если анимация не найдена
    }
}