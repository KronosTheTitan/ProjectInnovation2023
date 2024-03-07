﻿using System;
using EventBus;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private Node[] path;
    [SerializeField] private float progress;
    [SerializeField] private Character character;
    [SerializeField] private float speed = 1;
    [SerializeField] public bool isMoving;
    [SerializeField] private int index;
    [SerializeField] private int lastIndex;
    [SerializeField] private AudioSource walkingSound;
    [SerializeField] private float startOfMovement;

    public void StartMovement(Node[] pPath)
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        Debug.Log(EventSystem.current.IsPointerOverGameObject());
        Debug.Log("Starting mover");
        
        if(character.remainingSpeed <= 0)
            return;
        
        //Debug.Log("character has sufficient speed left");
        
        if(pPath.Length == 0)
            return;

        startOfMovement = Time.time;
        //Debug.Log("path length is not 0");

        EventBus<OnCharacterStartMoving>.Publish(new OnCharacterStartMoving(character));

        index = 0;
        lastIndex = 0;
        path = pPath;
        progress = 0;
        isMoving = true;
    }

    private void StopMovement()
    {
        walkingSound.mute = true;
        //Debug.Log("Stopping mover");
        isMoving = false;
        EventBus<OnCharacterStopMoving>.Publish(new OnCharacterStopMoving(character));
    }

    private void Awake()
    {
        EventBus<OnEndTurn>.OnEvent += OnEndTurn;
        walkingSound.Play();
        walkingSound.mute = true;
    }

    private void Update()
    {
        Move();
        //Debug.Log(walkingSound.isPlaying);
    }
    
    private void OnEndTurn(OnEndTurn onEndTurn)
    {
        if(!isMoving)
            return;

        if (Time.time - startOfMovement < 0.1f)
        {
            character.location.character = null;
            character.location = path[0];
            character.location.character = character;

            character.transform.position = path[0].transform.position;
            
            StopMovement();
            return;
        }

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
        
        //Debug.Log("end of turn");
        StopMovement();
    }

    private void Move()
    {
        if(!isMoving)
            return;

        walkingSound.mute = false;

        progress += speed * Time.deltaTime;
        Vector3 newPos = GetPointOnPath(progress);
        RotateTowardsDestination(newPos);
        if (newPos == transform.position)
        {
            character.location.character = null;
            character.location = path[^1];
            character.location.character = character;
            //Debug.Log("Reached end");
            StopMovement();
        }
        else
        {
            transform.position = newPos;
        }
    }

    private void RotateTowardsDestination(Vector3 pNewPos)
    {
        Vector3 direction = (pNewPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * character.rotationSpeed);
    }

    private Vector3 GetPointOnPath(float inputT)
    {
        
        if (character.remainingSpeed == 0)
        {
            character.location.character = null;
            character.location = path[index];
            character.location.character = character;

            //Debug.Log("ran out of speed");
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