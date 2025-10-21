using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    #region Variables

    [SerializeField] protected MeleeWeaponData meleeWeaponData;

    //Объявляем интерфейсы
    protected IAttackMelee AttackMeleeBehaviour;
    protected IBalisticAttack BalisticAttackBehaviour;
    protected IAnimate AnimateBehaviour;

    #endregion

    #region Interface init

    /*
     * Инициализация интерфеса IAttackMelee
    * @param интерфейс IAttackMelee
    * @return получаем поведения игрока(helper)
    */
    public void SetAttackMeleeBehaviour(IAttackMelee attackMeleeBehaviour)
    {
        AttackMeleeBehaviour = attackMeleeBehaviour;
    }
    
    /*
     * Инициализация интерфеса IBalisticAttack
    * @param интерфейс IBalisticAttack
    * @return получаем поведения игрока(helper)
    */
    public void SetAttackRangeBehaviour(IBalisticAttack balisticAttackBehaviour)
    {
        BalisticAttackBehaviour = balisticAttackBehaviour;
    }
    
    /*
    * Инициализация интерфеса IAnimate
    * @param интерфейс IAnimate
    * @return получаем поведения игрока(helper)
    */
    public void SetAnimate(IAnimate animateBehaviour)
    {
        AnimateBehaviour = animateBehaviour;
    }

    #endregion

    #region BaseMet

    /*
     * Реализация метода Attack
    * @return метод Attack через интерфейс IAttackMelee
    */
    protected void Attack()
    {
        AttackMeleeBehaviour.Attack();
    }
    
    /*
     * Реализация метода BalsticAttack
    * @return метод BalisticAttack через интерфейс IBalisticAttack
    */
    protected void BalisticAttack(Vector2 initialVelocity)
    {
        BalisticAttackBehaviour.BalisticAttack(initialVelocity);
    }
    
    /*
    * Реализация метода Animate
    * @return метод Animate через интерфейс IAnimate
    */
    protected void Animate()
    {
        AnimateBehaviour.Animate();
    }
   
    #endregion
}
