using UnityEngine;

public class Armour : Item
{
    [SerializeField] private int defenceBonus;
    public int DefenceBonus => defenceBonus;
}