using System;
using Mirror;
using Mirror.Core;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private void Awake()
    {
        if(!isLocalPlayer)
            return;
        
        Debug.Log("Sending ID to server");
    }
}