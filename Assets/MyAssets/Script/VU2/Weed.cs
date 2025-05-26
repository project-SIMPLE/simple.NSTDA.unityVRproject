using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weed : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> treeInArea;

    [SerializeField]
    private int weedHP = 1;

    void Start()
    {
        treeInArea = new List<GameObject>();
    }

    private bool isOnCooldown = false;
    private float cooldown = 0.5f;
    private float count = 0;

    private void FixedUpdate()
    {
        if(isOnCooldown)
        {
            count -= Time.deltaTime;
            if(count <= cooldown)
            {
                isOnCooldown = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Tools")
        {
            Debug.Log("Weed CUTTTTTT");
            if(isOnCooldown)
            {

            }
            else
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
        
        if (!treeInArea.Contains(other.gameObject) && other.gameObject.tag== "tree")
        {
            treeInArea.Add(other.gameObject);
            other.gameObject.GetComponent<Seeding>().ChangeWeedCount(1);
            
        }
    }

    private void OnWeedDestroyed()
    {
        if (treeInArea.Count <= 0) return;

        foreach (GameObject tree in treeInArea)
        {
            if (tree == null) return;
            tree.gameObject.GetComponent<Seeding>().ChangeWeedCount(-1);
        }
    }
}
