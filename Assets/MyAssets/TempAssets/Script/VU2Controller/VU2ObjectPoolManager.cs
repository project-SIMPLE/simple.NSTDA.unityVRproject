using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class VU2ObjectPoolManager : MonoBehaviour
{
    public static VU2ObjectPoolManager Instance { get; private set; }

    private GameObject threatsHolder;

    private Dictionary<GameObject, ObjectPool<GameObject>> objectPools;
    private Dictionary<GameObject, GameObject> prefabMap;

    /*public enum PoolType
    {
        Alien,
        Flame1,
        Flame2,
    }
    public PoolType PoolingType;*/

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
        prefabMap = new Dictionary<GameObject, GameObject>();

        threatsHolder = new GameObject("Threat Pools");
    }

    private void CreatePool(GameObject prefab, Vector3 pos, quaternion rot)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () => CreateObjects(prefab, pos, rot),
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject
            );

        objectPools.Add(prefab, pool);
    }
    private GameObject CreateObjects(GameObject prefab, Vector3 pos, quaternion rot)
    {
        prefab.SetActive(false);

        GameObject obj = Instantiate(prefab, pos, rot);
        obj.transform.SetParent(threatsHolder.transform);

        return obj;
    }
    private void OnGetObject(GameObject obj)
    {

    }
    private void OnReleaseObject(GameObject obj) 
    {
        obj.SetActive(false);
    }
    private void OnDestroyObject(GameObject obj)
    {
        if(prefabMap.ContainsKey(obj))
        {
            prefabMap.Remove(obj);

        }
    }
    
    public GameObject SpawnObject(GameObject spawnPrefab, Vector3 pos, quaternion rot)
    {
        if(!objectPools.ContainsKey(spawnPrefab))
        {
            CreatePool(spawnPrefab, pos, rot);
        }

        GameObject obj = objectPools[spawnPrefab].Get();

        if(obj != null) 
        {
            if(!prefabMap.ContainsKey(obj))
            {
                prefabMap.Add(obj, spawnPrefab);
            }

            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.SetActive(true);

            return obj;
        }

        return null;
    }

    public void ReturnObjectToPool(GameObject returnPrefab)
    {
        if(prefabMap.TryGetValue(returnPrefab, out GameObject prefab)) 
        { 
            if(objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            {
                pool.Release(returnPrefab);
            }

        }
        else
        {
            Debug.Log("Error: can not return to pool: "+ returnPrefab.name);
        }
    }

}
