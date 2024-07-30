using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform prefab;
    public float randomRange = 0.5f;
    public float timeToSpawn = 4.0f;

    private float timer;

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToSpawn)
        {
            timer = 0.0f;

            var obj = Instantiate(prefab, transform.position + Random.insideUnitSphere * randomRange, transform.rotation);
            obj.gameObject.SetActive(true);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, randomRange);
    }
}
