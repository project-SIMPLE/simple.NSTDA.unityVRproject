using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireSmallArea : BaseFire,ICreateFireOnTree,IGlobalThreat
{
    [SerializeField]
    private List<GameObject> treeInArea;

    [SerializeField]
    protected float targetAuraSize = 7;
    [SerializeField]
    private float fireSpreadRate = 0.3f;
    [SerializeField]
    private GameObject flameOnTreePrefab;
    [SerializeField]
    private List<GameObject> flameEffectLists;

    private void OnEnable()
    {
        
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect += OnRainShow;
        VU2ForestProtectionEventManager.Instance.OnGameStop += KillFire;
        VU2ForestProtectionEventManager.Instance.OnRemoveGlobalThreat += RemoveGlobalThreat;
    }
    private void OnDisable()
    {
        VU2ForestProtectionEventManager.Instance.OnUpdateRainEffect -= OnRainShow;
        VU2ForestProtectionEventManager.Instance.OnGameStop -= KillFire;
        VU2ForestProtectionEventManager.Instance.OnRemoveGlobalThreat -= RemoveGlobalThreat;
    }

    protected override void SetToInitialState()
    {
        base.SetToInitialState();
        flameHitBox.radius = 2;
        treeInArea = new List<GameObject>();
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
            flameHitBox.radius += fireSpreadRate * Time.deltaTime;
        }
    }

    protected override void FlameRecoverFromWater()
    {
        base.FlameRecoverFromWater();
    }

    public override void KillFire()
    {
        if (treeInArea == null) return;

        foreach (GameObject tree in treeInArea)
        {
            if (tree == null) continue;
            tree.gameObject.GetComponent<Seeding>().UnburnTree();
        }
        treeInArea.Clear();
        //VU2ObjectPoolManager.Instance?.ReturnObjectToPool(this.gameObject);
        //this.gameObject.SetActive(false);
        RemoveAllFireOnTree();
        base.KillFire();
    }
    private void OnRainShow(bool isRain)
    {
        if (isRain) KillFire();
    }
    private void OnTriggerEnter(Collider other)
    {
        /*
        if (!treeInArea.Contains(other.gameObject) && other.gameObject.CompareTag("tree"))
        {
            treeInArea.Add(other.gameObject);
            other.gameObject.GetComponent<Seeding>().TreeBurn();
            CreateFireOnTree(other.gameObject);
        }
*/
    }

    private void OnCollisionEnter(Collision collision)
    {
/*
        Debug.Log("HIT Tree!!");
        if (!treeInArea.Contains(collision.gameObject) && collision.gameObject.CompareTag("tree"))
        {
            treeInArea.Add(collision.gameObject);
            collision.gameObject.GetComponent<Seeding>().TreeBurn();
            CreateFireOnTree(collision.gameObject);
        }
*/
    }
    private void OnCollisionStay(Collision collision)
    {
        
    }


    protected virtual void CreateFireOnTree(GameObject target)
    {
        if (flameEffectLists == null)
        {
            flameEffectLists = new List<GameObject>();
        }
        GameObject tmp = VU2ObjectPoolManager.Instance?.SpawnObject(flameOnTreePrefab, target.transform.position, this.transform.rotation);
        if (!flameEffectLists.Contains(tmp))
        {
            flameEffectLists.Add(tmp);
        }

    }
    protected virtual void RemoveAllFireOnTree()
    {
        if (flameEffectLists == null) return;
        foreach (GameObject fire in flameEffectLists)
        {
            if (fire == null) continue;
            //flameEffectLists.Remove(fire);
            VU2ObjectPoolManager.Instance?.ReturnObjectToPool(fire);
            
        }
        flameEffectLists.Clear();
    }

    public virtual void OnFireHitTree(GameObject tree)
    {
        if (treeInArea == null)
        {
            treeInArea = new List<GameObject>();
        }
        //Debug.Log("HIT Tree!!");
        if (!treeInArea.Contains(tree) && tree.CompareTag("tree"))
        {
            treeInArea.Add(tree);
            CreateFireOnTree(tree);
        }
    }

    public void RemoveGlobalThreat(GlobalThreat type)
    {
        if(type == GlobalThreat.Fire)
        {
            KillFire();
        }
    }
}
