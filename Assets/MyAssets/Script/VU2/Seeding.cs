using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeding : MonoBehaviour
{
    [SerializeField]
    private GameObject[] treeStateModels;
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

    private int WeedCount =0;
    // Start is called before the first frame update
    void Start()
    {
        treeState = 0;
        treeStateModels[treeState].SetActive(true);

        maxGrownState = treeStateModels.Length -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (treeState == maxGrownState) return;
        grownCount += grownRate * Time.deltaTime;
        if(grownCount >= grownTime) 
        {
            TreeGrown();
        }
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


    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if(tag == "Fire")
        {

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
