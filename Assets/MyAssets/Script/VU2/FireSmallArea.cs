using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSmallArea : BaseFire
{
    [SerializeField]
    private List<GameObject> treeInArea;

    [SerializeField]
    private float targetAuraSize = 7;
    [SerializeField]
    private float fireSpreadRate = 0.3f;
    [SerializeField]
    private GameObject flameOnTreePrefab;


    private void OnEnable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += OnRainShow;
        VU2ForestProtectionEventManager.Instance.OnGameStop += KillFire;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= OnRainShow;
        VU2ForestProtectionEventManager.Instance.OnGameStop -= KillFire;
    }

    protected override void SetToInitialState()
    {
        base.SetToInitialState();
        flameHitBox.radius = 2;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void FlameGrowingByTime()
    {

        base.FlameGrowingByTime();
        IncreaseFireSize();
    }

    private void IncreaseFireSize()
    {
        if (flameHitBox.radius < targetAuraSize)
        {
            Debug.Log("Test");
            flameHitBox.radius += fireSpreadRate * Time.deltaTime;
        }
    }

    protected override void FlameRecoverFromWater()
    {
        base.FlameRecoverFromWater();
    }

    protected override void KillFire()
    {
        base.KillFire();

        foreach (GameObject tree in treeInArea)
        {
            if (tree == null) return;
            tree.gameObject.GetComponent<Seeding>().UnburnTree();
        }
        VU2ObjectPoolManager.Instance?.ReturnObjectToPool(this.gameObject);
        RemoveAllFireOnTree();
    }
    private void OnRainShow(bool isRain)
    {
        if (isRain) KillFire();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!treeInArea.Contains(collision.gameObject) && collision.gameObject.CompareTag("tree"))
        {
            treeInArea.Add(collision.gameObject);
            collision.gameObject.GetComponent<Seeding>().TreeBurn();
            CreateFireOnTree(collision.gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        
    }
    private List<GameObject> flameEffectLists;
    private void CreateFireOnTree(GameObject target)
    {
        if (flameOnTreePrefab == null) return;
        GameObject tmp = VU2ObjectPoolManager.Instance?.SpawnObject(flameOnTreePrefab, target.transform.position, this.transform.rotation);
        if (!flameEffectLists.Contains(tmp))
        {
            flameEffectLists.Add(tmp);
        }

    }
    private void RemoveAllFireOnTree()
    {
        foreach (GameObject fire in flameEffectLists)
        {
            if (fire == null) return;
            VU2ObjectPoolManager.Instance?.ReturnObjectToPool(fire);
        }
    }

}
