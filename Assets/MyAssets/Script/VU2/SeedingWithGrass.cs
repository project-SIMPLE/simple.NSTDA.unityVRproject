using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedingWithGrass : Seeding
{
    [SerializeField]
    private GameObject[] grasses;
    [SerializeField]
    private int grassCount;

    public void GrassesGrow()
    {
        grassCount = 3;
        foreach(GameObject grass in grasses)
        {
            grass.SetActive(true);
        }
        GotWeedOnTree();
    }
    public void GrassCut()
    {
        grassCount--;
        if(grassCount <= 0)
        {
            grassCount = 0;
            RemoveWeedOnTree();
        }
    }



}
