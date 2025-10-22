using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeding : MonoBehaviour
{
    [SerializeField]
    private GameObject[] treeStateModels;

    /**
     * 0 = dead
     * 1 = defaul
     * 2 = grow 1
     * 3 = grow 2
     * 
     **/
    [SerializeField]
    private int treeState;
    [SerializeField]
    private float timeToGrow;
    [SerializeField]
    private float growTimer = 0f;
    /*[SerializeField]
    private float growRate = 1.0f;

    [SerializeField]
    private int maxGrowState;*/

    [SerializeField]
    private GameObject alertIcon;

    [SerializeField]
    private int WeedCount = 0;

    [SerializeField]
    private bool isTreeDying;
    // Start is called before the first frame update
    void Start()
    {
        treeState = 1;
        treeStateModels[treeState].SetActive(true);
        alertIcon.SetActive(false);
        isTreeDying = false;
        //maxGrowState = treeStateModels.Length -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private float deathTimer = 0;
    private float timeUntilDie = 20f;
    private void FixedUpdate()
    {
        if(isTreeDying)
        {
            if (deathTimer >= timeUntilDie)
            {
                TreeDied();
                deathTimer = 0;
            }
            deathTimer += Time.deltaTime;
        }
/*
        if(WeedCount >= 7 && treeState == 1)
        {
            
        }*/
        /*
        if (treeState == maxGrownState || treeState == -1) return;
        grownCount += grownRate * Time.deltaTime;
        if(grownCount >= timeToGrow) 
        {
            TreeGrown();
        }*/
    }
    private void TreeGrown()
    {
        growTimer = 0;


        treeStateModels[treeState].SetActive(false);
        treeState += 1;
        treeStateModels[treeState].SetActive(true);
        
    }

    private void TreeDegrown()
    {
        growTimer = 0;
        if (treeState == 0) return;

        treeStateModels[treeState].SetActive(false);
        treeState -= 1;
        treeStateModels[treeState].SetActive(true);
        
    }
    public void ChangeGrowState(int state)
    {
        if(state == 99)
        {
            return;
        }
        treeStateModels[treeState].SetActive(false);
        treeState = state;
        treeStateModels[treeState].SetActive(true);
        if (alertIcon.activeSelf && state != 1)
        {
            alertIcon.SetActive(false);
        }
    }
/*
    private void ChangeGrownRate()
    {
        switch(WeedCount)
        {
            case 0:
                growRate = 1.0f;
                break;
            case 1:
                growRate = 0.8f;
                break;
            case 2:
                growRate = 0.6f;
                break;
            case 3:
                growRate = 0.4f;
                break;
            default:
                growRate = 0f;
                break;

        }
    }*/
    /*private void AddFertilizer()
    {
        float tmp = (timeToGrow - growTimer)/2;
        growTimer += tmp;
    }*/

    private void IsTreeDysing()
    {
        if (WeedCount >= 7 && treeState == 1)
        {
            isTreeDying = true;
            alertIcon.SetActive(true);

        }
        else
        {
            alertIcon.SetActive(false);
            if (fireOnTree != 0)
            {
                isTreeDying = false;
            }
        }
        
        
    }

    public void ChangeWeedCount(int num)
    {
        WeedCount += num;
        //ChangeGrownRate();
    }

    //
    // -1 = stop growing
    //  0 = die
    //  1 = growing
    //
    private void TreeDied()
    {
        /*treeStateModels[treeState].SetActive(false);
        treeState = -1;*/
        ChangeGrowState(0);
        VU2ForestProtectionEventManager.Instance?.TreeChangeState(this.gameObject.name, "0");
    }

    private int fireOnTree = 0;
    public void TreeBurn(GameObject other)
    {
        //Debug.Log(other);
        
        if (other.transform.parent.TryGetComponent<ICreateFireOnTree>(out ICreateFireOnTree fire))
        {
            
            fire.OnFireHitTree(this.gameObject);
        }

        isTreeDying = true;
        fireOnTree++;
    }
    public void UnburnTree()
    {
        fireOnTree--;
        if( fireOnTree <= 0)
        {
            fireOnTree = 0;
            isTreeDying = false;
        }
    }

    
    public void GotWeedOnTree()
    {
        if(WeedCount == 0 && treeState!=0)
        {
            VU2ForestProtectionEventManager.Instance?.TreeChangeState(this.gameObject.name,"-1");
        }
        WeedCount++;
        IsTreeDysing();
    }
    
    public void RemoveWeedOnTree()
    {
        if(WeedCount > 0)
        {
            WeedCount--;
        }

        if(WeedCount == 0 && treeState != 0)
        {
            VU2ForestProtectionEventManager.Instance?.TreeChangeState(this.gameObject.name,"1");
            
        }
        IsTreeDysing();
    }
    private void OnParticleCollision(GameObject other)
    {
        
        if (other.transform.tag == "Fire")
        {
            Debug.Log("Tree Burn1");
            /*TreeDied();*/
            TreeBurn(other);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if(other.gameObject.CompareTag("Fire"))
        {
            Debug.Log("Tree Burn2");
            //TreeDied();
            TreeBurn(other.gameObject);
        }
        if (other.gameObject.CompareTag("Tools"))
        {
            if (other.gameObject.transform.parent == null) return;


            if(other.gameObject.transform.parent.TryGetComponent<WeedCutter>(out WeedCutter weedScript))
            {
                if (weedScript.IsItemOnPlayerHand())
                {
                    
                    Debug.Log("Player cut tree");
                    TreeDied();
                }
            }
        }
        /*
        if (other.gameObject.tag == "Weed")
        {
            WeedCount++;
            ChangeGrownRate();
        }*/
        
    }
    private void OnTriggerExit(Collider other)
    {
        /*if (other.gameObject.tag == "Weed")
        {
            WeedCount--;
            ChangeGrownRate();
        }*/
    }

   

}
