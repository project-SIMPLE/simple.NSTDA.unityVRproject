using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBunch : MonoBehaviour
{
    [SerializeField]
    string hitObjectTag = "Tools";

    [SerializeField]
    GameObject[] fruits;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals(hitObjectTag, System.StringComparison.InvariantCultureIgnoreCase))
        {
            ActiveHit();
        }
    }

    private void ActiveHit()
    {
        Debug.Log("Hit BOX HITTTT");

        if (fruits.Length <= 0) { this.gameObject.SetActive(false); }
        foreach (GameObject fruit in fruits)
        {
            if (fruit.GetComponent<Rigidbody>()!= null)
            {
                fruit.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
