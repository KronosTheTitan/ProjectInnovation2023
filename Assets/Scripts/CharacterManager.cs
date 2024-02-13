using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private List<Character> characters;
    [SerializeField] private DataRegistry dataRegistry;
    
    public void MakeAttack(int attacker, int target)
    {
        
    }

    public void TakeDamage(int target, int amount)
    {
        Character character = characters[target];

        int modifiedAmount = math.clamp(amount - GetTotalDefence(target), 0, int.MaxValue);
    }

    public int GetTotalDefence(int target)
    {
        Character character = characters[target];

        int headDef = dataRegistry.;
        int chestDef;
        int legsDef;
        int feetDef;

        int baseDef = character.defence;

        return baseDef + headDef + chestDef + legsDef + feetDef;
    }
}