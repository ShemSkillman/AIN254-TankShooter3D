using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateSpawner : MonoBehaviour
{
    bool ammoCrateConsumed = false;

    [SerializeField] GameObject instance;
    [SerializeField] GameObject ammoCratePrefab;
    [SerializeField] float timeUntilRefresh = 300f;

    // Update is called once per frame
    void Update()
    {
        if (instance == null && !ammoCrateConsumed)
        {
            ammoCrateConsumed = true;
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeUntilRefresh);

        instance = Instantiate(ammoCratePrefab, transform.position, transform.rotation, transform);
        ammoCrateConsumed = false;
    }
}
