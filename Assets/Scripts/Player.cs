using System;
using System.Linq;
using Mirror.Core;
using PlayerActions;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerAction[] actions;
    [SerializeField, SyncVar] private PlayerAction selectedAction;
    [SerializeField] private Node targetedNode;
    [SerializeField, SyncVar] private Character character;

    private void Update()
    {
        if(isLocalPlayer)
            OwnerOnlyUpdate();
    }

    private void OwnerOnlyUpdate()
    {
        GetTargetedTile();

        UseAction();
    }

    private void GetTargetedTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        targetedNode = null;

        if(!Physics.Raycast(ray, out hit))
            return;

        Node target = hit.collider.gameObject.GetComponent<Node>();

        if (target == null)
            return;

        targetedNode = target;
        
        Debug.Log("GettingTargetedTile");
        Debug.Log(targetedNode.name);
    }

    private void UseAction()
    {
        if(targetedNode == null)
            return;
        
        if (!Input.GetMouseButtonUp(0))
            return;

        Node[] targets = selectedAction.PotentialTargets(character.location);
        
        if(targets.Length == 0)
            return;
        
        if(!targets.Contains(targetedNode))
            return;
        
        if (selectedAction == null)
            return;
        
        selectedAction.PerformAction(targetedNode, character);
    }
}