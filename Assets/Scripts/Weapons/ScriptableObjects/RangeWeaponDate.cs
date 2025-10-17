using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/NewRangeWeapon")]
public class RangeWeaponDate : ScriptableObject {
    public string weaponName;
    public Sprite icon;
    public float attackSpeed;
    public float shootCooldown;
    public float angle;
    public float projectileSpeed;
    public int numberPointsTrajectory;
}