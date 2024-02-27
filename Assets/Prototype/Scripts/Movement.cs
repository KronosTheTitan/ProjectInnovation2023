using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private GameObject movementTiles;
    [SerializeField]
    private float speed;

    private Grid grid;
    private NavMeshAgent agent;

    private void Start()
    {
        grid = movementTiles.GetComponentInParent<Grid>();
        if (grid == null)
        {
            throw new System.Exception("There is no Grid component in parent.");
        }

        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            throw new System.Exception("There is no NavMeshAgent component.");
        }
    }

    private void Update()
    {
        GatherTouchInput();
    }

    private void GatherTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = camera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    MoveTowardsTarget(grid.GetCellCenterWorld(grid.WorldToCell(hit.point)), speed);
                }
            }
        }

    }

    public void MoveTowardsTarget(Vector3 pTargetPosition, float pSpeed)
    {
        if (agent == null)
            return;

        agent.speed = pSpeed;
        agent.SetDestination(pTargetPosition);
    }
}
