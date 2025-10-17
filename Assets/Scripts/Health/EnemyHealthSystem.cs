using UnityEngine;
using System.Collections;

public class EnemyHealthSystem : HealthSystem
{
    private Animator _animator;
    private string _deathAnimationName = "Death";
   // private DamageRendering _damageRendering;
    
    public bool isDeath { get; private set; }
    
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
        //_damageRendering = GetComponent<DamageRendering>();
    }
    
    /*
    * Метод получения урона без учета статов
    * Уменьшает текущее здоровье на указанное количество урона.
    * Если здоровье падает до 0 или ниже, вызывается метод Die().
    *
    * @param damage Количество урона, наносимого объекту.
    */
    public override void TakeDamage(float damage)
    {
        _currentHealth -= damage;

       
       _animator.SetTrigger("Hit");
        
        
       // _damageRendering.ShowDamageText(damage);
        
        Debug.Log($"Enemy took {damage} damage. Current health: {_currentHealth}");
        
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }
    
    /**
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
        
        Debug.Log($"Enemy healed by {amount}. Current health: {_currentHealth}");
    }

    /**
    * Метод смерти.
    */
    protected override void Die()
    {
        Debug.Log("Enemy died");

        StartCoroutine(DyingCoroutine());
        isDeath = true;
            //LootSystem.Instance.AddGoldToSceneDroppedEnemy(transform.position,_isDestroyObject);!!!!!!!!!!!!
        //LootSystem.Instance.TryDropGold(transform.position, mob.lootData);
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

        float animationLength = 1;
        
        _animator.SetTrigger("Death");
        
         animationLength = GetAnimationLength(_deathAnimationName);
         
        yield return new WaitForSeconds(animationLength);
        
        Destroy(gameObject);
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