using System;
using System.Linq;
using EventBus;
using Mirror;
using PlayerActions;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : CanTakeTurn
{
    [SerializeField] private MoveAction move;
    [SerializeField] private AttackAction attack;
    [SerializeField] private PlayerAction selectedAction;
    [SerializeField] private Node targetedNode;
    [SerializeField, SyncVar] private Character character;
    [SerializeField] private new Camera camera;
    public Character Character => character;

    private void Awake()
    {
        if(isServer)
            EventBus<OnStartTurn>.OnEvent += OnStartTurn;
    }

    private void Start()
    {
        if (isServer)
        {
            character.location = Map.GetInstance().Nodes[0];
            EventBus<OnPlayerJoinedServer>.Publish(new OnPlayerJoinedServer(this));
        }

        if (isLocalPlayer)
        {
            if (Camera.main != null) Camera.main.transform.SetParent(transform);
            EventBus<OnPlayerJoinedLocal>.Publish(new OnPlayerJoinedLocal(this));
        }
    }

    private void Update()
    {
        if(isLocalPlayer)
            OwnerOnlyUpdate();
    }

    private void OwnerOnlyUpdate()
    {
        GetTargetedTile();
        
        SetSelectedAction();

        UseAction();
    }

    private void GetTargetedTile()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        targetedNode = null;

        if(!Physics.Raycast(ray, out hit))
            return;

        Node target = hit.collider.gameObject.GetComponent<Node>();

        if (target == null)
            return;

        targetedNode = target;
    }

    private void UseAction()
    {
        if (selectedAction == null)
            return;
        
        if(targetedNode == null)
            return;
        
        if (!Input.GetMouseButtonDown(0))
            return;
        
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Node[] targets = selectedAction.PotentialTargets(character.location);
        
        if(targets.Length == 0)
            return;
        
        if(!targets.Contains(targetedNode))
            return;
        
        selectedAction.PerformAction(targetedNode, character);
    }

    private void SetSelectedAction()
    {
        if(targetedNode == null)
            return;
        
        if (targetedNode.character == null || !character.location.connections.Contains(targetedNode))
            selectedAction = move;
        else
            selectedAction = attack;
    }

    private void OnStartTurn(OnStartTurn onStartTurn)
    {
        
    }

    public override void TakeTurn()
    {
        
    }
}