using System.Linq;
using Mirror;
using PlayerActions;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : NetworkBehaviour
{
    [SerializeField] private MoveAction move;
    [SerializeField] private AttackAction attack;
    [SerializeField] private PlayerAction selectedAction;
    [SerializeField] private Node targetedNode;
    [SerializeField, SyncVar] private Character character;
    public Character Character => character;

    private void Start()
    {
        if (isServer)
        {
            character.location = Map.GetInstance().Nodes[0];
            character.location.character = character;
            EventBus<OnStartTurn>.OnEvent += OnStartTurn;
        }

        if (isLocalPlayer)
        {
            Hud.GetInstance().Setup(this,move,attack);
            Camera.main.transform.SetParent(transform);
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

    public void SetSelectedAction(PlayerAction action)
    {
        selectedAction = action;
    }

    private void OnStartTurn(OnStartTurn onStartTurn)
    {
        SetSelectedAction(null);
    }
}