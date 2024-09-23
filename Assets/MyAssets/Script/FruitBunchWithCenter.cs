using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBunchWithCenter : FruitBase
{

    [SerializeField]
    string hitObjectTag = "Tools";
    [SerializeField]
    private GameObject interactableFruits;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void ActiveHit()
    {
        if (interactableFruits != null && interactableFruits.transform.childCount > 0)
        {
            foreach (Transform child in interactableFruits.transform)
            {

                if (child == null) continue;
                if (child.GetComponent<Seed>() != null)
                {
                    child.GetComponent<Seed>().ActiveSeedObject();
                }
            }
        }
        this.gameObject.SetActive(false);

    }
    public override void ActiveFruitBunchOnHook()
    {
        ActiveHit();
    }


}
