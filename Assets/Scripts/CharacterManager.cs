using System;
using System.Collections.Generic;
using Mirror;
using Unity.Mathematics;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private List<Character> characters;
    [SerializeField] private DataRegistry dataRegistry;
    
    [Server]
    public void MakeAttack(int pAttacker, int pTarget)
    {
        
    }
}