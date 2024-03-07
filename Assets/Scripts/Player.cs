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
            EventBus<OnPlayerJoinedServer>.Publish(new OnPlayerJoinedServer(this));
        }

        if (isLocalPlayer)
        {
            if (Camera.main != null)
            {
                camera = Camera.main;
                camera.transform.parent.GetComponent<CameraFollow>().target = transform;
            }
            Hud.GetInstance().Setup(this);
            EventBus<OnPlayerJoinedLocal>.Publish(new OnPlayerJoinedLocal(this));
        } else
        {
            GameObject spotlight = GetComponentInChildren<Light>().gameObject;
            if (spotlight != null)
            {
                spotlight.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(isLocalPlayer)
            HideHealthBars();

        if (TurnManager.ActiveTurnTaker != this)
            return;
        
        if(isLocalPlayer)
            OwnerOnlyUpdate();
    }

    private void OwnerOnlyUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        GetTargetedTile();
        
        SetSelectedAction();

        UseAction();
    }

    private void HideHealthBars()
    {
        foreach (GameObject healthbar in GameObject.FindGameObjectsWithTag("EnemyHealthBar"))
        {
            if ((healthbar.gameObject.transform.position - transform.position).magnitude >= character.sense)
            {
                healthbar.transform.localScale = new Vector3(0, 0, 0);
            } else
            {
                healthbar.transform.localScale = new Vector3(-0.00075f, 0.0004f, 0);
            }
        }
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
        if (character.Mover.isMoving)
            return;

        if (character.isAttacking)
            return;

        if (selectedAction == null)
            return;
        
        if(targetedNode == null)
            return;
        
        if (!Input.GetMouseButtonUp(0))
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
        character.remainingSpeed = character.speed;
    }
}