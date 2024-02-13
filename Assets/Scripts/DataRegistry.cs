using UnityEngine;

[CreateAssetMenu(fileName = "Data Registry", menuName = "Project Innovation/DataRegistry")]
public class DataRegistry : ScriptableObject
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private Item[] items;
    [SerializeField] private HeadArmour[] headArmours;
    [SerializeField] private ChestArmour[] chestArmours;
    [SerializeField] private LegArmour[] legArmours;
    [SerializeField] private FeetArmour[] feetArmours;

    public Weapon[] Weapons => weapons;
    public Item[] Items => items;
    public HeadArmour[] HeadArmours => headArmours;
    public ChestArmour[] ChestArmours => chestArmours;
    public LegArmour[] LegArmours => legArmours;
    public FeetArmour[] FeetArmours => feetArmours;
}