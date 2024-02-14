using System;
using System.Collections.Generic;
using Items;
using Mirror;
using Mirror.Core;
using Unity.Mathematics;
using UnityEngine;

public class Character : NetworkBehaviour
{
    [Header("Stats")]
    [SerializeField, SyncVar] public int health;
    [SerializeField, SyncVar] public int speed;
    [SerializeField, SyncVar] public int remainingSpeed;
    [SerializeField, SyncVar] public int defence;
    [SerializeField, SyncVar] public int attack;
    [SerializeField, SyncVar] public int sense;

    [Header("Slots")]
    [SerializeField, SyncVar] public HeadArmour head;
    [SerializeField, SyncVar] public ChestArmour chest;
    [SerializeField, SyncVar] public LegArmour legs;
    [SerializeField, SyncVar] public FeetArmour feet;

    [SerializeField, SyncVar] public Weapon weapon;

    [SerializeField, SyncVar] public GridTile location;

    [Server]
    public void TakeDamage(int amount){

        int modifiedAmount = math.clamp(amount - GetTotalDefence(), 0, int.MaxValue);

        health -= modifiedAmount;
    }

    [Server]
    public void MakeAttack(Character target)
    {
        if(Vector3.Distance(transform.position , target.transform.position) > weapon.Range)
            return;

        int damage = attack + weapon.Damage;
        
        target.TakeDamage(damage);
    }

    [Server]
    public void Move(GridTile destination)
    {
        //the lists to keep track of the found path and all the nodes that still need to be checked
        List<GridTile> path = new List<GridTile>();
        List<GridTile> todo = new List<GridTile>();

        //this dictionary keeps track of every nodes parent
        Dictionary<GridTile, GridTile> parents = new Dictionary<GridTile, GridTile>();

        
        //the boolean used to control the exploration section
        bool pathFound = false;
        
        //add all the nodes neighboring the starting node
        todo.AddRange(location.adjacentTiles);
        
        //loop over all the neighbors of the start node to check if
        //it is the end node, this caused problems in the other section.
        foreach (GridTile node in location.adjacentTiles)
        {
            parents.Add(node,location);
            if (node == destination)
            {
                path.Add(location);
                path.Add(destination);
                
                CompleteMove(path);
            }
        }
        
        //keep looping while no path has been found
        while (!pathFound)
        {
            Console.WriteLine(todo.Count);
            if (path.Count != 0)
            {
                pathFound = true;
                continue;
            }

            List<GridTile> newTodo = new List<GridTile>();

            while (todo.Count>0)
            {
                //loop over all the connected nodes to the current one
                foreach (GridTile node1 in todo[0].adjacentTiles)
                {
                    //if this node already has a parent (which means it has already been checked) set continue
                    if(parents.ContainsKey(node1)) continue;
                    
                    //add the node to the parents dictionary
                    parents.Add(node1,todo[0]);
                    //add the node to todo list
                    todo.Add(node1);
                    
                    //check if it is the end node
                    if (node1 == destination)
                    {
                        //add the end node to the path
                        path.Add(node1);
                    }
                }
                
                //remove the current node from the todo list
                todo.Remove(todo[0]);
            }
        }

        //stay in loop until the start point has been reached
        //yes it will be in the wrong order, this was the easiest way to do it.
        while (path[path.Count-1]!= location)
        {
            //add the node that is the parent of the last node to the end of the list
            path.Add(parents[path[path.Count-1]]);
        }
        
        //put the path in the correct order.
        path.Reverse();
        CompleteMove(path);
    }

    private void CompleteMove(List<GridTile> path)
    {
        if (path.Count < remainingSpeed)
        {
            remainingSpeed -= path.Count;
            location = path[path.Count - 1];
            transform.position = location.transform.position;
            return;
        }
        
        for (int i = 0; remainingSpeed > 0; i++, remainingSpeed--)
        {
            location = path[i];
            remainingSpeed--;
        }
        
        transform.position = location.transform.position;

        remainingSpeed = 0;
    }

    private int GetTotalDefence()
    {
        int headDef = head.DefenceBonus;
        int chestDef = chest.DefenceBonus;
        int legsDef = legs.DefenceBonus;
        int feetDef = feet.DefenceBonus;

        int baseDef = defence;

        return baseDef + headDef + chestDef + legsDef + feetDef;
    }
}