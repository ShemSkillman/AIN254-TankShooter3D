using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankExplosion : MonoBehaviour
{
    public delegate void ExplosionFinished();
    public event ExplosionFinished onExplosionFinished;

    [SerializeField] GameObject miniExplosionPrefab;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionPower = 10f;
    [SerializeField] float minTankPartLifeTime = 1f;
    [SerializeField] float maxTankPartLifeTime = 8f;

    public void Explode(Rigidbody[] tankParts)
    {
        foreach (Rigidbody part in tankParts)
        {
            Joint joint = part.GetComponent<Joint>();
            Destroy(joint);
            part.useGravity = true;
            part.drag = 0;            
        }

        foreach (Rigidbody part in tankParts)
        {
            part.AddExplosionForce(explosionPower * part.mass, transform.position + Random.insideUnitSphere, explosionRadius, 1f);

            StartCoroutine(DestroyTankPart(part));
        }

        StartCoroutine(DestroyTank());
    }

    IEnumerator DestroyTankPart(Rigidbody tankPart)
    {
        float timeDelay = Random.Range(minTankPartLifeTime, maxTankPartLifeTime);
        yield return new WaitForSeconds(timeDelay);

        if (miniExplosionPrefab != null) Instantiate(miniExplosionPrefab, tankPart.position, Quaternion.identity);
        Destroy(tankPart.gameObject);
    }

    IEnumerator DestroyTank()
    {
        yield return new WaitForSeconds(maxTankPartLifeTime * 2);

        onExplosionFinished?.Invoke();
    }

}
