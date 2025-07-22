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
    [SerializeField]
    private float growRate = 1.0f;

    [SerializeField]
    private int maxGrowState;


    [SerializeField]
    private int WeedCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        treeState = 1;
        treeStateModels[treeState].SetActive(true);

        maxGrowState = treeStateModels.Length -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
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

    }
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
    }
    private void AddFertilizer()
    {
        float tmp = (timeToGrow - growTimer)/2;
        growTimer += tmp;
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
    private void Treeburn()
    {
        /*treeStateModels[treeState].SetActive(false);
        treeState = -1;*/
        ChangeGrowState(0);
        VU2ForestProtectionEventManager.Instance?.TreeChangeState(this.gameObject.name, "0");
    }
    
    public void GotWeedOnTree()
    {
        if(WeedCount == 0)
        {
            VU2ForestProtectionEventManager.Instance?.TreeChangeState(this.gameObject.name,"-1");
        }
        WeedCount++;
    }
    
    public void RemoveWeedOnTree()
    {
        if(WeedCount > 0)
        {
            WeedCount--;
        }

        if(WeedCount == 0)
        {
            VU2ForestProtectionEventManager.Instance?.TreeChangeState(this.gameObject.name,"1");
        }
        
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Tree Burn1");
        if (other.transform.tag == "Fire")
        {
            Debug.Log("Tree Burn");
            Treeburn();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if(tag == "Fire")
        {
            Debug.Log("Tree Burn");
            Treeburn();
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
