using UnityEngine;
using TankShooter.Movement;
using UnityEngine.AI;
using System.Collections;

namespace TankShooter.Control
{
    public class AIDriverController : MonoBehaviour
    {
        [Header("Pathfinding")]
        [SerializeField] Transform waypoint;
        [SerializeField] float pathRefreshRate = 0.25f;

        [Header("Tolerances")]
        [SerializeField] float waypointTolerance = 0.1f;
        [SerializeField] float turnTolerance = 0.01f;

        TankMove tankMove;
        NavMeshAgent navMeshAgent;
        NavMeshPath currentPath;
        Vector3 nextWaypoint;

        private void Start()
        {
            tankMove = GetComponentInChildren<TankMove>();            
        }

        private void Update()
        {
            bool isTurning = tankMove.TurnTankTowards(waypoint.position, turnTolerance);

            if (!isTurning)
            {
                tankMove.MoveTank(1f);
            }
        }

        IEnumerator MoveControl()
        {
            while(true)
            {
                currentPath = new NavMeshPath();
                NavMesh.CalculatePath(transform.position, waypoint.position, NavMesh.AllAreas, currentPath);
                
                yield return new WaitForSeconds(pathRefreshRate);
            }
        }

        private bool AtWaypoint(Vector3 waypoint)
        {
            Vector2 tankPos = new Vector3(transform.position.x, transform.position.z);
            Vector2 waypointPos = new Vector3(waypoint.x, waypoint.z);

            float distance = Vector2.Distance(tankPos, waypointPos);

            if (distance < waypointTolerance) return true;

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            if (currentPath == null) return;

            for (int i = 0; i < currentPath.corners.Length; i++)
            {
                Gizmos.DrawSphere(currentPath.corners[i], 1f);

                if (i < currentPath.corners.Length - 1)
                {
                    Gizmos.DrawLine(currentPath.corners[i], currentPath.corners[i + 1]);
                }                
            }
        }
        
    }
}

