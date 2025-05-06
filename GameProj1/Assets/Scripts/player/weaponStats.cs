using UnityEngine;

[CreateAssetMenu(fileName = "weaponStats", menuName = "Weapon/weaponStats")]
public class weaponStats : ScriptableObject
{
    [Header("*** Components ***")]
    public GameObject model;

    [Header("*** Information ***")]
    public bool isMelee;
    public bool isAutomatic;

    [Header("*** Universal ***")]
    public int range;
    public float attackRate;
    public float damage;

    [Header("*** Ranged ***")]
    public int currentAmmo;
    public int maxAmmo;
}
