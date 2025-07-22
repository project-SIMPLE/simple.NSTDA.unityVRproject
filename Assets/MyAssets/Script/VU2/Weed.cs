using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class Weed : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> treeInArea;


    [SerializeField]
    private int weedHP = 1;
    [SerializeField]
    private WeedState weedState;

    [SerializeField]
    private GrowDir growFrom;
    [SerializeField]
    private float growCount;

    [SerializeField]
    private float timeUnitlWeedSpread;
    [SerializeField]
    private float spreadingRadius;

    private Dictionary<GrowDir, Vector3> growPos = new Dictionary<GrowDir, Vector3>()
    {
        { GrowDir.Center ,      new Vector3(0, 0, 0) },
        { GrowDir.UpLeft ,      new Vector3(-1, 0, 1) },
        { GrowDir.Up ,          new Vector3(0, 0, 1) },
        { GrowDir.UpRight ,     new Vector3(1, 0, 1) },
        { GrowDir.Right ,       new Vector3(1, 0, 0) },
        { GrowDir.DownRight ,   new Vector3(1, 0, -1) },
        { GrowDir.Down ,        new Vector3(0, 0, -1) },
        { GrowDir.DownLeft ,    new Vector3(-1, 0, -1) },
        { GrowDir.Left ,        new Vector3(-1, 0, 0) }
    };



    private bool isOnCooldown = false;
    private float cooldown = 0.5f;
    private float count = 0;

    [SerializeField]
    private bool isSpreading;
    private float weedTimer = 0;

    [SerializeField]
    private GameObject Model;
    [SerializeField]
    private float targetSize = 0.2f;


    void Start()
    {
        treeInArea = new List<GameObject>();
    }
    private void OnEnable()
    {
        weedState = WeedState.Growing;
        targetSize = UnityEngine.Random.Range(0.15f, 0.25f);
        Model.transform.localScale = new Vector3 (0.05f, 0.05f, 0.05f);
    }

    private void FixedUpdate()
    {
        if(isOnCooldown)
        {
            count += Time.deltaTime;
            if(count >= cooldown)
            {
                isOnCooldown = false;
                count = 0;
            }
        }
        else
        {
            if (weedState != WeedState.Finish)
            {
                

                switch (weedState)
                {
                    case WeedState.Growing:

                        if(Model.transform.localScale.x < targetSize)
                        {
                            Model.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 0.008f;
                        }
                        else
                        {
                            weedState = WeedState.Spreading;
                        }


                        break;
                    case WeedState.Spreading:
                        if (!isSpreading)
                        {
                            weedState = WeedState.Finish;
                        }
                        else
                        {
                            weedTimer += Time.deltaTime;
                            if (weedTimer >= timeUnitlWeedSpread)
                            {
                                CreateNewWeed();
                                weedState = WeedState.Finish;
                            }
                        }
                        break;
                }
            }

            /*if (isSpreading)
            {
                weedTimer += Time.deltaTime;
                if(weedTimer >= timeUnitlWeedSpread)
                {
                    CreateNewWeed();
                    isSpreading = false;
                }
            }*/
        }
    }
    
    
    private void CreateNewWeed()
    {
        Vector3 currentPos = this.transform.position;

        foreach (GrowDir dir in growPos.Keys.ToList())
        {
            Vector3 pos;
            if(growPos.TryGetValue(dir, out pos))
            {
                Vector3 tmp = currentPos + (pos * spreadingRadius);
                if (IsLocationEmpty(tmp))
                {
                    if( ((growCount*20f)+50f) < UnityEngine.Random.Range(0f, 100f)) break;

                    GameObject obj = Instantiate(this.gameObject, tmp, this.transform.rotation);
                    Weed objScript = obj.GetComponent<Weed>();
                    objScript.SetGrowFrom(dir);
                }
            }
        }



        /*if(growFrom == GrowDir.Center)
        {

        }
        else
        {

        }*/

        isSpreading = false;
    }
    private float overlapRadious = 0.3f;
    public bool IsLocationEmpty(Vector3 pos)
    {
        Collider[] hitColliders =  Physics.OverlapSphere(pos, overlapRadious);
        if(hitColliders.Length > 0)
        {
            foreach (var collider in hitColliders)
            {
                if (collider.gameObject.tag == "Weed" || collider.gameObject.tag == "Fire")
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("TAG ="+ collision.gameObject.tag);
        if (collision.gameObject.tag == "Tools")
        {
            Debug.Log("Weed CUTTTTTT");
            if(!isOnCooldown)
            {
                isOnCooldown = true;
                ReduceHP();
            }
        }
    }
    private void ReduceHP()
    {
        weedHP--;
        if(weedHP <= 0)
        {
            OnWeedDestroyed();
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger TAG =" + other.gameObject.tag);

        if (!treeInArea.Contains(other.gameObject) && other.gameObject.tag== "tree")
        {
            treeInArea.Add(other.gameObject);
            //other.gameObject.GetComponent<Seeding>().ChangeWeedCount(1);
            other.gameObject.GetComponent<Seeding>().GotWeedOnTree();
            isSpreading = false;
        }else if(other.gameObject.tag== "Tools")
        {
            if (!isOnCooldown)
            {
                isOnCooldown = true;
                ReduceHP();
            }

        }
        else if(other.gameObject.tag== "Fire")
        {
            ReduceHP();
        }
    }


    private void OnWeedDestroyed()
    {
        if (treeInArea.Count <= 0) return;

        foreach (GameObject tree in treeInArea)
        {
            if (tree == null) return;
            //tree.gameObject.GetComponent<Seeding>().ChangeWeedCount(-1);
            tree.gameObject.GetComponent<Seeding>().RemoveWeedOnTree();
        }
    }
    public void SetGrowFrom(GrowDir dir)
    {
        growFrom = dir;
        
        growCount--;
        if(growCount <= 0) { growCount = 0; }
    }
    
/*
    private struct WeedGrowDir
    {
        GrowDir posIndex;
        Vector3[] Pos;
    }
*/
    private enum WeedState
    {
        Growing,
        Spreading,
        Finish
    }
    public enum GrowDir
    {
        Center = 0,
        UpLeft = 1,
        Up = 2,
        UpRight = 3,
        Right = 4,
        DownRight = 5,
        Down = 6,
        DownLeft = 7,
        Left = 8
    }
}
