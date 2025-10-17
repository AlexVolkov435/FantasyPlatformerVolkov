using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/NewMeleeWeapon")]
public class MeleeWeaponData : ScriptableObject {
    public string weaponName;
    public Sprite icon;
    public float damage;
    public float attackSpeed;
    public float rangeAttack;
    public float shootCooldown;
}
