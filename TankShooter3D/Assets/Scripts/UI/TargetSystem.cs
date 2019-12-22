using UnityEngine;
using UnityEngine.UI;
using TankShooter.Combat;

namespace TankShooter.UI
{
    public class TargetSystem : MonoBehaviour
    {
        [SerializeField] float maxTargetDistance = 100f;
        [SerializeField] Camera gunnerCam;
        [SerializeField] Image targetMarkerPrefab;
        [SerializeField] Transform gunEnd;

        public delegate void OnTargetLock(bool isTargetLocked);
        public event OnTargetLock onTargetLocked;

        LayerMask enemyMask;
        RectTransform canvasRect;
        Crosshair crosshair;

        Health lockedTarget;
        Image lockedTargetMarker;
        Health spottedTarget;
        Image spottedTargetMarker;

        private void Awake()
        {
            canvasRect = GetComponent<RectTransform>();
            crosshair = GetComponent<Crosshair>();
        }

        private void Start()
        {
            enemyMask = LayerMask.GetMask("Enemy detection");
        }

        // Update is called once per frame
        void Update()
        {
            UpdateSpottedTarget();
            UpdateLockedTarget();

            UpdateTargetMarkerPosition(spottedTarget, spottedTargetMarker);
            UpdateTargetMarkerPosition(lockedTarget, lockedTargetMarker);
          
        }

        private void UpdateSpottedTarget()
        {
            bool isHit = Physics.Raycast(gunEnd.position, gunEnd.forward, out RaycastHit hit, maxTargetDistance, enemyMask);

            if (!isHit || (lockedTarget != null && spottedTarget == lockedTarget))
            {
                spottedTarget = null;
                if (spottedTargetMarker != null) Destroy(spottedTargetMarker.gameObject);
                return;
            }

            Health health = hit.collider.GetComponentInParent<Health>();

            if (health == null || health.GetIsDead() || spottedTarget == health || lockedTarget == health) return;

            spottedTarget = health;            
            if (spottedTargetMarker == null) spottedTargetMarker = Instantiate(targetMarkerPrefab, canvasRect.transform);
        }

        private void UpdateTargetMarkerPosition(Health target, Image targetMarker)
        {
            if (target == null)
            {
                if (targetMarker != null) Destroy(targetMarker.gameObject);
                return;
            }

            Vector3 enemyTankPos = target.GetTankPosition();
            Vector3 viewportPos = gunnerCam.WorldToViewportPoint(enemyTankPos);

            Vector2 canvasPos = new Vector2(
            ((viewportPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((viewportPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

            if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1) targetMarker.gameObject.SetActive(false);
            else targetMarker.gameObject.SetActive(true);

            targetMarker.rectTransform.anchoredPosition = canvasPos;

            targetMarker.gameObject.SetActive(true);
        }

        private void UpdateLockedTarget()
        {
            crosshair.SetTarget(lockedTarget);

            if (lockedTarget != null && lockedTarget.GetIsDead()) LockTarget();
        }

        public void LockTarget()
        {
            if (spottedTarget == null)
            {
                lockedTarget = null;
                if (lockedTargetMarker != null) Destroy(lockedTargetMarker.gameObject);

                onTargetLocked(false);
            }
            else
            {
                lockedTarget = spottedTarget;

                if (lockedTargetMarker == null)
                {
                    lockedTargetMarker = Instantiate(targetMarkerPrefab, canvasRect.transform);
                    lockedTargetMarker.gameObject.SetActive(false);
                    lockedTargetMarker.color = Color.green;
                }

                onTargetLocked(true);
            }
        }
    }
}

