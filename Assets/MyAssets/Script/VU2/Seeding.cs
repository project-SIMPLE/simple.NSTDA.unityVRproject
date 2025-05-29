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
    private float grownTime;
    [SerializeField]
    private float grownCount = 0f;
    [SerializeField]
    private float grownRate = 1.0f;

    [SerializeField]
    private int maxGrownState;


    [SerializeField]
    private int WeedCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        treeState = 1;
        treeStateModels[treeState].SetActive(true);

        maxGrownState = treeStateModels.Length -1;
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
        if(grownCount >= grownTime) 
        {
            TreeGrown();
        }*/
    }
    private void TreeGrown()
    {
        grownCount = 0;


        treeStateModels[treeState].SetActive(false);
        treeState += 1;
        treeStateModels[treeState].SetActive(true);
        
    }

    private void TreeDegrown()
    {
        grownCount = 0;
        if (treeState == 0) return;

        treeStateModels[treeState].SetActive(false);
        treeState -= 1;
        treeStateModels[treeState].SetActive(true);
        
    }
    public void ChangeGrowState(int state)
    {
        treeStateModels[treeState].SetActive(false);
        treeState = state;
        treeStateModels[treeState].SetActive(true);

    }
    private void ChangeGrownRate()
    {
        switch(WeedCount)
        {
            case 0:
                grownRate = 1.0f;
                break;
            case 1:
                grownRate = 0.8f;
                break;
            case 2:
                grownRate = 0.6f;
                break;
            case 3:
                grownRate = 0.4f;
                break;
            default:
                grownRate = 0f;
                break;

        }
    }

    private void Treeburn()
    {
        /*treeStateModels[treeState].SetActive(false);
        treeState = -1;*/
        ChangeGrowState(0);
    }
    private void AddFertilizer()
    {
        float tmp = (grownTime - grownCount)/2;
        grownCount += tmp;
    }
    public void ChangeWeedCount(int num)
    {
        WeedCount += num;
        ChangeGrownRate();
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
