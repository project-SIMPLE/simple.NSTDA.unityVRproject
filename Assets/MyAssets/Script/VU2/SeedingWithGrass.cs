using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedingWithGrass : Seeding
{
    [SerializeField]
    private GameObject[] grasses;
    [SerializeField]
    private int grassCount;

    

    private bool haveGrasses = false;
    public void GrassesGrow()
    {
        grassCount = 1;
        foreach(GameObject grass in grasses)
        {
            grass.SetActive(true);
        }
        if (!haveGrasses)
        {
            GotWeedOnTree();
            haveGrasses = true;
        }
        
    }
    public void GrassCut()
    {
        grassCount--;
        if(grassCount <= 0)
        {
            grassCount = 0;
            RemoveWeedOnTree();
            VU2BGSoundManager.Instance?.PlayTreeSoundEffect(this.gameObject,2);
            haveGrasses = false;
        }
    }

    
}
