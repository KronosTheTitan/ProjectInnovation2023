using System;
using Mirror;
using UnityEngine;

public class PlayerMarker : NetworkBehaviour
{
    [SerializeField] private GameObject worldSpaceUI;
    private void Start()
    {
        if(isLocalPlayer)
            worldSpaceUI.SetActive(true);
            
    }
}
