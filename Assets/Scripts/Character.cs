using System;
using UnityEngine;

[Serializable]
public class Character
{
    [Header("Stats")]
    public int health;
    public int speed;
    public int defence;
    public int attack;
    public int sense;

    [Header("Slots")]
    public int head;
    public int chest;
    public int legs;
    public int feet;

    public int weapon;

    public int location;
}