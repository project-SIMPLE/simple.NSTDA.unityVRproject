using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReuseableFruitBunchWithCenter : FruitBase
{
    [SerializeField]
    string hitObjectTag = "Tools";
    [SerializeField]
    private GameObject fruitPrefab;
    [SerializeField]
    private Transform[] tranformList;

    private void ActiveHit()
    {
        if (fruitPrefab == null) return;

        foreach (Transform child in tranformList)
        {
            Instantiate(fruitPrefab, child.position, child.rotation);
        }
        this.gameObject.SetActive(false);
    }
    public override void ActiveFruitBunchOnHook()
    {
        ActiveHit();
    }
}
