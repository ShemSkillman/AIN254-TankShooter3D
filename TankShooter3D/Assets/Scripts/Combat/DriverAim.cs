using UnityEngine;
using UnityEngine.UI;
using TankShooter.Movement;
using System;

namespace TankShooter.Combat
{
    public class DriverAim : MonoBehaviour
    {
        [SerializeField] Image aimingReticle;
        [SerializeField] Image aimingBoxBounds;
        [SerializeField] float aimSensitivity = 300f;
        [SerializeField] float maxAimRange = 100f;

        [SerializeField] GameObject[] weapons;
        [SerializeField] Camera driverCamera;
        [SerializeField] Image shootLocationMarkerPrefab;

        RectTransform canvasRect;
        Vector2 minBounds;
        Vector2 maxBounds;

        GunElevate[] gunElevates;
        GunShoot[] gunShoots;
        TurretRotate[] turretRotates;
        Image[] shootLocationMarkers;

        LayerMask layerMask;

        private void Awake()
        {
            canvasRect = GetComponent<RectTransform>();
            gunElevates = new GunElevate[weapons.Length];
            gunShoots = new GunShoot[weapons.Length];
            turretRotates = new TurretRotate[weapons.Length];
            shootLocationMarkers = new Image[weapons.Length];

            for (int i = 0; i < weapons.Length; i++)
            {
                gunElevates[i] = weapons[i].GetComponentInChildren<GunElevate>();
                gunShoots[i] = weapons[i].GetComponentInChildren<GunShoot>();
                turretRotates[i] = weapons[i].GetComponentInChildren<TurretRotate>();
                shootLocationMarkers[i] = Instantiate(shootLocationMarkerPrefab, canvasRect.transform);
            }
        }

        private void Start()
        {
            minBounds = aimingBoxBounds.rectTransform.rect.min;
            maxBounds = aimingBoxBounds.rectTransform.rect.max;

            layerMask = LayerMask.GetMask("Player");
            layerMask = ~layerMask;
        }

        private void FixedUpdate()
        {
            AimTurrets();
        }        

        public void MoveReticle(Vector2 movement)
        {
            movement *= Time.deltaTime * aimSensitivity;

            float xPos = Mathf.Clamp(aimingReticle.rectTransform.anchoredPosition.x + movement.x, minBounds.x, maxBounds.y);
            float yPos = Mathf.Clamp(aimingReticle.rectTransform.anchoredPosition.y + movement.y, minBounds.y, maxBounds.y);

            aimingReticle.rectTransform.anchoredPosition = new Vector2(xPos, yPos);
        }

        private void AimTurrets()
        {
            Vector3 cameraPoint = driverCamera.WorldToViewportPoint(aimingReticle.rectTransform.position);
            Ray ray = driverCamera.ViewportPointToRay(cameraPoint);

            bool isHit = Physics.Raycast(ray, out RaycastHit hit, maxAimRange, layerMask);

            Debug.DrawRay(ray.origin, ray.direction * 100f);

            if (!isHit) hit.point = ray.origin + ray.direction * maxAimRange;

            Vector3 location = hit.point;

            for(int i = 0; i < weapons.Length; i++)
            {
                turretRotates[i].RotateTurretTowards(location);
                gunElevates[i].ElevateGunTowards(location);

                Vector2 viewportPos = driverCamera.WorldToViewportPoint(gunShoots[i].GetShootLocation(maxAimRange));
                Vector2 canvasPos = new Vector2(
                ((viewportPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                ((viewportPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
                
                shootLocationMarkers[i].rectTransform.anchoredPosition = canvasPos;
            }
        }

        public void ShootTurrets(float input)
        {
            if (input == 0f) return;

            foreach(GunShoot gunShoot in gunShoots)
            {
                gunShoot.ShootGun();
            }
        }
    }
}

