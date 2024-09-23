using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBunchWithHook : FruitBase
{

    [SerializeField]
    string hitObjectTag = "Tools";


    [SerializeField]
    GameObject[] fruits;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void ActiveHit()
    {
        //Debug.Log("Hit BOX HITTTT");

        if (fruits.Length <= 0) { this.gameObject.SetActive(false); }
        foreach (GameObject fruit in fruits)
        {
            if (fruit == null) continue;
            if (fruit.GetComponent<Seed>() != null)
            {
                fruit.GetComponent<Seed>().ActiveSeedPhysic();
            }
        }
        this.gameObject.SetActive(false);
    }
    public override void ActiveFruitBunchOnHook()
    {
        ActiveHit();
    }
}
