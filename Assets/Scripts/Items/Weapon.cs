using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Project Innovation/Items/Weapon")]
public class Weapon : Item
{
    [SerializeField] private int damage;
    [SerializeField] private float range;
    public int Damage => damage;
    public float Range => range;
}