using System;
using System.Linq;
using EventBus;
using Mirror;
using PlayerActions;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

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
            character.transform.position = character.location.transform.position;
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
        if (TurnManager.ActiveTurnTaker != this)
            return;
        
        if(isLocalPlayer)
            OwnerOnlyUpdate();
    }

    private void OwnerOnlyUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("Canceling update");
            //return;
        }
        
        GetTargetedTile();
        
        SetSelectedAction();

        UseAction();
    }

    private void GetTargetedTile()
    {
        if (Input.mousePosition.x > 1344+256 && Input.mousePosition.y < 589-128-64)
        {
            targetedNode = null;
            return;
        }
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
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            return;
        
        if (character.Mover.isMoving)
            return;

        if (character.isAttacking)
            return;

        if (selectedAction == null)
            return;
        
        if(targetedNode == null)
            return;

        /*
        if (!Input.GetMouseButtonUp(0))
            return;
        */
        /*
        if (Input.touchCount > 0 && !TappedOnUi())
            return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        */
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended)
            return;



        Node[] targets = selectedAction.PotentialTargets(character.location);
        if(targets.Length == 0)
            return;
        
        if(!targets.Contains(targetedNode))
            return;

        selectedAction.PerformAction(targetedNode, character);
    }

    private bool TappedOnUi()
    {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.GetTouch(0).position;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        // return results.Count > 0;
        foreach (var item in results)
        {
            if (item.gameObject.CompareTag("UI"))
            {
                return true;
            }
        }
        return false;
    }

    private void SetSelectedAction()
    {
        if(targetedNode == null)
            return;
        
        if (targetedNode.character == null)
            selectedAction = move;
        else
        {
            if (Vector3.Distance(transform.position, targetedNode.transform.position) <= character.weapon.Range)
            {
                selectedAction = attack;
            }
        }
    }

    public override void TakeTurn()
    {
        character.remainingSpeed = character.speed;
    }
}