using UnityEngine;
using TankShooter.Movement;
using UnityEngine.AI;
using System.Collections;
using System;

namespace TankShooter.Control
{
    public class AIDriverController : MonoBehaviour
    {
        [SerializeField] float sightRange = 10f;

        [Header("Pathfinding")]
        [SerializeField] float pathRefreshRate = 0.25f;
        [SerializeField] float maxWanderRange = 5f;
        [SerializeField] float maxWaitTime = 10f;

        [Header("Tolerances")]
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float turnTolerance = 0.01f;

        TankMove tankMove;
        Transform target;
        Vector3[] currentWaypoints;
        Coroutine traversePath;

        private void Start()
        {
            tankMove = GetComponentInChildren<TankMove>();

            StartCoroutine(PathRefresh());
        }

        IEnumerator PathRefresh()
        {
            while(true)
            {
                yield return new WaitForSeconds(pathRefreshRate);
                ScanForPlayer();

                if (traversePath != null) continue;

                NavMeshPath newPath = new NavMeshPath();
                Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
                NavMesh.CalculatePath(pos, GetDestination(), NavMesh.AllAreas, newPath);

                if (newPath.status == NavMeshPathStatus.PathInvalid) continue;

                currentWaypoints = newPath.corners;                
                traversePath = StartCoroutine(TraversePath());              
            }
        }

        private void ScanForPlayer()
        {
            int layerMask = 1 << 9;

            Collider[] targets = Physics.OverlapSphere(transform.position, sightRange, layerMask);
            if (targets.Length == 0) return;

            target = targets[0].transform;
        }

        IEnumerator TraversePath()
        {
            int waypointIndex = 0;

            while (waypointIndex < currentWaypoints.Length)
            {
                Vector3 currentWaypoint = currentWaypoints[waypointIndex];

                bool isTurning = TurnTankTowards(currentWaypoint);

                if (!isTurning)
                {
                    bool isMoving = MoveTankTowards(currentWaypoint);
                    if (!isMoving) waypointIndex++;
                }

                yield return null;
            }

            float waitTime = UnityEngine.Random.Range(0, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            traversePath = null;
        }

        private Vector3 GetDestination()
        {
            Vector3 location;

            if (target != null)
            {
                location = target.position;
            }
            else
            {
                location = (UnityEngine.Random.insideUnitSphere * maxWanderRange) + transform.position;
            }
            

            NavMesh.SamplePosition(location, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas);
            return hit.position;
        }

        //private bool IsDifferentPath(Vector3[] newWaypoints)
        //{
        //    if (currentWaypoints == null || currentWaypoints.Length != newWaypoints.Length) return true;

        //    for (int i = 0; i < newWaypoints.Length; i++)
        //    {
        //        if (newWaypoints[i] != currentWaypoints[i]) return true;
        //    }

        //    return false;
        //}
        
        // Returns true when tank is not facing the target point
        public bool TurnTankTowards(Vector3 point)
        {
            Vector3 difference = point - transform.position;

            float dot = Vector3.Dot(transform.right, difference.normalized);

            if (Mathf.Abs(dot) < turnTolerance) return false;

            tankMove.TurnTank(Mathf.Sign(dot));
            return true;
        }

        // Returns true when tank is not at the waypoint
        public bool MoveTankTowards(Vector3 waypoint)
        {
            Vector3 difference = waypoint - transform.position;

            if (AtWaypoint(waypoint)) return false;

            tankMove.MoveTank(Mathf.Sign(difference.magnitude));
            return true;
        }

        private bool AtWaypoint(Vector3 waypoint)
        {
            Vector2 tankPos = new Vector3(transform.position.x, transform.position.z);
            Vector2 waypointPos = new Vector3(waypoint.x, waypoint.z);

            float distance = Vector2.Distance(tankPos, waypointPos);

            if (distance < waypointTolerance) return true;

            return false;
        }



        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            if (currentWaypoints == null) return;

            for (int i = 0; i < currentWaypoints.Length; i++)
            {
                Gizmos.DrawSphere(currentWaypoints[i], 1f);

                if (i < currentWaypoints.Length - 1)
                {
                    Gizmos.DrawLine(currentWaypoints[i], currentWaypoints[i + 1]);
                }                
            }
        }
        
    }
}

