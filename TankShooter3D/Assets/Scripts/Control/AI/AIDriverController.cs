using UnityEngine;
using TankShooter.Movement;
using UnityEngine.AI;
using System.Collections;
using System;

namespace TankShooter.Control
{
    public class AIDriverController : MonoBehaviour
    {
        [Header("Pathfinding")]
        [SerializeField] float pathRefreshRate = 0.25f;
        [SerializeField] float maxWanderRange = 5f;
        [SerializeField] float maxWaitTime = 10f;

        [Header("Tolerances")]
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float turnTolerance = 0.1f;
        
        Transform followTarget;        
        bool isStationary = false;
        TankMove tankMove;
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
                
                if (traversePath != null || isStationary) continue;

                NavMeshPath newPath = new NavMeshPath();
                Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
                NavMesh.CalculatePath(pos, GetDestination(), NavMesh.AllAreas, newPath);

                if (newPath.status == NavMeshPathStatus.PathInvalid) continue;

                currentWaypoints = newPath.corners;                
                traversePath = StartCoroutine(TraversePath());              
            }
        }        

        IEnumerator TraversePath()
        {
            int waypointIndex = 0;

            while (waypointIndex < currentWaypoints.Length)
            {
                Vector3 currentWaypoint = currentWaypoints[waypointIndex];

                bool isTurning = tankMove.TurnTankTowards(currentWaypoint, turnTolerance);

                if (!isTurning)
                {
                    bool isMoving = tankMove.MoveTankTowards(currentWaypoint, waypointTolerance);
                    if (!isMoving) waypointIndex++;
                }

                yield return null;
            }
            
            yield return new WaitForSeconds(GetWaitTime());

            traversePath = null;
        }

        private float GetWaitTime()
        {
            if (followTarget != null) return 0f;

            return UnityEngine.Random.Range(0, maxWaitTime);
        }

        private Vector3 GetDestination()
        {
            Vector3 location;

            if (followTarget != null)
            {
                location = followTarget.position;
            }
            else
            {
                location = (UnityEngine.Random.insideUnitSphere * maxWanderRange) + transform.position;
            }            

            NavMesh.SamplePosition(location, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas);
            return hit.position;
        }

        private bool AtWaypoint(Vector3 waypoint)
        {
            Vector2 tankPos = new Vector3(transform.position.x, transform.position.z);
            Vector2 waypointPos = new Vector3(waypoint.x, waypoint.z);

            float distance = Vector2.Distance(tankPos, waypointPos);

            if (distance < waypointTolerance) return true;

            return false;
        }

        public void SetToFollow(Transform target)
        {
            if (followTarget == null && followTarget != target) StopCurrentPath();
            followTarget = target;
        }

        private void StopCurrentPath()
        {
            if (traversePath == null) return;

            StopCoroutine(traversePath);
            traversePath = null;
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

        public void SetIsStationary(bool isStationary)
        {
            this.isStationary = isStationary;
            if (traversePath != null && isStationary == true) StopCurrentPath();
        }
    }
}

