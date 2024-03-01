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
    [SerializeField] private Light spotlight;
    [SerializeField] private new Camera camera;
    public Character Character => character;
    [SyncVar] public TurnManager TurnManager;

    private void Start()
    {
        if (isServer)
        {
            character.location = Map.GetInstance().Nodes[0];
            character.location.character = character;
            spotlight.spotAngle = Mathf.Atan((character.sense + 0.5f) / spotlight.transform.position.y) * (180 / Mathf.PI) * (spotlight.range / spotlight.transform.position.y);
            spotlight.innerSpotAngle = spotlight.spotAngle;
            character.healthbar = Hud.GetInstance().GetHealthBar();
            EventBus<OnStartTurn>.OnEvent += OnStartTurn;
            EventBus<OnPlayerJoinedServer>.Publish(new OnPlayerJoinedServer(this));
        }

        if (isLocalPlayer)
        {
            if (Camera.main != null)
            {
                camera = Camera.main;
                Camera.main.transform.SetParent(transform);
            }
            Hud.GetInstance().Setup(this);
            EventBus<OnPlayerJoinedLocal>.Publish(new OnPlayerJoinedLocal(this));
        }
    }

    private void Update()
    {
        if(TurnManager.ActiveTurnTaker != this)
            return;
        
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

    public override void TakeTurn()
    {
        Debug.Log("Reseting movement");
        character.remainingSpeed = character.speed;
    }
}