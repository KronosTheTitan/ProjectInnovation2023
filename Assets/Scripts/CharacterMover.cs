using System;
using EventBus;
using Mirror;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private Node[] path;
    [SerializeField] private float progress;
    [SerializeField] private Character character;
    [SerializeField] private float speed = 1;
    [SerializeField] public bool isMoving;
    [SerializeField] private int index;
    [SerializeField] private int lastIndex;

    public void StartMovement(Node[] pPath)
    {
        Debug.Log("Starting mover");
        
        if(character.remainingSpeed <= 0)
            return;
        
        Debug.Log("character has sufficient speed left");
        
        if(pPath.Length == 0)
            return;
        Debug.Log("path length is not 0");
        
        index = 0;
        lastIndex = 0;
        path = pPath;
        progress = 0;
        isMoving = true;
    }

    private void StopMovement()
    {
        Debug.Log("Stopping mover");
        isMoving = false;
    }

    private void Awake()
    {
        EventBus<OnEndTurn>.OnEvent += OnEndTurn;
    }

    private void Update()
    {
        Move();
    }
    
    private void OnEndTurn(OnEndTurn onEndTurn)
    {
        if(!isMoving)
            return;
        
        Debug.Log("Moving");
        
        while (character.remainingSpeed > 0)
        {
            if(index >= path.Length - 1)
                break;
            
            character.remainingSpeed--;
            index++;
            
            character.location.character = null;
            character.location = path[index];
            character.location.character = character;

            character.transform.position = path[index].transform.position;
        }
        
        Debug.Log("end of turn");
        StopMovement();
    }

    private void Move()
    {
        if(!isMoving)
            return;
        
        progress += speed * Time.deltaTime;
        Vector3 newPos = GetPointOnPath(progress);
        if (newPos == transform.position)
        {
            character.location.character = null;
            character.location = path[^1];
            character.location.character = character;
            Debug.Log("Reached end");
            StopMovement();
        }
        else
        {
            transform.position = newPos;
        }
    }

    private Vector3 GetPointOnPath(float inputT)
    {
        
        if (character.remainingSpeed == 0)
        {
            character.location.character = null;
            character.location = path[index];
            character.location.character = character;

            Debug.Log("ran out of speed");
            StopMovement();
                
            return path[index].transform.position;
        }
        
        index = (int)inputT;

        if (index > lastIndex)
        {
            character.remainingSpeed--;
        }
        
        lastIndex = index;
        
        float t = inputT - index;
        if (inputT > path.Length-1)
        {
            return path[^1].transform.position;
        }
        
        character.location.character = null;
        character.location = path[index];
        character.location.character = character;
        return Vector3.Lerp(path[index].transform.position, path[index + 1].transform.position, t);
    }
}