using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public List<Node> FindPath(Node pFrom, Node pTo)
    {
        //the lists to keep track of the found path and all the nodes that still need to be checked
        List<Node> path = new List<Node>();
        List<Node> todo = new List<Node>();

        //this dictionary keeps track of every nodes parent
        Dictionary<Node, Node> parents = new Dictionary<Node, Node>();

        //the boolean used to control the exploration section
        bool pathFound = false;
        
        //add all the nodes neighboring the starting node
        todo.AddRange(pFrom.connections);
        
        //loop over all the neighbors of the start node to check if
        //it is the end node, this caused problems in the other section.
        foreach (Node node in pFrom.connections)
        {
            parents.Add(node,pFrom);
            if (node == pTo)
            {
                path.Add(pFrom);
                path.Add(pTo);
                
                return path;
            }
        }
        
        //keep looping while no path has been found
        while (!pathFound)
        {
            if (path.Count != 0)
            {
                pathFound = true;
                continue;
            }

            List<Node> newTodo = new List<Node>();

            while (todo.Count>0)
            {
                //loop over all the connected nodes to the current one
                foreach (Node node1 in todo[0].connections)
                {
                    
                    if(node1.character != null) continue;
                    //if this node already has a parent (which means it has already been checked) set continue
                    if(parents.ContainsKey(node1)) continue;
                    
                    //add the node to the parents dictionary
                    parents.Add(node1,todo[0]);
                    //add the node to todo list
                    todo.Add(node1);
                    
                    //check if it is the end node
                    if (node1 == pTo)
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
        while (path[path.Count-1]!= pFrom)
        {
            //add the node that is the parent of the last node to the end of the list
            path.Add(parents[path[path.Count-1]]);
        }
        
        //put the path in the correct order.
        path.Reverse();

        if (path[^1].character != null)
            path.Remove(path[^1]);
            
        return path;
    }
}